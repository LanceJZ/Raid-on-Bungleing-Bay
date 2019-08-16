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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            LoadModel("Player-Body");
            blade.LoadModel("Player-Blade");
            rotor.LoadModel("Player-Rotor");
            blade.PO.RotationVelocity.Z = 12;
            rotor.PO.RotationVelocity.X = 12;

            base.LoadContent();
        }

        public override void BeginRun()
        {
            base.BeginRun();
            blade.PO.Position.Z = 6.5f;
            rotor.PO.Position.Z = 1;
            rotor.PO.Position.Y = -20;
        }
        #endregion
        #region Update
        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
        }
        #endregion
    }
}
