#region Using
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using Panther;
#endregion
namespace Panther
{
    public class MapCross : Cube
    {
        #region Fields
        Cube _left;
        Cube _right;
        Cube _top;
        Cube _bottom;
        List<Vector3> _pointsOfInterest;
        float _camOrg;
        #endregion
        #region Properties

        #endregion
        #region Constructor
        public MapCross(Game game, Camera camera) : base(game, camera)
        {
            Enabled = false;
            //_camera = camera;
            _left = new Cube(game, camera);
            _right = new Cube(game, camera);
            _top = new Cube(game, camera);
            _bottom = new Cube(game, camera);
            _pointsOfInterest = new List<Vector3>();
        }
        #endregion
        #region Initialize
        public override void Initialize()
        {
            PO.Position.Z = 0.5f;
            _camOrg = _camera.Position.Z;

            _left.AddAsChildOf(this);
            _right.AddAsChildOf(this);
            _top.AddAsChildOf(this);
            _bottom.AddAsChildOf(this);

            _left.PO.Position.X = -2;
            _right.PO.Position.X = 2;
            _top.PO.Position.Y = -2;
            _bottom.PO.Position.Y = 2;

            ScaleAll = 0.5f;

            _left.Scale.X = 2;
            _right.Scale.X = 2;
            _top.Scale.Y = 2;
            _bottom.Scale.Y = 2;
            _left.Scale.Y = 0.5f;
            _right.Scale.Y = 0.5f;
            _top.Scale.X = 0.5f;
            _bottom.Scale.X = 0.5f;

            base.Initialize();
        }
        #endregion
        #region Update
        public override void Update(GameTime gameTime)
        {
            _camera.MoveTo(new Vector3(PO.Position.X, PO.Position.Y, _camera.Position.Z));

            GetInput();

            base.Update(gameTime);
        }
        #endregion
        #region Public methods
        #endregion
        #region Private/Protected methods
        void GetInput()
        {
            if (Helper.KeyDown(Keys.Up))
            {
                PO.Velocity.Y = 5;
            }
            else if (Helper.KeyDown(Keys.W))
            {
                PO.Velocity.Y = 25;
            }
            else if (Helper.KeyDown(Keys.Down))
            {
                PO.Velocity.Y = -5;
            }
            else if (Helper.KeyDown(Keys.S))
            {
                PO.Velocity.Y = -25;
            }
            else
            {
                PO.Velocity.Y = 0;
            }

            if (Helper.KeyDown(Keys.Left))
            {
               PO.Velocity.X = -5f;
            }
            else if (Helper.KeyDown(Keys.A))
            {
                PO.Velocity.X = -25;
            }
            else if (Helper.KeyDown(Keys.Right))
            {
                PO.Velocity.X = 5f;
            }
            else if (Helper.KeyDown(Keys.D))
            {
                PO.Velocity.X = 25;
            }
            else
            {
                PO.Velocity.X = 0;
            }

            if (Helper.KeyDown(Keys.PageUp))
            {
                _camera.MoveTo(new Vector3(_camera.X, _camera.Y, _camera.Z += 10));
            }

            if (Helper.KeyDown(Keys.PageDown))
            {
                _camera.MoveTo(new Vector3(_camera.X, _camera.Y, _camera.Z -= 10));
            }

            if (Helper.KeyDown(Keys.Back))
            {
                _camera.MoveTo(new Vector3(_camera.X, _camera.Y, _camOrg));
            }

            if (Helper.KeyPressed(Keys.M))
            {
                Vector3 pos = Vector3.Zero;
                pos.X = (int)Math.Round(Position.X);
                pos.Y = (int)Math.Round(Position.Y);
                pos.Z = 1;

                System.Diagnostics.Debug.WriteLine("Location " + pos.ToString() + " added to list.");

                _pointsOfInterest.Add(pos);
            }

            if (Helper.KeyPressed(Keys.Enter))
            {
                string pointsOut = "";

                foreach(Vector3 v in _pointsOfInterest)
                {
                    pointsOut += "X: " + v.X.ToString() + " Y: " + v.Y.ToString() + " Z: " + v.Z.ToString() + "\n";
                    pointsOut += "Vector3(" + v.X.ToString() + ", " + v.Y.ToString() + ", " + v.Z.ToString() + ")\n";
                }

                File.WriteAllText("pointesOfInterest.txt", pointsOut);
                System.Diagnostics.Debug.WriteLine("Points Saved.");
            }
        }
        #endregion
    }
}
