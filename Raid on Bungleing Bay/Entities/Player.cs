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
    class Player : ModelEntity
    {
        #region Fields
        ModelEntity blade;
        ModelEntity rotor;
        GameLogic LogicRef;
        #endregion
        #region Properties

        #endregion
        #region Constructor
        public Player(Game game, Camera camera, GameLogic gameLogic) : base(game, camera)
        {
            LogicRef = gameLogic;
            Enabled = true;
            blade = new ModelEntity(game, camera);
            rotor = new ModelEntity(game, camera);
        }
        #endregion
        #region Initialize-Load-BeginRun
        public override void Initialize()
        {
            blade.AddAsChildOf(this);
            rotor.AddAsChildOf(this);
            PO.Position.Z = 10;
            PO.Rotation.Z = MathHelper.Pi / 2;
            //PO.Rotation.Y = MathHelper.Pi / 2;
            blade.PO.RotationVelocity.Z = 13;
            blade.PO.Position.Z = 0.65f;
            rotor.PO.RotationVelocity.Y = 16;
            rotor.PO.Position.Y = -0.1f;
            rotor.PO.Position.X = -2.1f;

            PO.Position.Y = -30f;
            PO.Position.X = 14.5f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            LoadModel("Player-Body");
            blade.LoadModel("Player-Blade");
            rotor.LoadModel("Player-Rotor");

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
            GetInput();
            CameraRef.MoveTo(new Vector3(PO.Position.X, PO.Position.Y, CameraRef.Position.Z));

            base.Update(gameTime);
        }

        void GetInput()
        {
            if (Helper.KeyDown(Keys.Up))
            {
                if (MaxVelocity())
                    PO.Acceleration = Helper.VelocityFromAngleZ(PO.Rotation.Z, 10);
            }
            else if (Helper.KeyDown(Keys.Down))
            {
                if (MaxVelocity())
                    PO.Acceleration = Helper.VelocityFromAngleZ(PO.Rotation.Z, -10);
            }
            else
            {
                PO.Acceleration = Vector3.Zero;
                PO.Acceleration = -PO.Velocity * 0.75f;
            }

            if (Helper.KeyDown(Keys.Left))
            {
                PO.RotationVelocity.Z = MathHelper.Pi;
            }
            else if (Helper.KeyDown(Keys.Right))
            {
                PO.RotationVelocity.Z = -MathHelper.Pi;
            }
            else
            {
                PO.RotationVelocity.Z = 0;
            }
        }

        bool MaxVelocity()
        {
            if (Math.Abs(Velocity.X) + Math.Abs(Velocity.Y) < 500)
                return true;
            else return false;
        }
        #endregion
    }
}
