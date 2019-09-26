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
    class JetPlane : ModelEntity
    {
        #region Fields
        GameLogic _logicRef;
        Shot _shot;
        Player _playerRef;
        Land _landRef;
        Mode _currentMode = Mode.idle;
        Timer _searchForPlayerTimer;
        Timer _changeCourseTimer;
        Vector3 _targetPos;
        Vector3 _targetOldPos;
        //Vector2 _widthHeight;

        #endregion
        #region Properties

        #endregion
        #region Constructor
        public JetPlane(Game game, Camera camera, GameLogic gameLogic) : base(game, camera)
        {
            _logicRef = gameLogic;
            _landRef = gameLogic._land;
            _playerRef = gameLogic._player;
            _shot = new Shot(game, camera, gameLogic);
            _searchForPlayerTimer = new Timer(game, 1);
            _changeCourseTimer = new Timer(game, 5);

            //Enabled = false;
        }
        #endregion
        #region Initialize
        public override void Initialize()
        {
            Position = new Vector3(-2, -2, 10);


            base.Initialize();

            LoadModel("JetPlane");

            ChangeHeading();

            PO.MapSize = new Vector2(_landRef.BoundingBox.Width, _landRef.BoundingBox.Height);
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

            PO.CheckEdgeOfMap();

            base.Update(gameTime);
        }
        #endregion
        #region Public methods
        #endregion
        #region Private/Protected methods

        void Idle()
        {
            if (_searchForPlayerTimer.Elapsed)
                _currentMode = Mode.search;

            if (_changeCourseTimer.Elapsed)
                ChangeHeading();
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

        }

        void SearchForPlayer() //Do this on move too.
        {
            _searchForPlayerTimer.Reset();

            if (Helper.RandomMinMax(0, 10) > 1)
            {
                if (Vector3.Distance(Position, _playerRef.Position) < 35) //35 works for game.
                {
                    _targetOldPos = _targetPos;
                    _targetPos = _playerRef.Position;
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
            _shot.Fire(Position, Helper.VelocityFromAngleZ(PO.Rotation.Z, 20));
            _searchForPlayerTimer.Reset();
            _currentMode = Mode.idle;
        }
        #endregion
    }
}
