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
        Factory _mirror;
        public Factory (Game game, Camera camera, GameLogic gameLogic, Vector3 position, Factory mirror = null) : base(game, camera)
        {
            Position = position;

            if (mirror != null)
                _mirror = mirror;
        }

        public override void Initialize()
        {
            Enabled = true;
            LoadModel("Factory");

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

    }
}
