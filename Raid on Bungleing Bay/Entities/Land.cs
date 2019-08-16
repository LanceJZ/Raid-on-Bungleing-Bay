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
    class Land : PositionedObject
    {
        #region Fields
        ModelEntity topRightEdge;
        ModelEntity bottomRightEdge;
        ModelEntity topRight;
        ModelEntity bottomRight;
        ModelEntity topMid;
        ModelEntity bottomMid;
        ModelEntity topLeft;
        ModelEntity bottomLeft;
        GameLogic LogicRef;

        #endregion
        #region Properties

        #endregion
        #region Constructor
        public Land(Game game, Camera camera, GameLogic gameLogic) : base(game)
        {
            LogicRef = gameLogic;
            Enabled = true;

            topRightEdge = new ModelEntity(game, camera);
            bottomRightEdge = new ModelEntity(game, camera);
            topRight = new ModelEntity(game, camera);
            bottomRight = new ModelEntity(game, camera);
            topMid = new ModelEntity(game, camera);
            bottomMid = new ModelEntity(game, camera);
            topLeft = new ModelEntity(game, camera);
            bottomLeft = new ModelEntity(game, camera);
        }
        #endregion
        #region Initialize-Load-BeginRun
        public override void Initialize()
        {
            base.Initialize();
            topRightEdge.PO.AddAsChildOf(this);
            bottomRightEdge.PO.AddAsChildOf(this);
            bottomRight.PO.AddAsChildOf(this);
            topRight.PO.AddAsChildOf(this);
            bottomMid.PO.AddAsChildOf(this);
            bottomLeft.PO.AddAsChildOf(this);
            topMid.PO.AddAsChildOf(this);
            topLeft.PO.AddAsChildOf(this);

        }

        public override void BeginRun()
        {
            base.BeginRun();

            bottomRightEdge.LoadModel("Land-0-Right edge");
            topRightEdge.LoadModel("Land-1-Right edge"); //Top
            bottomRight.LoadModel("Land-2-Bottom right map");
            topRight.LoadModel("Land-3-top right");
            bottomMid.LoadModel("Land-4-Bottom mid");
            bottomLeft.LoadModel("Land-5-Bottom left");
            topMid.LoadModel("Land-6-Top mid");
            topLeft.LoadModel("Land-7-Top left");
            RotationVelocity.X = MathHelper.Pi;

        }
        #endregion
        #region Update
        public override void Update(GameTime gameTime)
        {
            Rotation.X = MathHelper.Pi;

            base.Update(gameTime);
        }
        #endregion
    }
}
