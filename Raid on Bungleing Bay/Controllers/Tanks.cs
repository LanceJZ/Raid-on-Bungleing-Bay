﻿using Microsoft.Xna.Framework;
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
        List<Tank> _puppetTanks;
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
            _puppetTanks = new List<Tank>();

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
            List<Waypoint> tankWaypoints = new List<Waypoint>();

            for(int i = 0; i < 22; i++)
            {
                tankWaypoints.Add(new Waypoint());
            }

            tankWaypoints[0].points.Add(new Vector3(29, -12, 1));
            tankWaypoints[0].points.Add(new Vector3(29, -40, 1));
            tankWaypoints[1].points.Add(new Vector3(-18, -3, 1));
            tankWaypoints[1].points.Add(new Vector3(15, -3, 1));
            tankWaypoints[2].points.Add(new Vector3(-10, -12, 1));
            tankWaypoints[2].points.Add(new Vector3(-30, -12, 1));
            tankWaypoints[3].points.Add(new Vector3(-95, -93, 1));
            tankWaypoints[3].points.Add(new Vector3(-81, -85, 1));
            tankWaypoints[3].points.Add(new Vector3(-80, -74, 1));
            tankWaypoints[3].points.Add(new Vector3(-72.5f, -72, 1));
            tankWaypoints[3].points.Add(new Vector3(-73, -70, 1));
            tankWaypoints[3].points.Add(new Vector3(-72, -63, 1));
            tankWaypoints[3].points.Add(new Vector3(-72, -55, 1));
            tankWaypoints[3].points.Add(new Vector3(-57, -55, 1));
            tankWaypoints[4].points.Add(new Vector3(134, 17, 1));
            tankWaypoints[4].points.Add(new Vector3(145, 10, 1));
            tankWaypoints[4].points.Add(new Vector3(145, 2, 1));
            tankWaypoints[4].points.Add(new Vector3(157, 2, 1));
            tankWaypoints[4].points.Add(new Vector3(157, -2, 1));
            tankWaypoints[4].points.Add(new Vector3(138, -2, 1));
            tankWaypoints[4].points.Add(new Vector3(149, -13, 1));
            tankWaypoints[4].points.Add(new Vector3(129, -17, 1));
            tankWaypoints[5].points.Add(new Vector3(99, -114, 1));
            tankWaypoints[5].points.Add(new Vector3(88, -114, 1));
            tankWaypoints[5].points.Add(new Vector3(88, -90, 1));
            tankWaypoints[5].points.Add(new Vector3(151, -90, 1));
            tankWaypoints[6].points.Add(new Vector3(120, -102, 1));
            tankWaypoints[6].points.Add(new Vector3(120, -111, 1));
            tankWaypoints[6].points.Add(new Vector3(91, -111, 1));
            tankWaypoints[6].points.Add(new Vector3(100, -111, 1));
            tankWaypoints[6].points.Add(new Vector3(100, -94, 1));
            tankWaypoints[6].points.Add(new Vector3(131, -94, 1));
            tankWaypoints[7].points.Add(new Vector3(100, -116, 1));
            tankWaypoints[7].points.Add(new Vector3(100, -119, 1));
            tankWaypoints[7].points.Add(new Vector3(109, -119, 1));
            tankWaypoints[7].points.Add(new Vector3(123, -119, 1));
            tankWaypoints[7].points.Add(new Vector3(123, -108, 1));
            tankWaypoints[8].points.Add(new Vector3(153, -87, 1));
            tankWaypoints[8].points.Add(new Vector3(153, -74, 1));
            tankWaypoints[8].points.Add(new Vector3(130, -74, 1));
            tankWaypoints[8].points.Add(new Vector3(130, -79, 1));
            tankWaypoints[8].points.Add(new Vector3(135, -83, 1));
            tankWaypoints[8].points.Add(new Vector3(132, -84, 1));
            tankWaypoints[8].points.Add(new Vector3(132, -87, 1));
            tankWaypoints[8].points.Add(new Vector3(141, -87, 1));
            tankWaypoints[8].points.Add(new Vector3(140, -84, 1));
            tankWaypoints[8].points.Add(new Vector3(134, -84, 1));
            tankWaypoints[9].points.Add(new Vector3(126, -77, 1));
            tankWaypoints[9].points.Add(new Vector3(105, -66, 1));
            tankWaypoints[9].points.Add(new Vector3(105, -65, 1));
            tankWaypoints[9].points.Add(new Vector3(87, -65, 1));
            tankWaypoints[9].points.Add(new Vector3(87, -80, 1));
            tankWaypoints[9].points.Add(new Vector3(93, -77, 1));
            tankWaypoints[9].points.Add(new Vector3(97, -82, 1));
            tankWaypoints[10].points.Add(new Vector3(119, -97, 1));
            tankWaypoints[10].points.Add(new Vector3(128, -102, 1));
            tankWaypoints[10].points.Add(new Vector3(132, -102, 1));
            tankWaypoints[10].points.Add(new Vector3(132, -95, 1));
            tankWaypoints[10].points.Add(new Vector3(132, -101, 1));
            tankWaypoints[10].points.Add(new Vector3(156, -101, 1));
            tankWaypoints[10].points.Add(new Vector3(156, -97, 1));
            tankWaypoints[10].points.Add(new Vector3(146, -97, 1));
            tankWaypoints[11].points.Add(new Vector3(171, 56, 1));
            tankWaypoints[11].points.Add(new Vector3(124, 56, 1));
            tankWaypoints[11].points.Add(new Vector3(127, 56, 1));
            tankWaypoints[11].points.Add(new Vector3(127, 66, 1));
            tankWaypoints[11].points.Add(new Vector3(136, 72, 1));
            tankWaypoints[11].points.Add(new Vector3(139, 72, 1));
            tankWaypoints[12].points.Add(new Vector3(130, 62, 1));
            tankWaypoints[12].points.Add(new Vector3(164, 62, 1));
            tankWaypoints[12].points.Add(new Vector3(164, 58, 1));
            tankWaypoints[12].points.Add(new Vector3(164, 75, 1));
            tankWaypoints[12].points.Add(new Vector3(175, 75, 1));
            tankWaypoints[12].points.Add(new Vector3(175, 84, 1));
            tankWaypoints[12].points.Add(new Vector3(182, 84, 1));
            tankWaypoints[13].points.Add(new Vector3(183, 61, 1));
            tankWaypoints[13].points.Add(new Vector3(191, 61, 1));
            tankWaypoints[13].points.Add(new Vector3(191, 64, 1));
            tankWaypoints[13].points.Add(new Vector3(182, 72, 1));
            tankWaypoints[13].points.Add(new Vector3(192, 74, 1));
            tankWaypoints[13].points.Add(new Vector3(192, 84, 1));
            tankWaypoints[13].points.Add(new Vector3(187, 85, 1));
            tankWaypoints[13].points.Add(new Vector3(169, 96, 1));
            tankWaypoints[14].points.Add(new Vector3(166, 96, 1));
            tankWaypoints[14].points.Add(new Vector3(142, 96, 1));
            tankWaypoints[14].points.Add(new Vector3(126, 95, 1));
            tankWaypoints[14].points.Add(new Vector3(126, 87, 1));
            tankWaypoints[14].points.Add(new Vector3(134, 89, 1));
            tankWaypoints[14].points.Add(new Vector3(136, 88, 1));
            tankWaypoints[14].points.Add(new Vector3(141, 87, 1));
            tankWaypoints[14].points.Add(new Vector3(144, 90, 1));
            tankWaypoints[14].points.Add(new Vector3(163, 90, 1));
            tankWaypoints[14].points.Add(new Vector3(156, 90, 1));
            tankWaypoints[14].points.Add(new Vector3(156, 77, 1));
            tankWaypoints[15].points.Add(new Vector3(7, 70, 1));
            tankWaypoints[15].points.Add(new Vector3(18, 70, 1));
            tankWaypoints[15].points.Add(new Vector3(28, 64, 1));
            tankWaypoints[15].points.Add(new Vector3(28, 59, 1));
            tankWaypoints[15].points.Add(new Vector3(23, 59, 1));
            tankWaypoints[15].points.Add(new Vector3(19, 57, 1));
            tankWaypoints[15].points.Add(new Vector3(16, 56, 1));
            tankWaypoints[15].points.Add(new Vector3(2, 54, 1));
            tankWaypoints[15].points.Add(new Vector3(-5, 54, 1));
            tankWaypoints[15].points.Add(new Vector3(-5, 58, 1));
            tankWaypoints[15].points.Add(new Vector3(-7, 58, 1));
            tankWaypoints[15].points.Add(new Vector3(-7, 64, 1));
            tankWaypoints[15].points.Add(new Vector3(-13, 68, 1));
            tankWaypoints[15].points.Add(new Vector3(-13, 71, 1));
            tankWaypoints[16].points.Add(new Vector3(-30, 67, 1));
            tankWaypoints[16].points.Add(new Vector3(-36, 67, 1));
            tankWaypoints[16].points.Add(new Vector3(-36, 58, 1));
            tankWaypoints[16].points.Add(new Vector3(-14, 58, 1));
            tankWaypoints[16].points.Add(new Vector3(-14, 54, 1));
            tankWaypoints[16].points.Add(new Vector3(-53, 54, 1));
            tankWaypoints[16].points.Add(new Vector3(-53, 58, 1));
            tankWaypoints[16].points.Add(new Vector3(-35, 58, 1));
            tankWaypoints[16].points.Add(new Vector3(-35, 53, 1));
            tankWaypoints[17].points.Add(new Vector3(-55, 48, 1));
            tankWaypoints[17].points.Add(new Vector3(-23, 48, 1));
            tankWaypoints[17].points.Add(new Vector3(-15, 45, 1));
            tankWaypoints[17].points.Add(new Vector3(-15, 39, 1));
            tankWaypoints[17].points.Add(new Vector3(-8, 39, 1));
            tankWaypoints[17].points.Add(new Vector3(-12, 35, 1));
            tankWaypoints[17].points.Add(new Vector3(-15, 35, 1));
            tankWaypoints[17].points.Add(new Vector3(-15, 32, 1));
            tankWaypoints[17].points.Add(new Vector3(-14, 39, 1));
            tankWaypoints[17].points.Add(new Vector3(-28, 39, 1));
            tankWaypoints[17].points.Add(new Vector3(-28, 36, 1));
            tankWaypoints[17].points.Add(new Vector3(-36, 36, 1));
            tankWaypoints[17].points.Add(new Vector3(-36, 49, 1));
            tankWaypoints[17].points.Add(new Vector3(-23, 49, 1));
            tankWaypoints[17].points.Add(new Vector3(-15, 46, 1));
            tankWaypoints[17].points.Add(new Vector3(-15, 44, 1));
            tankWaypoints[17].points.Add(new Vector3(-19, 44, 1));
            tankWaypoints[18].points.Add(new Vector3(-49, 31, 1));
            tankWaypoints[18].points.Add(new Vector3(-69, 31, 1));
            tankWaypoints[18].points.Add(new Vector3(-69, 23, 1));
            tankWaypoints[18].points.Add(new Vector3(-56, 23, 1));
            tankWaypoints[18].points.Add(new Vector3(-72, 12, 1));
            tankWaypoints[18].points.Add(new Vector3(-78, 12, 1));
            tankWaypoints[19].points.Add(new Vector3(-78, 8, 1));
            tankWaypoints[19].points.Add(new Vector3(-81, 8, 1));
            tankWaypoints[19].points.Add(new Vector3(-82, 8, 1));
            tankWaypoints[19].points.Add(new Vector3(-82, 33, 1));
            tankWaypoints[19].points.Add(new Vector3(-86, 33, 1));
            tankWaypoints[19].points.Add(new Vector3(-86, 20, 1));
            tankWaypoints[19].points.Add(new Vector3(-98, 13, 1));
            tankWaypoints[19].points.Add(new Vector3(-92, 9, 1));
            tankWaypoints[19].points.Add(new Vector3(-85, 9, 1));
            tankWaypoints[19].points.Add(new Vector3(-84, 11, 1));
            tankWaypoints[20].points.Add(new Vector3(-102, 15, 1));
            tankWaypoints[20].points.Add(new Vector3(-116, 22, 1));
            tankWaypoints[20].points.Add(new Vector3(-116, 32, 1));
            tankWaypoints[20].points.Add(new Vector3(-114, 32, 1));
            tankWaypoints[20].points.Add(new Vector3(-98, 39, 1));
            tankWaypoints[20].points.Add(new Vector3(-90, 34, 1));
            tankWaypoints[21].points.Add(new Vector3(-86, -83, 1));
            tankWaypoints[21].points.Add(new Vector3(-86, -83, 1));
            tankWaypoints[21].points.Add(new Vector3(-90, -84, 1));
            tankWaypoints[21].points.Add(new Vector3(-92, -84, 1));
            tankWaypoints[21].points.Add(new Vector3(-92, -77, 1));
            tankWaypoints[21].points.Add(new Vector3(-124, -77, 1));
            tankWaypoints[21].points.Add(new Vector3(-134, -84, 1));
            tankWaypoints[21].points.Add(new Vector3(-146, -84, 1));
            tankWaypoints[21].points.Add(new Vector3(-146, -77, 1));
            tankWaypoints[21].points.Add(new Vector3(-131, -77, 1));
            tankWaypoints[21].points.Add(new Vector3(-124, -84, 1));
            tankWaypoints[21].points.Add(new Vector3(-101, -84, 1));
            tankWaypoints[21].points.Add(new Vector3(-100, -84, 1));
            tankWaypoints[21].points.Add(new Vector3(-91, -84, 1));

            //tankWaypoints[21].points.Add(new );

            for (int i = 0; i < tankWaypoints.Count; i++)
            {
                _tanks.Add(new Tank(Game, _camera, _logic, tankWaypoints[i].points, Vector3.Zero));
            }

            //Puppets.
            foreach (Tank tank in _tanks)
            {
                if (tank.Y > 0)
                {
                    _puppetTanks.Add(new Tank(Game, _camera, _logic, tank.PatrolRoute,
                        new Vector3(0, -250, 0), tank));
                }

                if (tank.Y < 0)
                {
                    _puppetTanks.Add(new Tank(Game, _camera, _logic, tank.PatrolRoute,
                        new Vector3(0, 250, 0), tank));
                }

                if (tank.X > 0)
                {
                    _puppetTanks.Add(new Tank(Game, _camera, _logic, tank.PatrolRoute,
                        new Vector3(-400, 0, 0), tank));
                }

                if (tank.X < 0)
                {
                    _puppetTanks.Add(new Tank(Game, _camera, _logic, tank.PatrolRoute,
                        new Vector3(400, 0, 0), tank));
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
        #region  Private/Protected Methods
        #endregion
    }
}
