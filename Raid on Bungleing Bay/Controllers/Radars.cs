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
    class Radars : GameComponent
    {
        #region Fields
        GameLogic _logic;
        Camera _camera;
        List<Radar> _radars;
        #endregion
        #region Properties

        #endregion
        #region Constructor
        public Radars(Game game, Camera camera, GameLogic gameLogic) : base(game)
        {
            _logic = gameLogic;
            _camera = camera;
            _radars = new List<Radar>();

            game.Components.Add(this);
        }
        #endregion
        #region Initialize-Load-BeginRun
        public override void Initialize()
        {
            _radars.Add(new Radar(Game, _camera, _logic, new Vector3(17, -19, 1)));
            _radars.Add(new Radar(Game, _camera, _logic, new Vector3(-91, 41, 1)));
            _radars.Add(new Radar(Game, _camera, _logic, new Vector3(-8, 54, 1)));
            _radars.Add(new Radar(Game, _camera, _logic, new Vector3(-114, -93, 1)));
            _radars.Add(new Radar(Game, _camera, _logic, new Vector3(-63, -59, 1)));
            _radars.Add(new Radar(Game, _camera, _logic, new Vector3(127, -67, 1)));
            _radars.Add(new Radar(Game, _camera, _logic, new Vector3(92, -107, 1)));
            _radars.Add(new Radar(Game, _camera, _logic, new Vector3(135, -6, 1)));
            _radars.Add(new Radar(Game, _camera, _logic, new Vector3(195, 77, 1)));
            _radars.Add(new Radar(Game, _camera, _logic, new Vector3(136, 79, 1)));
            _radars.Add(new Radar(Game, _camera, _logic, new Vector3(-3, -107, 1)));
            List<Radar> mirrorFactories = new List<Radar>();
            //Mirrors.
            foreach (Radar radar in _radars)
            {
                if (radar.Y > 80)
                {
                    mirrorFactories.Add(new Radar(Game, _camera, _logic, new Vector3(radar.X, radar.Y - 250, radar.Z), radar));
                }


                if (radar.Y < -80)
                {
                    mirrorFactories.Add(new Radar(Game, _camera, _logic, new Vector3(radar.X, radar.Y + 250, radar.Z), radar));
                }

                if (radar.X > 126)
                {
                    mirrorFactories.Add(new Radar(Game, _camera, _logic, new Vector3(radar.X - 400, radar.Y, radar.Z), radar));
                }

                if (radar.X < -126)
                {
                    mirrorFactories.Add(new Radar(Game, _camera, _logic, new Vector3(radar.X + 400, radar.Y, radar.Z), radar));
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
