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

        ModelEntity topMidOtherSide;
        ModelEntity topRightOtherSide;
        ModelEntity topLeftOtherSide;
        ModelEntity topRightEdgeOtherSide;

        ModelEntity bottomRightOtherSide;
        ModelEntity bottomRightEdgeOtherSide;

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

            topMidOtherSide = new ModelEntity(game, camera);
            topRightOtherSide = new ModelEntity(game, camera);
            topLeftOtherSide = new ModelEntity(game, camera);
            topRightEdgeOtherSide = new ModelEntity(game, camera);

            bottomRightOtherSide = new ModelEntity(game, camera);
            bottomRightEdgeOtherSide = new ModelEntity(game, camera);
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

            topMidOtherSide.PO.AddAsChildOf(this);
            topRightOtherSide.PO.AddAsChildOf(this);
            topLeftOtherSide.PO.AddAsChildOf(this);
            topRightEdgeOtherSide.PO.AddAsChildOf(this);

            bottomRightOtherSide.PO.AddAsChildOf(this);
            bottomRightEdgeOtherSide.PO.AddAsChildOf(this);

            Rotation.X = -MathHelper.Pi / 2;
        }

        public override void BeginRun()
        {
            base.BeginRun();

            bottomRightEdge.LoadModel("Land-0-Right edge");
            topRightEdge.LoadModel("Land-1-Right edge");
            bottomRight.LoadModel("Land-2-Bottom right map");
            topRight.LoadModel("Land-3-top right");
            bottomMid.LoadModel("Land-4-Bottom mid");
            bottomLeft.LoadModel("Land-5-Bottom left");
            topMid.LoadModel("Land-6-Top mid");
            topLeft.LoadModel("Land-7-Top left");

            topMidOtherSide.LoadModel("Land-6-Top mid");
            topMidOtherSide.PO.Position.Z = -250;

            topRightOtherSide.LoadModel("Land-3-top right");
            topRightOtherSide.PO.Position.Z = -250;

            topLeftOtherSide.LoadModel("Land-7-Top left");
            topLeftOtherSide.PO.Position.Z = -250;

            topRightEdgeOtherSide.LoadModel("Land-1-Right edge");
            topRightEdgeOtherSide.PO.Position.Z = -250;

            bottomRightOtherSide.LoadModel("Land-2-Bottom right map");
            bottomRightOtherSide.PO.Position.Z = 250;
            bottomRightEdgeOtherSide.LoadModel("Land-0-Right edge");
            bottomRightEdgeOtherSide.PO.Position.Z = 250;

            BoundingBox = new Rectangle(0, 0, 400, 250);
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
