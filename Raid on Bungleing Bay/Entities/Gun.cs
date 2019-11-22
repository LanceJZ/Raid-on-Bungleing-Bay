#region Using
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Linq;
using System;
using Panther;
#endregion
namespace Raid_on_Bungleing_Bay.Entities
{
    class Gun : ModelEntity
    {
        #region Fields
        GameLogic _logic;
        Shot _shot;
        Player _player;
        Gun _master;
        Gun _puppet;
        Mode _currentMode = Mode.idle;
        Timer _searchForPlayerTimer;
        Vector3 _targetPos;
        Vector3 _targetOldPos;
        bool _puppeted = false;
        #endregion
        #region Properties
        public Gun Puppet { get => _puppet; set => _puppet = value; }
        #endregion
        #region Constructor
        public Gun(Game game, Camera camera, GameLogic gameLogic, Vector3 position,
            Gun master = null) : base(game, camera)
        {
            _logic = gameLogic;
            Position = position;
            PO.Rotation.Z = Helper.RandomRadian();
            //Enabled = false;

            _searchForPlayerTimer = new Timer(game, 0.25f);
            _player = gameLogic._player;
            _shot = new Shot(game, camera, gameLogic);

            if (master != null)
            {
                _master = master;
                master.Puppet = this;
                _puppeted = true;
                _searchForPlayerTimer.Enabled = false;
                Rotation = master.Rotation;
            }
        }
        #endregion
        #region Initialize
        public override void Initialize()
        {
            LoadModel("MachineGun");

            base.Initialize();
        }
        #endregion
        #region Update
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            switch (_currentMode)
            {
                case Mode.idle:
                    Idle();
                    break;
                case Mode.search:
                    SearchForPlayer();
                    break;
                case Mode.fire:
                    FireShot();
                    break;
                case Mode.turn:
                    TurnTowardsPlayer();
                    break;
            }
        }
        #endregion
        void Idle()
        {
            if (_searchForPlayerTimer.Elapsed)
            {
                _searchForPlayerTimer.Reset();
                _currentMode = Mode.search;
            }
        }

        void SearchForPlayer() //Do this on move too.
        {
            if (Helper.RandomMinMax(0, 10) > 1)
            {
                if (Vector3.Distance(Position, _player.Position) < 25) //25 works for game.
                {
                    _targetOldPos = _targetPos;
                    _targetPos = _player.Position;
                    _currentMode = Mode.turn;
                    RotationVelocity = Vector3.Zero;
                }
                else
                {
                    _currentMode = Mode.idle;
                }
            }
            else
            {
                _currentMode = Mode.idle;
            }
        }

        void TurnTowardsPlayer()
        {
            PO.RotationAcceleration.Z = Helper.AimAtTargetZ(Position, _targetPos, Rotation.Z, 0.15f);

            if (PO.RotationVelocity.Z > 0.5f)
            {
                PO.RotationVelocity.Z = 0.5f;
                PO.RotationAcceleration.Z = 0;
            }

            if (PO.RotationVelocity.Z < -0.5f)
            {
                PO.RotationVelocity.Z = -0.5f;
                PO.RotationAcceleration.Z = 0;
            }

            float tAngle = Rotation.Z;
            float pAngle = Helper.AngleFromVectorsZ(Position, _targetPos);
            float aDiffernce = 0;

            if (tAngle < pAngle)
            {
                aDiffernce = pAngle - tAngle;
            }
            else
            {
                aDiffernce = tAngle - pAngle;
            }

            if (aDiffernce < 0.25f)
            {
                PO.RotationVelocity.Z = 0;
                PO.RotationAcceleration.Z = 0;
                _currentMode = Mode.fire;
            }

            if (_puppet != null)
            {
                _puppet.Rotation = Rotation;
            }

            if (_puppeted)
            {
                _master.Rotation = Rotation;
            }
        }

        public void FireShot(bool master = true)
        {
            _currentMode = Mode.idle;

            if (_shot.Enabled)
                return;

            if (_puppet != null)
            {
                _puppet.FireShot(false);
            }

            if (_puppeted && master)
            {
                _master.FireShot();
            }

            _shot.Fire(Position, Helper.VelocityFromAngleZ(PO.Rotation.Z, 10));
            _searchForPlayerTimer.Reset();
        }
    }
}
