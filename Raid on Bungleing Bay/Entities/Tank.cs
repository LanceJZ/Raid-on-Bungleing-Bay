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
    enum Mode
    {
        idle,
        search,
        move,
        turn,
        fire
    }

    class Tank : ModelEntity
    {
        #region Fields
        GameLogic _logicRef;
        Shot _shot;
        Player _playerRef;
        Tank _puppet;
        Tank _master;
        Mode _currentMode = Mode.idle;
        Timer _searchForPlayerTimer;
        Vector3 _targetPos;
        Vector3 _targetOldPos;
        Vector3 _puppetOffset;
        List<Vector3> _patrolRoute;
        int _patrolNextWayPoint = 1;
        int _patrolPoint = 1;
        bool _puppeted = false;
        #endregion
        #region Properties
        public Tank Puppet { get => _puppet; set => _puppet = value; }
        public List<Vector3> PatrolRoute { get => _patrolRoute; set => _patrolRoute = value; }
        public Vector3 PuppetOffset { get => _puppetOffset; set => _puppetOffset = value; }
        #endregion
        #region Constructor
        public Tank(Game game, Camera camera, GameLogic gameLogic, List<Vector3> route,
            Vector3 puppetOffset, Tank master = null) : base(game, camera)
        {
            Enabled = true;
            _logicRef = gameLogic;
            _playerRef = gameLogic._player;
            _shot = new Shot(game, camera, gameLogic);
            _searchForPlayerTimer = new Timer(game, 1);

            if (master != null)
            {
                _master = master;
                _master.Puppet = this;
                _puppeted = true;
                Position = route[0] + puppetOffset;
                _puppetOffset = puppetOffset;
            }
            else
            {
                Position = route[0];
                PatrolRoute = route;
            }
        }
        #endregion
        #region Initialize
        public override void Initialize()
        {
            base.Initialize();

            LoadModel("Tank");

            float rad = PO.Radius;
        }
        #endregion
        #region Update
        public override void Update(GameTime gameTime)
        {
            switch (_currentMode)
            {
                case Mode.idle:
                    Idle();
                    break;
                case Mode.search:
                    SearchForPlayer();
                    break;
                case Mode.move:
                    Move();
                    break;
                case Mode.fire:
                    FireShot();
                    break;
                case Mode.turn:
                    TurnTowardsPlayer();
                    break;
            }

            base.Update(gameTime);
        }
        #endregion

        void Move()
        {
            if (_puppeted)
                return;

            if (0.25f > Vector3.Distance(Position, PatrolRoute[_patrolNextWayPoint]))
            {

                if (_patrolNextWayPoint + 1 > PatrolRoute.Count - 1)
                {
                    _patrolPoint = -1;
                }
                else if (_patrolNextWayPoint < 1)
                {
                    _patrolPoint = 1;
                }

                _patrolNextWayPoint += _patrolPoint;
            }

            float angle = Helper.AngleFromVectorsZ(Position, PatrolRoute[_patrolNextWayPoint]);
            PO.Rotation.Z = angle;
            Velocity = Helper.VelocityFromAngleZ(angle, 2.5f);

            if (_puppet != null)
            {
                _puppet.Position = Position + _puppet.PuppetOffset;
                _puppet.Rotation = Rotation;
            }

            if (_searchForPlayerTimer.Elapsed)
            {
                SearchForPlayer();
            }
        }

        void Idle()
        {
            Velocity = Vector3.Zero;

            if (_searchForPlayerTimer.Elapsed)
                _currentMode = Mode.search;
        }

        void SearchForPlayer()
        {
            _searchForPlayerTimer.Reset();

            if (Helper.RandomMinMax(0, 10) > 1)
            {
                if (Vector3.Distance(Position, _playerRef.Position) < 15) //25 works for game.
                {
                    _targetOldPos = _targetPos;
                    _targetPos = _playerRef.Position;
                    _currentMode = Mode.turn;
                    Velocity = Vector3.Zero;
                    RotationVelocity = Vector3.Zero;

                    if (_puppet != null)
                    {
                        _puppet.Rotation = Rotation;
                    }
                }
                else
                {
                    _currentMode = Mode.move;
                }
            }
        }

        void TurnTowardsPlayer()//TODO: See if I can get puppet to aim and shoot at player.
        {
            PO.RotationAcceleration.Z = Helper.AimAtTargetZ(Position, _targetPos, Rotation.Z, 0.15f);

            if (PO.RotationVelocity.Z > 0.25f)
            {
                PO.RotationVelocity.Z = 0.25f;
                PO.RotationAcceleration.Z = 0;
            }

            if (PO.RotationVelocity.Z < -0.25f)
            {
                PO.RotationVelocity.Z = -0.25f;
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

            if (aDiffernce < 0.05f)
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

        void FireShot(bool master = true)
        {
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
            _currentMode = Mode.idle;
        }
    }
}
