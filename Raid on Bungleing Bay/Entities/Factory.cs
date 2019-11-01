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
        public Factory (Game game, Camera camera, GameLogic gameLogic, Vector3 position, Factory mirror = null)
            : base(game, camera)
        {
            Position = position;

            switch(Helper.RandomMinMax(1, 4))
            {
                case 2:
                    PO.Rotation.Z = MathHelper.PiOver2;
                    break;
                case 3:
                    PO.Rotation.Z = MathHelper.Pi;
                    break;
                case 4:
                    PO.Rotation.Z = MathHelper.Pi + MathHelper.PiOver2;
                    break;
            }

            if (mirror != null)
            {
                _mirror = mirror;
                Rotation = mirror.Rotation;
            }
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
