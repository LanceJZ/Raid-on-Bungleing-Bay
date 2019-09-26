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

        ModelEntity topMidBottomSide;
        ModelEntity topRighbottomSide;
        ModelEntity topLeftBottomSide;
        ModelEntity topRightEdgeBottomSide;

        ModelEntity bottomRightTopSide;
        ModelEntity bottomRightEdgTopSide;
        ModelEntity bottomMidTopSide;
        ModelEntity bottomLeftTopSide;

        ModelEntity topLeftRightSide;
        ModelEntity bottomLeftRightSide;
        ModelEntity topRightEdgeLeftSide;
        ModelEntity bottomRightEdgeLeftSide;
        ModelEntity topRightLeftSide;
        ModelEntity bottomRightLeftSide;

        ModelEntity topLeftBottomRight;
        ModelEntity bottomLeftTopRight;
        ModelEntity topRightEdgeBottomLeft;
        ModelEntity topRightBottomLeft;
        ModelEntity bottomRightEdgeTopLeft;
        ModelEntity bottomRightTopLeft;

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

            topMidBottomSide = new ModelEntity(game, camera);
            topRighbottomSide = new ModelEntity(game, camera);
            topLeftBottomSide = new ModelEntity(game, camera);
            topRightEdgeBottomSide = new ModelEntity(game, camera);

            bottomRightTopSide = new ModelEntity(game, camera);
            bottomRightEdgTopSide = new ModelEntity(game, camera);
            bottomMidTopSide = new ModelEntity(game, camera);
            bottomLeftTopSide = new ModelEntity(game, camera);

            topLeftRightSide = new ModelEntity(game, camera);
            bottomLeftRightSide = new ModelEntity(game, camera);
            topRightEdgeLeftSide = new ModelEntity(game, camera);
            bottomRightEdgeLeftSide = new ModelEntity(game, camera);
            topRightLeftSide = new ModelEntity(game, camera);
            bottomRightLeftSide = new ModelEntity(game, camera);

            topLeftBottomRight = new ModelEntity(game, camera);
            bottomLeftTopRight = new ModelEntity(game, camera);
            topRightEdgeBottomLeft = new ModelEntity(game, camera);
            topRightBottomLeft = new ModelEntity(game, camera);
            bottomRightEdgeTopLeft = new ModelEntity(game, camera);
            bottomRightTopLeft = new ModelEntity(game, camera);
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

            topMidBottomSide.PO.AddAsChildOf(this);
            topRighbottomSide.PO.AddAsChildOf(this);
            topLeftBottomSide.PO.AddAsChildOf(this);
            topRightEdgeBottomSide.PO.AddAsChildOf(this);

            bottomRightTopSide.PO.AddAsChildOf(this);
            bottomRightEdgTopSide.PO.AddAsChildOf(this);
            bottomMidTopSide.PO.AddAsChildOf(this);
            bottomLeftTopSide.PO.AddAsChildOf(this);

            topLeftRightSide.PO.AddAsChildOf(this);
            bottomLeftRightSide.PO.AddAsChildOf(this);
            topRightEdgeLeftSide.PO.AddAsChildOf(this);
            bottomRightEdgeLeftSide.PO.AddAsChildOf(this);
            topRightLeftSide.PO.AddAsChildOf(this);
            bottomRightLeftSide.PO.AddAsChildOf(this);

            topLeftBottomRight.PO.AddAsChildOf(this);
            bottomLeftTopRight.PO.AddAsChildOf(this);
            topRightEdgeBottomLeft.PO.AddAsChildOf(this);
            topRightBottomLeft.PO.AddAsChildOf(this);
            bottomRightEdgeTopLeft.PO.AddAsChildOf(this);
            bottomRightTopLeft.PO.AddAsChildOf(this);

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

            topMidBottomSide.LoadModel("Land-6-Top mid");
            topMidBottomSide.PO.Position.Z = -250;
            topRighbottomSide.LoadModel("Land-3-top right");
            topRighbottomSide.PO.Position.Z = -250;
            topLeftBottomSide.LoadModel("Land-7-Top left");
            topLeftBottomSide.PO.Position.Z = -250;
            topRightEdgeBottomSide.LoadModel("Land-1-Right edge");
            topRightEdgeBottomSide.PO.Position.Z = -250;

            bottomRightTopSide.LoadModel("Land-2-Bottom right map");
            bottomRightTopSide.PO.Position.Z = 250;
            bottomRightEdgTopSide.LoadModel("Land-0-Right edge");
            bottomRightEdgTopSide.PO.Position.Z = 250;
            bottomMidTopSide.LoadModel("Land-4-Bottom mid");
            bottomMidTopSide.PO.Position.Z = 250;
            bottomLeftTopSide.LoadModel("Land-5-Bottom left");
            bottomLeftTopSide.PO.Position.Z = 250;

            topLeftRightSide.LoadModel("Land-7-Top left");
            topLeftRightSide.PO.Position.X = 400;
            bottomLeftRightSide.LoadModel("Land-5-Bottom left");
            bottomLeftRightSide.PO.Position.X = 400;
            topRightEdgeLeftSide.LoadModel("Land-1-Right edge");
            topRightEdgeLeftSide.PO.Position.X = -400;
            bottomRightEdgeLeftSide.LoadModel("Land-0-Right edge");
            bottomRightEdgeLeftSide.PO.Position.X = -400;
            topRightLeftSide.LoadModel("Land-3-top right");
            topRightLeftSide.PO.Position.X = -400;
            bottomRightLeftSide.LoadModel("Land-2-Bottom right map");
            bottomRightLeftSide.PO.Position.X = -400;

            topLeftBottomRight.LoadModel("Land-7-Top left");
            topLeftBottomRight.PO.Position.X = 400;
            topLeftBottomRight.PO.Position.Z = -250;
            bottomLeftTopRight.LoadModel("Land-5-Bottom left");
            bottomLeftTopRight.PO.Position.X = 400;
            bottomLeftTopRight.PO.Position.Z = 250;
            topRightEdgeBottomLeft.LoadModel("Land-1-Right edge");
            topRightEdgeBottomLeft.PO.Position.X = -400;
            topRightEdgeBottomLeft.PO.Position.Z = -250;
            topRightBottomLeft.LoadModel("Land-3-top right");
            topRightBottomLeft.PO.Position.X = -400;
            topRightBottomLeft.PO.Position.Z = -250;
            bottomRightEdgeTopLeft.LoadModel("Land-0-Right edge");
            bottomRightEdgeTopLeft.PO.Position.X = -400;
            bottomRightEdgeTopLeft.PO.Position.Z = 250;
            bottomRightTopLeft.LoadModel("Land-2-Bottom right map");
            bottomRightTopLeft.PO.Position.X = -400;
            bottomRightTopLeft.PO.Position.Z = 250;

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
