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
        Player _player;
        List<Tank> _tanks;
        #endregion
        #region Properties

        #endregion
        #region Constructor
        public Tanks(Game game, Camera camera, GameLogic gameLogic) : base(game)
        {
            _logic = gameLogic;
            _player = gameLogic._player;
            _camera = camera;
            _tanks = new List<Tank>();

            game.Components.Add(this);
        }
        #endregion
        #region Initialize
        public override void Initialize()
        {
            List<Vector3> pat1 = new List<Vector3>();
            List<Vector3> pat2 = new List<Vector3>();
            List<Vector3> pat3 = new List<Vector3>();

            pat1.Add(new Vector3(29, -12, 1));
            pat1.Add(new Vector3(29, -40, 1));
            pat2.Add(new Vector3(-18, -3, 1));
            pat2.Add(new Vector3(15, -3, 1));

            _tanks.Add(new Tank(Game, _camera, _logic, pat1));
            _tanks.Add(new Tank(Game, _camera, _logic, pat2));
            //_tanks.Add(new Tank(Game, _camera, _logic, new Vector3(-10, -12, 1), new Vector3(-30, -12, 1)));
            //_tanks.Add(new Tank(Game, _camera, _logic, ));
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
