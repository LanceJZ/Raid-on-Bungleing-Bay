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
        GameLogic LogicRef;

        #endregion
        #region Properties

        #endregion
        #region Constructor
        public JetPlane(Game game, Camera camera, GameLogic gameLogic) : base(game, camera)
        {
            LogicRef = gameLogic;
            //Enabled = false;

        }
        #endregion
        #region Initialize-Load
        public override void Initialize()
        {
            Position = new Vector3(-25, 25, 0);


            base.Initialize();
        }

        protected override void LoadContent()
        {
            LoadModel("JetPlane");

            base.LoadContent();
        }
        #endregion
        #region Update
        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }
        #endregion
        #region Public methods
        #endregion
        #region Private/Protected methods
        #endregion
    }
}
