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
        public JetPlane(Game game, Camera camera, GameLogic gameLogic) : base(game, camera)
        {
            _logicRef = gameLogic;
            _landRef = gameLogic._land;
            _playerRef = gameLogic._player;
            _shot = new Shot(game, camera, gameLogic);
            _searchForPlayerTimer = new Timer(game, 1);
            //Enabled = false;

        }
        #endregion
        #region Initialize
        public override void Initialize()
        {
            Position = new Vector3(-25, 25, 0);


            base.Initialize();

            LoadModel("JetPlane");

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

            if (Position.X > _landRef.BoundingBox.Width)
            {

            }

            base.Update(gameTime);
        }
        #endregion
        #region Public methods
        #endregion
        #region Private/Protected methods
        void CheckEdgeOfMap()
        {

        }

        void Idle()
        {

            if (_searchForPlayerTimer.Elapsed)
                _currentMode = Mode.search;
        }

        void Move()
        {

        }

        void SearchForPlayer() //Do this on move too.
        {
            _searchForPlayerTimer.Reset();

            if (Helper.RandomMinMax(0, 10) > 1)
            {
                if (Vector3.Distance(Position, _playerRef.Position) < 20) //35 works for game.
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
            _shot.Fire(Position, Helper.VelocityFromAngleZ(PO.Rotation.Z, 10));
            _searchForPlayerTimer.Reset();
            _currentMode = Mode.idle;
        }
        #endregion
    }
}
