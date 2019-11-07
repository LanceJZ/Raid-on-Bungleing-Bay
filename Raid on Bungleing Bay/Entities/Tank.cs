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
        Tank _puppet;
        Tank _master;
        Mode _currentMode = Mode.idle;
        Timer _searchForPlayerTimer;
        Vector3 _targetPos;
        Vector3 _targetOldPos;
        List<Vector3> _patrolRoute;
        int _patrolNextWayPoint = 1;
        int _patrolPoint = 1;
        bool _puppeted = false;
        #endregion
        #region Properties
        public Tank Puppet { get => _puppet; set => _puppet = value; }
        #endregion
        #region Constructor
        public Tank(Game game, Camera camera, GameLogic gameLogic, List<Vector3> route,
            Tank master = null) : base(game, camera)
        {
            Enabled = true;
            _logicRef = gameLogic;
            _playerRef = gameLogic._player;
            _shot = new Shot(game, camera, gameLogic);
            _searchForPlayerTimer = new Timer(game, 1);
            Position = route[0];
            _patrolRoute = route;

            if (master != null)
            {
                _master = master;
                _master.Puppet = this;
                _puppeted = true;
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
            if (0.25f > Vector3.Distance(Position, _patrolRoute[_patrolNextWayPoint]))
            {

                if (_patrolNextWayPoint + 1 > _patrolRoute.Count - 1)
                {
                    _patrolPoint = -1;
                }
                else if (_patrolNextWayPoint < 1)
                {
                    _patrolPoint = 1;
                }

                _patrolNextWayPoint += _patrolPoint;
            }

            float angle = Helper.AngleFromVectorsZ(Position, _patrolRoute[_patrolNextWayPoint]);
            PO.Rotation.Z = angle;
            Velocity = Helper.VelocityFromAngleZ(angle, 2.5f);

            if (_puppet != null)
            {
                _puppet.Velocity = Velocity;
            }

            if (_puppeted)
            {
                _master.Velocity = Velocity;
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

        //void MoveTankToStart()
        //{
        //    if (PO.Rotation.Z > (MathHelper.Pi + MathHelper.Pi / 2) + 0.25f ||
        //        PO.Rotation.Z < (MathHelper.Pi + MathHelper.Pi / 2) - 0.25f)
        //    {
        //        PO.RotationVelocity.Z = Helper.AimAtTargetZ(Position, _patrolStart, Rotation.Z, 0.25f);
        //    }
        //    else
        //    {
        //        PO.RotationVelocity.Z = 0;
        //        PO.Rotation.Z = MathHelper.Pi + MathHelper.Pi / 2;

        //        if ((int)_patrolStart.X == (int)_patrolEnd.X)
        //        {
        //            if (Y < _patrolStart.Y)
        //            {
        //                PO.Velocity.Y = 0;
        //                _currentHeading = Heading.toEnd;
        //            }
        //            else
        //            {
        //                PO.Velocity.Y = -2.5f;
        //            }
        //        }
        //        else
        //        {
        //            if (X < _patrolStart.X)
        //            {
        //                PO.Velocity.X = 0;
        //                _currentHeading = Heading.toEnd;
        //            }
        //            else
        //            {
        //                PO.Velocity.X = -2.5f;
        //            }
        //        }
        //    }
        //}

        //void MoveTankToEnd() //Tanks not moving.
        //{
        //    if ((int)_patrolStart.X == (int)_patrolEnd.X)
        //    {
        //        if (PO.Rotation.Z > (MathHelper.Pi / 2) + 0.25f || PO.Rotation.Z < (MathHelper.Pi / 2) - 0.25f)
        //        {
        //            PO.RotationVelocity.Z = Helper.AimAtTargetZ(Position, _patrolEnd, Rotation.Z, 0.25f);
        //        }
        //        else
        //        {
        //            PO.RotationVelocity.Z = 0;
        //            PO.Rotation.Z = MathHelper.Pi / 2;

        //            if ((int)_patrolStart.X == (int)_patrolEnd.X)
        //            {
        //                if (Y > _patrolEnd.Y)
        //                {
        //                    PO.Velocity.Y = 0;
        //                    _currentHeading = Heading.toStart;
        //                }
        //                else
        //                {
        //                    PO.Velocity.Y = 2.5f;
        //                }
        //            }
        //            else
        //            {
        //                if (PO.Rotation.Z > 0 + 0.25f || PO.Rotation.Z < (MathHelper.TwoPi) - 0.25f)
        //                {
        //                    PO.RotationVelocity.Z = Helper.AimAtTargetZ(Position, _patrolEnd, Rotation.Z, 0.25f);
        //                }
        //                else
        //                {
        //                    PO.RotationVelocity.Z = 0;
        //                    PO.Rotation.Z = 0;

        //                    if (X > _patrolEnd.X)
        //                    {
        //                        PO.Velocity.X = 0;
        //                        _currentHeading = Heading.toStart;
        //                    }
        //                    else
        //                    {
        //                        PO.Velocity.X = 2.5f;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

    }
}
