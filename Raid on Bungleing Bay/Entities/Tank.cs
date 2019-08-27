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

    enum Heading
    {
        toStart,
        toEnd
    }

    class Tank : ModelEntity
    {
        #region Fields
        Shot _shot;
        GameLogic _logicRef;
        Player _playerRef;
        Mode _currentMode = Mode.idle;
        Heading _currentHeading = Heading.toEnd;
        Timer _searchForPlayerTimer;
        Vector3 _targetPos;
        Vector3 _targetOldPos;
        Vector3 _patrolStart;
        Vector3 _patrolEnd;
        #endregion
        #region Properties

        #endregion
        #region Constructor
        public Tank(Game game, Camera camera, GameLogic gameLogic) : base(game, camera)
        {
            Enabled = true;
            _patrolStart = PO.Position;
            _patrolEnd = PO.Position + new Vector3(0, -10, 0);
            _logicRef = gameLogic;
            _playerRef = gameLogic.ThePlayer;
            _shot = new Shot(game, camera, gameLogic);
            _searchForPlayerTimer = new Timer(game, 3);
        }
        #endregion
        #region Initialize-Load-BeginRun
        public override void Initialize()
        {
            PO.Position.X = 27;
            PO.Position.Y = -22;
            PO.Rotation.Z = MathHelper.Pi / 2;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            LoadModel("Tank");

            base.LoadContent();
        }

        public override void BeginRun()
        {
            base.BeginRun();

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
                    MoveTank();
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

        void MoveTankToStart()
        {
            if (PO.Rotation.Z != 0)
            {
                PO.RotationVelocity.Z = Helper.AimAtTargetZ(Position, _patrolEnd, Rotation.Z, 0.25f);
            }
            else
            {
                if (Vector3.Distance(Position, _patrolStart) < 0.01)
                {
                    PO.Velocity.Y = 0;
                    _currentHeading = Heading.toEnd;
                }
                else
                {
                    PO.Velocity.Y = 5;
                }
            }
        }

        void MoveTankToEnd()
        {
            if (PO.Rotation.Z != MathHelper.Pi)
            {
                PO.RotationVelocity.Z = Helper.AimAtTargetZ(Position, _patrolEnd, Rotation.Z, 0.25f);
            }
            else
            {
                if (Vector3.Distance(Position, _patrolEnd) < 0.01)
                {
                    PO.Velocity.Y = 0;
                    _currentHeading = Heading.toStart;
                }
                else
                {
                    PO.Velocity.Y = -5;
                }
            }
        }

        void MoveTank()
        {
            switch (_currentHeading)
            {
                case Heading.toEnd:
                    MoveTankToEnd();
                    break;
                case Heading.toStart:
                    MoveTankToStart();
                    break;
            }

            if (_searchForPlayerTimer.Elapsed)
                _currentMode = Mode.search;

        }

        void Idle()
        {
            if (_searchForPlayerTimer.Elapsed)
                _currentMode = Mode.search;
        }

        void SearchForPlayer() //Do this on move too.
        {
            if (Helper.RandomMinMax(0, 10) > 5)
            {
                if (Vector3.Distance(PO.Position, _playerRef.PO.Position) < 25)
                {
                    _targetOldPos = _targetPos;
                    _targetPos = _playerRef.PO.Position;
                    _currentMode = Mode.turn;
                }
            }
            else
            {
                _searchForPlayerTimer.Reset();
                _currentMode = Mode.move;
            }
        }

        void TurnTowardsPlayer()
        {
            PO.RotationVelocity.Z = Helper.AimAtTargetZ(PO.Position, _targetPos,
                PO.Rotation.Z, 0.25f);

            if (PO.RotationVelocity.Z < 0.05f)
            {
                PO.RotationVelocity.Z = 0;
                _currentMode = Mode.fire;
            }
        }

        void FireShot()
        {
            _shot.Fire(PO.Position, Helper.VelocityFromAngleZ(PO.Rotation.Z, 10));
            _searchForPlayerTimer.Reset();
            _currentMode = Mode.idle;
        }
    }
}
