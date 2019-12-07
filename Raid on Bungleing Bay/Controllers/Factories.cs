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
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(-62.5f, 48f, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(-74.0f, 23.0f, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(-99.5f, 20.0f, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(-76.5f, 43.0f, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(168.5f, 70.0f, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(151.5f, 86.0f, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(185.5f, 78.0f, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(140.5f, 4.0f, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(149.0f, -62, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(144.5f, -83.0f, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(134.5f, -111.0f, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(112.5f, -98.0f, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(112.5f, -78.0f, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(-1.0f, -9.0f, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(16.5f, -7.5f, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(14.5f, -37.5f, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(-46, -67, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(-69, -70, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(-77, -68, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(-95, -80.5f, 2)));
            _factories.Add(new Factory(Game, _camera, _logic, new Vector3(-150, -80, 2)));

            List<Factory> mirrorFactories = new List<Factory>();
            //Mirrors.
            foreach(Factory factory in _factories)
            {
                if (factory.Y > 84)
                {
                    mirrorFactories.Add(new Factory(Game, _camera, _logic, new Vector3(factory.X, factory.Y - 250, factory.Z), factory));
                }


                if (factory.Y < -84)
                {
                    mirrorFactories.Add(new Factory(Game, _camera, _logic, new Vector3(factory.X, factory.Y + 250, factory.Z), factory));
                }

                if (factory.X > 145)
                {
                    mirrorFactories.Add(new Factory(Game, _camera, _logic, new Vector3(factory.X - 400, factory.Y, factory.Z), factory));
                }

                if (factory.X < -145)
                {
                    mirrorFactories.Add(new Factory(Game, _camera, _logic, new Vector3(factory.X + 400, factory.Y, factory.Z), factory));
                }
            }

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
