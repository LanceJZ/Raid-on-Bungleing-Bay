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
    class Radar : ModelEntity
    {
        #region Fields
        GameLogic LogicRef;

        #endregion
        #region Properties

        #endregion
        #region Constructor
        public Radar(Game game, Camera camera, GameLogic gameLogic) : base(game, camera)
        {
            LogicRef = gameLogic;
            //Enabled = false;

        }
        #endregion
        #region Initialize
        public override void Initialize()
        {
            base.Initialize();

            Position = new Vector3(13, -22f, 0);
            PO.RotationVelocity.Z = 1;

            LoadModel("Radar");
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
