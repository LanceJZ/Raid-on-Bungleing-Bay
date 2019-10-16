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
        GameLogic _logicRef;
        Shot _shot;
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
            _logicRef = gameLogic;
            _playerRef = gameLogic._player;
            _shot = new Shot(game, camera, gameLogic);
            _searchForPlayerTimer = new Timer(game, 1);
        }
        #endregion
        #region Initialize
        public override void Initialize()
        {
            PO.Position.X = 28;
            PO.Position.Y = -38;
            PO.Position.Z = 1;
            PO.Rotation.Z = MathHelper.Pi / 2;
            _patrolStart = Position;
            _patrolEnd = Position;
            _patrolEnd.Y = PO.Position.Y + 22;

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

        void MoveTankToStart()
        {
            if (PO.Rotation.Z > (MathHelper.Pi + MathHelper.Pi / 2) + 0.25f || PO.Rotation.Z < (MathHelper.Pi + MathHelper.Pi / 2) - 0.25f)
            {
                PO.RotationVelocity.Z = Helper.AimAtTargetZ(Position, _patrolStart, Rotation.Z, 0.25f);
            }
            else
            {
                PO.RotationVelocity.Z = 0;
                PO.Rotation.Z = MathHelper.Pi + MathHelper.Pi / 2;


                if (Position.Y < _patrolStart.Y)
                {
                    PO.Velocity.Y = 0;
                    _currentHeading = Heading.toEnd;
                }
                else
                {
                    PO.Velocity.Y = -2.5f;
                }
            }
        }

        void MoveTankToEnd()
        {
            if (PO.Rotation.Z > (MathHelper.Pi / 2) + 0.25f || PO.Rotation.Z < (MathHelper.Pi / 2) - 0.25f)
            {
                PO.RotationVelocity.Z = Helper.AimAtTargetZ(Position, _patrolEnd, Rotation.Z, 0.25f);
            }
            else
            {
                PO.RotationVelocity.Z = 0;
                PO.Rotation.Z = MathHelper.Pi / 2;

                if (Position.Y > _patrolEnd.Y)
                {
                    PO.Velocity.Y = 0;
                    _currentHeading = Heading.toStart;
                }
                else
                {
                    PO.Velocity.Y = 2.5f;
                }
            }
        }

        void Move()
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

        void SearchForPlayer() //Do this on move too.
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
                }
                else
                {
                    _currentMode = Mode.move;
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

            if (aDiffernce < 0.25f)
            {
                PO.RotationVelocity.Z = 0;
                PO.RotationAcceleration.Z = 0;
                _currentMode = Mode.fire;
            }
        }

        void FireShot()
        {
            if (!_shot.Enabled)
                return;

            _shot.Fire(Position, Helper.VelocityFromAngleZ(PO.Rotation.Z, 10));
            _searchForPlayerTimer.Reset();
            _currentMode = Mode.idle;
        }
    }
}
