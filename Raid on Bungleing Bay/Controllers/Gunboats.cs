using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Linq;
using System;
using Panther;
using Raid_on_Bungleing_Bay.Entities;

//Ctrl M, Ctrl L to view regions.
namespace Raid_on_Bungleing_Bay.Controllers
{
    class Gunboats : GameComponent
    {
        #region Fields
        GameLogic _logic;
        Camera _camera;
        Gunboat gunboat;

        #endregion
        #region Properties

        #endregion
        #region Constructor
        public Gunboats(Game game, Camera camera, GameLogic gameLogic) : base(game)
        {
            _logic = gameLogic;
            _camera = camera;

            List<Vector3> waypoints;
            waypoints = new List<Vector3>();
            waypoints.Add(new Vector3(0, 5, 0));
            waypoints.Add(new Vector3(0, 20, 0));
            waypoints.Add(new Vector3(20, 20, 0));
            waypoints.Add(new Vector3(20, 5, 0));

            gunboat = new Gunboat(game, camera, gameLogic, waypoints);

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
