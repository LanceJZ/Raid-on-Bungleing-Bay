#region Using
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Linq;
using System;
using Panther;
#endregion
namespace Raid_on_Bungleing_Bay.Entities
{
    class Shot : ShapeGenerater
    {
        #region Fields
        GameLogic _logicRef;
        Timer _lifeTimer;
        //ShapeGenerater _cube;
        #endregion
        #region Properties

        #endregion
        #region Constructor
        public Shot(Game game, Camera camera, GameLogic gameLogic) : base(game, camera, GeneratedShapes.Cube(), 12)
        {
            //TODO: change this to generated cube.
            Enabled = false;
            _logicRef = gameLogic;
            _lifeTimer = new Timer(game, 3);
            //_cube = new ShapeGenerater(game, camera, GeneratedShapes.Cube(), 12);
        }
        #endregion
        #region Initialize-Load
        public override void Initialize()
        {
            Scale = 0.5f;
            base.Initialize();
        }
        #endregion
        #region Update
        public override void Update(GameTime gameTime)
        {
            if (_lifeTimer.Elapsed)
            {
                Enabled = false;
            }

            base.Update(gameTime);
        }

        public void Fire(Vector3 position, Vector3 velocity)
        {
            Enabled = true;
            PO.Position = position;
            PO.Velocity = velocity;
            _lifeTimer.Reset();
        }
        #endregion
    }
}
