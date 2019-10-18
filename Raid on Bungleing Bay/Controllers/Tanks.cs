using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Linq;
using System;
using Panther;
using Raid_on_Bungleing_Bay.Entities;

namespace Raid_on_Bungleing_Bay.Controllers
{
    class Tanks : GameComponent
    {
        #region Fields
        GameLogic _logic;
        Camera _camera;
        List<Tank> _tanks;
        #endregion
        #region Properties

        #endregion
        #region Constructor
        public Tanks(Game game, Camera camera, GameLogic gameLogic) : base(game)
        {
            _logic = gameLogic;
            _camera = camera;
            _tanks = new List<Tank>();

            game.Components.Add(this);
        }
        #endregion
        #region Initialize
        public override void Initialize()
        {

            base.Initialize();
        }
        #endregion
        #region Update
        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }
        #endregion
        #region Public Methods
        #endregion
        #region  Private/Protected Methods
        #endregion
    }
}
