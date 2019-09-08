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
    class Machinegun : ModelEntity
    {
        #region Fields
        GameLogic LogicRef;
        Shot _shot;
        Player _playerRef;
        Mode _currentMode = Mode.idle;
        Timer _searchForPlayerTimer;
        Vector3 _targetPos;
        Vector3 _targetOldPos;

        #endregion
        #region Properties

        #endregion
        #region Constructor
        public Machinegun(Game game, Camera camera, GameLogic gameLogic) : base(game, camera)
        {
            LogicRef = gameLogic;
            //Enabled = false;

            _searchForPlayerTimer = new Timer(game, 0.25f);
            _playerRef = gameLogic._player;
            _shot = new Shot(game, camera, gameLogic);

        }
        #endregion
        #region Initialize
        public override void Initialize()
        {
            LoadModel("MachineGun");

            PO.Position.X = -8.25f;
            PO.Position.Y = -12.0f;

            base.Initialize();
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
        void Idle()
        {
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
        }

        void FireShot()
        {
            _shot.Fire(Position, Helper.VelocityFromAngleZ(PO.Rotation.Z, 10));
            _searchForPlayerTimer.Reset();
            _currentMode = Mode.idle;
        }
    }
}
