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
    class Guns : GameComponent
    {
        #region Fields
        GameLogic _logic;
        Camera _camera;
        List<Entities.Machinegun> _guns;

        #endregion
        #region Properties

        #endregion
        #region Constructor
        public Guns(Game game, Camera camera, GameLogic gameLogic) : base(game)
        {
            _logic = gameLogic;
            _camera = camera;

            _guns = new List<Entities.Machinegun>();

            game.Components.Add(this);
        }
        #endregion
        #region Initialize
        public override void Initialize()
        {
            _guns.Add(new Machinegun(Game, _camera, _logic, new Vector3(-8.25f, -12, 1)));
            _guns.Add(new Machinegun(Game, _camera, _logic, new Vector3(-22, -3, 1)));
            _guns.Add(new Machinegun(Game, _camera, _logic, new Vector3(-30, -15, 1)));
            _guns.Add(new Machinegun(Game, _camera, _logic, new Vector3(6, -13, 1)));
            _guns.Add(new Machinegun(Game, _camera, _logic, new Vector3(4, -30, 1)));
            _guns.Add(new Machinegun(Game, _camera, _logic, new Vector3(-24, 34, 1)));
            _guns.Add(new Machinegun(Game, _camera, _logic, new Vector3(-24, 45, 1)));
            _guns.Add(new Machinegun(Game, _camera, _logic, new Vector3(-44, 46, 1)));
            _guns.Add(new Machinegun(Game, _camera, _logic, new Vector3(-59, 34, 1)));
            _guns.Add(new Machinegun(Game, _camera, _logic, new Vector3(-95, 61, 1)));
            _guns.Add(new Machinegun(Game, _camera, _logic, new Vector3(-45, 70, 1)));
            _guns.Add(new Machinegun(Game, _camera, _logic, new Vector3(34, 62, 1)));
            _guns.Add(new Machinegun(Game, _camera, _logic, new Vector3(-51, 14, 1)));
            _guns.Add(new Machinegun(Game, _camera, _logic, new Vector3(-72, 5, 1)));
            _guns.Add(new Machinegun(Game, _camera, _logic, new Vector3(12, 145, 1)));
            _guns.Add(new Machinegun(Game, _camera, _logic, new Vector3(-20, 144, 1)));
            _guns.Add(new Machinegun(Game, _camera, _logic, new Vector3(-51, 194, 1)));
            _guns.Add(new Machinegun(Game, _camera, _logic, new Vector3(-79, 195, 1)));
            _guns.Add(new Machinegun(Game, _camera, _logic, new Vector3(103, 183, 1)));
            _guns.Add(new Machinegun(Game, _camera, _logic, new Vector3(129, 171, 1)));
            _guns.Add(new Machinegun(Game, _camera, _logic, new Vector3(94, 157, 1)));
            _guns.Add(new Machinegun(Game, _camera, _logic, new Vector3(86, 178, 1)));
            _guns.Add(new Machinegun(Game, _camera, _logic, new Vector3(58, 185, 1)));

            base.Initialize();
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
