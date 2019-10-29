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
        public class Waypoint
        {
            public List<Vector3> points = new List<Vector3>();
        }
        #region Initialize
        public override void Initialize()
        {
            List<Waypoint> waypoints = new List<Waypoint>();

            for(int i = 0; i < 5; i++)
            {
                waypoints.Add(new Waypoint());
            }

            waypoints[0].points.Add(new Vector3(29, -12, 1));
            waypoints[0].points.Add(new Vector3(29, -40, 1));
            waypoints[1].points.Add(new Vector3(-18, -3, 1));
            waypoints[1].points.Add(new Vector3(15, -3, 1));
            waypoints[2].points.Add(new Vector3(-10, -12, 1));
            waypoints[2].points.Add(new Vector3(-30, -12, 1));
            waypoints[3].points.Add(new Vector3(-95, -93, 1));
            waypoints[3].points.Add(new Vector3(-81, -85, 1));
            waypoints[3].points.Add(new Vector3(-80, -74, 1));
            waypoints[3].points.Add(new Vector3(-72.5f, -72, 1));
            waypoints[3].points.Add(new Vector3(-73, -70, 1));
            waypoints[3].points.Add(new Vector3(-72, -63, 1));
            waypoints[3].points.Add(new Vector3(-72, -55, 1));
            waypoints[3].points.Add(new Vector3(-57, -55, 1));
            waypoints[4].points.Add(new Vector3(134, 17, 1));
            waypoints[4].points.Add(new Vector3(145, 10, 1));
            waypoints[4].points.Add(new Vector3(145, 2, 1));
            waypoints[4].points.Add(new Vector3(157, 2, 1));
            waypoints[4].points.Add(new Vector3(157, -2, 1));
            waypoints[4].points.Add(new Vector3(138, -2, 1));
            waypoints[4].points.Add(new Vector3(149, -13, 1));
            waypoints[4].points.Add(new Vector3(129, -17, 1));

            //waypoints[4].points.Add(new );

            for (int i = 0; i < waypoints.Count; i++)
            {
                _tanks.Add(new Tank(Game, _camera, _logic, waypoints[i].points));
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
        #region  Private/Protected Methods
        #endregion
    }
}
