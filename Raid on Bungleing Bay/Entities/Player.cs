#region Using
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Panther;
#endregion
namespace Raid_on_Bungleing_Bay.Entities
{
    public class Player : ModelEntity
    {
        #region Fields
        ModelEntity _blade;
        ModelEntity _rotor;
        GameLogic _logicRef;
        Land _landRef;
        #endregion
        #region Properties

        #endregion
        #region Constructor
        public Player(Game game, Camera camera, GameLogic gameLogic) : base(game, camera)
        {
            Enabled = true;
            _logicRef = gameLogic;
            _landRef = gameLogic._land;
            _blade = new ModelEntity(game, camera);
            _rotor = new ModelEntity(game, camera);
        }
        #endregion
        #region Initialize
        public override void Initialize()
        {
            _blade.AddAsChildOf(this);
            _rotor.AddAsChildOf(this);
            PO.Position.Z = 10;
            PO.Rotation.Z = MathHelper.Pi / 2; // Point player up at start.
            //_blade.PO.RotationVelocity.Z = 12;
            _blade.PO.Position.Z = 0.65f;
            _rotor.PO.RotationVelocity.Y = 16;
            _rotor.PO.Position.Y = -0.1f;
            _rotor.PO.Position.X = -2.1f;

            PO.Position.Y = -30f;
            PO.Position.X = 14.5f;

            LoadModel("Player-Body");
            _blade.LoadModel("Player-Blade");
            _rotor.LoadModel("Player-Rotor");

            PO.MapSize = new Vector2(_landRef.BoundingBox.Width, _landRef.BoundingBox.Height);

            base.Initialize();
        }
        #endregion
        #region Update
        public override void Update(GameTime gameTime)
        {
            GetInput();
            _camera.MoveTo(new Vector3(PO.Position.X, PO.Position.Y, _camera.Position.Z));

            _blade.PO.RotationVelocity.Z = 12 - RotationVelocity.Z;
            PO.CheckEdgeOfMap();

            base.Update(gameTime);
        }
        #endregion
        #region Public methods
        #endregion
        #region Private methods
        void GetInput()
        {
            if (Helper.KeyDown(Keys.Up))
            {
                if (MaxVelocity(100, PO.Velocity))
                    PO.Acceleration = Helper.VelocityFromAngleZ(PO.Rotation.Z, 10);
            }
            else if (Helper.KeyDown(Keys.Down))
            {
                if (MaxVelocity(50, PO.Velocity))
                    PO.Acceleration = Helper.VelocityFromAngleZ(PO.Rotation.Z, -6);
            }
            else
            {
                PO.Acceleration = Vector3.Zero;
                PO.Acceleration = -PO.Velocity * 0.75f;
            }

            if (Helper.KeyDown(Keys.Left))
            {
                if (MaxVelocity(MathHelper.Pi, PO.RotationVelocity))
                    PO.RotationAcceleration.Z = 2.5f;

            }
            else if (Helper.KeyDown(Keys.Right))
            {
                if (MaxVelocity(MathHelper.Pi, PO.RotationVelocity))
                    PO.RotationAcceleration.Z = -2.5f;
            }
            else
            {
                PO.RotationAcceleration.Z = 0;
                PO.RotationAcceleration = -PO.RotationVelocity * 1.75f;
            }
        }

        bool MaxVelocity(float max, Vector3 velocity)
        {
            if (Math.Abs(velocity.X) + Math.Abs(velocity.Y) < max)
                return true;
            else return false;
        }
        #endregion
    }
}
