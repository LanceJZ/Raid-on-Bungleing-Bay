﻿#region Using
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System;
#endregion
namespace Panther
{
    public class Cube : ModelEntity
    {
        #region Fields
        #endregion
        #region Properties

        #endregion
        #region Constructor
        public Cube(Game game, Camera camera) : base(game, camera)
        {

        }
        #endregion
        #region Initialize
        public override void Initialize()
        {
            base.Initialize();
            LoadModel("Core/Cube");
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
