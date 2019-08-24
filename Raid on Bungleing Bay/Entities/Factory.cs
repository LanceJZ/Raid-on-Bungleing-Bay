using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Panther;

namespace Raid_on_Bungleing_Bay.Entities
{
    class Factory : ModelEntity
    {
        public Factory (Game game, Camera camera, GameLogic gameLogic) : base(game, camera)
        {

        }
        public override void Initialize()
        {
            Enabled = true;
            PO.Position.X = 14.5f;
            PO.Position.Y = -42f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            LoadModel("Factory");

            base.LoadContent();
        }

        public override void BeginRun()
        {
            base.BeginRun();
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

    }
}
