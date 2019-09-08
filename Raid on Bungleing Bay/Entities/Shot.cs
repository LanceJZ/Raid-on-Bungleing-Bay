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
    class Shot : Cube
    {
        #region Fields
        GameLogic _logicRef;
        Timer _lifeTimer;
        #endregion
        #region Properties

        #endregion
        #region Constructor
        public Shot(Game game, Camera camera, GameLogic gameLogic) : base(game, camera)
        {
            Enabled = false;
            _logicRef = gameLogic;
            _lifeTimer = new Timer(game, 3);

        }
        #endregion
        #region Initialize-Load-BeginRun
        public override void Initialize()
        {
            Scale = 0.5f;
            base.Initialize();
        }

        protected override void LoadContent()
        {

            base.LoadContent();
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
