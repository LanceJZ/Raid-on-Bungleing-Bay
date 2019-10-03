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
    class Factories : GameComponent
    {
        #region Fields
        GameLogic _logic;
        Camera _camera;
        List<Factory> _factories;
        #endregion
        #region Properties

        #endregion
        #region Constructor
        public Factories(Game game, Camera camera, GameLogic gameLogic) : base(game)
        {
            _logic = gameLogic;
            _camera = camera;
            _factories = new List<Factory>();

            game.Components.Add(this);
        }
        #endregion
        #region Initialize
        public override void Initialize()
        {
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(81f - 75.5f, 60f, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(31f - 75.5f, 34f, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(-62.5f, 048f, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(-74.0f, 23.0f, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(-99.5f, 20.0f, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(-76.5f, 43.0f, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(168.5f, 70.0f, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(151.5f, 86.0f, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(185.5f, 78.0f, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(140.5f, 4.0f, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(149.0f, 116.0f - 178.0f, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(144.5f, -83.0f, 2))); //{X:144.4534 Y:-83.08024 Z:10}
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(134.5f, -111.0f, 2))); //{X:134.0355 Y:-111.2313 Z:10}
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(112.5f, -98.0f, 2))); //{X:112.9657 Y:-98.84859 Z:10}
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(112.5f, -78.0f, 2))); //{X:112.1338 Y:-78.10521 Z:10}
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(-1.0f, -9.0f, 2))); //{X:1.270111 Y:-9.708489 Z:10}
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(16.5f, -7.5f, 2))); //{X:16.41779 Y:-7.578918 Z:10}
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(14.5f, -37.5f, 2))); //{X:14.5 Y:-37.29755 Z:10}


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
        #region Private/Protected Methods
        #endregion
    }
}
