﻿#region Using
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
    class JetPlane : ModelEntity
    {
        #region Fields
        GameLogic _logic;
        Shot _shot;
        Player _player;
        Land _land;
        JetPlane _puppetX;
        JetPlane _puppetY;
        Mode _currentMode = Mode.idle;
        Timer _searchForPlayerTimer;
        Timer _changeCourseTimer;
        Vector3 _targetPos;
        Vector3 _targetOldPos;
        bool _puppet;

        internal JetPlane PuppetX { get => _puppetX; set => _puppetX = value; }
        internal JetPlane PuppetY { get => _puppetY; set => _puppetY = value; }
        #endregion
        #region Properties

        #endregion
        #region Constructor
        public JetPlane(Game game, Camera camera, GameLogic gameLogic, bool puppet = false) : base(game, camera)
        {
            _logic = gameLogic;
            _land = gameLogic._land;
            _logic = gameLogic;
            _player = gameLogic._player;
            _shot = new Shot(game, camera, gameLogic);
            _searchForPlayerTimer = new Timer(game, 1);
            _changeCourseTimer = new Timer(game, 5);
            _puppet = puppet;
            PO.MapSize = new Vector2(_land.BoundingBox.Width, _land.BoundingBox.Height);
        }
        #endregion
        #region Initialize
        public override void Initialize()
        {

            base.Initialize();

            LoadModel("JetPlane");

            if (!_puppet)
                return;

            _puppetX.Enabled = false;
            _puppetY.Enabled = false;
        }
        #endregion
        #region Update
        public override void Update(GameTime gameTime)
        {
            if (_puppet)
                return;

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

            PO.CheckEdgeOfMap();

            Puppets();

            base.Update(gameTime);
        }
        #endregion
        #region Public methods
        public override void Spawn(Vector3 position, Vector3 rotation)
        {
            _changeCourseTimer.Reset();
            _searchForPlayerTimer.Reset();

            base.Spawn(position, rotation);
        }
        #endregion
        #region Private/Protected methods
        void Puppets()
        {
            if (_puppetX == null)
                return;

            if (_puppetY == null)
                return;

            if (Y > 86)
            {
                _puppetY.PO.Position.Y = PO.Position.Y + 250;
                _puppetY.PO.Position.Z = 10;
                _puppetY.Enabled = true;
            }
            else if (Y < -86)
            {
                _puppetY.PO.Position.Y = PO.Position.Y - 250;
                _puppetY.PO.Position.Z = 10;
                _puppetY.Enabled = true;
            }
            else
            {
                _puppetY.Enabled = false;
            }

            if (X > 100)
            {
                _puppetX.PO.Position.X = PO.Position.X + 400;
                _puppetX.PO.Position.Z = 10;
                _puppetX.Enabled = true;
            }
            else if (X < 100)
            {
                _puppetX.PO.Position.X = PO.Position.X - 400;
                _puppetX.PO.Position.Z = 10;
                _puppetX.Enabled = true;
            }
            else
            {
                _puppetX.Enabled = false;
            }
        }

        void Idle()
        {
            if (_changeCourseTimer.Elapsed)
            {
                if (Velocity.Length() < 1)
                {
                    Velocity = Helper.VelocityFromAngleZ(Rotation.Z, 10);
                    _changeCourseTimer.Reset();
                    return;
                }

                PO.Position.Z = 10;
                ChangeHeading();
            }
        }

        void ChangeHeading()
        {
            _changeCourseTimer.Reset();

            float heading = Helper.Radian;
            PO.Rotation.Z = heading;
            Velocity = Helper.VelocityFromAngleZ(heading, 15);
        }

        void Move()
        {
            if (_searchForPlayerTimer.Elapsed)
                _currentMode = Mode.search;
        }

        void SearchForPlayer() //Do this on move too.
        {
            _searchForPlayerTimer.Reset();

            if (Helper.RandomMinMax(0, 10) > 1)
            {
                if (Vector3.Distance(Position, _player.Position) < 35) //35 works for game.
                {
                    _targetOldPos = _targetPos;
                    _targetPos = _player.Position;
                    _currentMode = Mode.turn;
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

            if (PO.RotationVelocity.Z > 0.75f)
            {
                PO.RotationVelocity.Z = 0.75f;
                PO.RotationAcceleration.Z = 0;
            }

            if (PO.RotationVelocity.Z < -0.75f)
            {
                PO.RotationVelocity.Z = -0.75f;
                PO.RotationAcceleration.Z = 0;
            }

            float tAngle = Rotation.Z;
            float pAngle = Helper.AngleFromVectorsZ(Position, _targetPos);
            float aDiffernce;

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

            Velocity = Helper.VelocityFromAngleZ(Rotation.Z, 15);
        }

        void FireShot()
        {
            if (!_shot.Enabled)
                return;

            _shot.Fire(Position, Helper.VelocityFromAngleZ(PO.Rotation.Z, 20));
            _searchForPlayerTimer.Reset();
            _currentMode = Mode.idle;
        }
        #endregion
    }
}
