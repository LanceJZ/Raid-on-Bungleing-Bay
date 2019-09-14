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
        #region Initialize-BeginRun
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
            Rotation.X = -MathHelper.Pi / 2;
        }

        public override void BeginRun()
        {
            base.BeginRun();

            bottomRightEdge.LoadModel("Land-0-Right edge");
            Rectangle totalWH = bottomRightEdge.PO.BoundingBox;
            topRightEdge.LoadModel("Land-1-Right edge");
            totalWH.Width += topRightEdge.PO.BoundingBox.Width;
            totalWH.Height += topRightEdge.PO.BoundingBox.Height;
            bottomRight.LoadModel("Land-2-Bottom right map");
            totalWH.Width += bottomRight.PO.BoundingBox.Width;
            totalWH.Height += bottomRight.PO.BoundingBox.Height;
            topRight.LoadModel("Land-3-top right");
            totalWH.Width += topRight.PO.BoundingBox.Width;
            totalWH.Height += topRight.PO.BoundingBox.Height;
            bottomMid.LoadModel("Land-4-Bottom mid");
            totalWH.Width += bottomMid.PO.BoundingBox.Width;
            totalWH.Height += bottomMid.PO.BoundingBox.Height;
            bottomLeft.LoadModel("Land-5-Bottom left");
            totalWH.Width += bottomLeft.PO.BoundingBox.Width;
            totalWH.Height += bottomLeft.PO.BoundingBox.Height;
            topMid.LoadModel("Land-6-Top mid");
            totalWH.Width += topMid.PO.BoundingBox.Width;
            totalWH.Height += topMid.PO.BoundingBox.Height;
            topLeft.LoadModel("Land-7-Top left");
            totalWH.Width += topLeft.PO.BoundingBox.Width;
            totalWH.Height += topLeft.PO.BoundingBox.Height;

            BoundingBox = totalWH;
        }
        #endregion
        #region Update
        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }
        #endregion
    }
}
