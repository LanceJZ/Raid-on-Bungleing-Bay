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
    class JetPlanes : GameComponent
    {
        #region Fields
        GameLogic _logic;
        Camera _camera;
        List<JetPlane> _jetPlanes;
        List<Vector3> _runways;
        Timer _takeoff;
        #endregion
        #region Properties

        #endregion
        #region Constructor
        public JetPlanes(Game game, Camera camera, GameLogic gameLogic) : base(game)
        {
            _logic = gameLogic;
            _camera = camera;
            _jetPlanes = new List<JetPlane>();
            _runways = new List<Vector3>();
            _takeoff = new Timer(game, 15);

            game.Components.Add(this);
        }
        #endregion
        #region Initialize
        public override void Initialize()
        {
            _runways.Add(new Vector3(-46, 65, 1));
            _runways.Add(new Vector3(-111, 65, 1));
            _runways.Add(new Vector3(88, -60, 1));
            _runways.Add(new Vector3(144, -60, 1));

            base.Initialize();
        }
        #endregion
        #region Update
        public override void Update(GameTime gameTime)
        {
            if (_takeoff.Elapsed)
            {
                _takeoff.Reset();

                Takeoff();
            }

            base.Update(gameTime);
        }
        #endregion
        #region Public Methods
        #endregion
        #region  Private/Protected Methods
        void Takeoff()
        {
            bool needNew = true;
            int useThisOne = 0;

            for (int i = 0; i < _jetPlanes.Count; i++)
            {
                if (!_jetPlanes[i].Enabled)
                {
                    useThisOne = i;
                    needNew = false;
                }
            }

            if (needNew)
            {
                _jetPlanes.Add(new JetPlane(Game, _camera, _logic));
                useThisOne = _jetPlanes.Count - 1;
                //JetPlane puppetX = new JetPlane(Game, _camera, _logic, true);
                //JetPlane puppetY = new JetPlane(Game, _camera, _logic, true);
                //_jetPlanes[useThisOne].PuppetX = puppetX;
                //_jetPlanes[useThisOne].PuppetY = puppetY;
            }

            int runway = Helper.RandomMinMax(0, 3);
            float direction = 0;

            switch(runway)
            {
                case 1:
                case 2:
                    direction = 0;
                    break;
                case 0:
                case 3:
                    direction = MathHelper.Pi;
                    break;
            }

            Vector3 heading = new Vector3(0, 0, direction);
            _jetPlanes[useThisOne].Spawn(_runways[runway], heading);
        }
        #endregion
    }
}
