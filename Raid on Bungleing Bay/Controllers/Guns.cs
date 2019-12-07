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
        List<Gun> _guns;
        List<Gun> _puppetGuns;

        #endregion
        #region Properties

        #endregion
        #region Constructor
        public Guns(Game game, Camera camera, GameLogic gameLogic) : base(game)
        {
            _logic = gameLogic;
            _camera = camera;

            _guns = new List<Gun>();
            _puppetGuns = new List<Gun>();

            game.Components.Add(this);
        }
        #endregion
        #region Initialize
        public override void Initialize()
        {
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(-8.25f, -12, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(-22, -3, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(-30, -15, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(6, -13, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(4, -30, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(-24, 34, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(-24, 45, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(-44, 46, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(-59, 34, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(-95, 61, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(-45, 70, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(34, 62, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(-51, 14, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(-72, 5, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(12, -105, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(-20, -104, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(-51, -56, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(-79, -57, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(94, -93, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(137, 65, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(137, 91, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(158, 74, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(191, 67, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(127, -8, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(131, 12, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(149, -54, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(134, -66, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(98, -87, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(113, -117, 1)));
            _guns.Add(new Gun(Game, _camera, _logic, new Vector3(142, -95, 1)));

            //Puppets.
            foreach (Gun gun in _guns)
            {
                if (gun.Y > 84)
                {
                    _puppetGuns.Add(new Gun(Game, _camera, _logic, new Vector3(gun.X, gun.Y - 250, gun.Z), gun));
                }


                if (gun.Y < -84)
                {
                    _puppetGuns.Add(new Gun(Game, _camera, _logic, new Vector3(gun.X, gun.Y + 250, gun.Z), gun));
                }

                if (gun.X > 145)
                {
                    _puppetGuns.Add(new Gun(Game, _camera, _logic, new Vector3(gun.X - 400, gun.Y, gun.Z), gun));
                }

                if (gun.X < -145)
                {
                    _puppetGuns.Add(new Gun(Game, _camera, _logic, new Vector3(gun.X + 400, gun.Y, gun.Z), gun));
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
    }
}
