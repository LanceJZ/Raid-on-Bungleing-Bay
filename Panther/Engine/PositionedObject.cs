#region Using
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Collections.Generic;
using System;
#endregion

namespace Panther
{
    public class PositionedObject : GameComponent
    {
        #region Fields
        Vector3 ChildPosition;
        public PositionedObject ParentPO;
        public List<PositionedObject> ChildrenPOs;
        public List<PositionedObject> ParentPOs;
        // These are fields because XYZ are fields of Vector3, a struct,
        // so they do not get data binned as a property.
        public Vector3 Position = Vector3.Zero;
        public Vector3 Acceleration = Vector3.Zero;
        public Vector3 Velocity = Vector3.Zero;
        public Vector3 Rotation = Vector3.Zero;
        public Vector3 RotationVelocity = Vector3.Zero;
        public Vector3 RotationAcceleration = Vector3.Zero;
        Vector2 TheHeightWidth;
        float TheElapsedGameTime;
        float TheScalePercent = 1;
        float GameRefScale = 1;
        float TheRadius = 0;
        bool TheHit;
        bool TheExplosionActive;
        bool IsPaused;
        bool IsMoveable = true;
        bool IsActiveDependent;
        bool IsDirectConnected;
        bool IsParent;
        bool IsChild;
        bool InDebugMode;
        #endregion
        #region Properties
        public Vector3 WorldPosition
        {
            get
            {
                Vector3 parentPOs = Vector3.Zero;

                foreach (PositionedObject po in ParentPOs)
                {
                    parentPOs += po.Position;
                }

                return Position + parentPOs;
            }
        }

        public Vector3 WorldVelocity
        {
            get
            {
                Vector3 parentPOs = Vector3.Zero;

                foreach (PositionedObject po in ParentPOs)
                {
                    parentPOs += po.Velocity;
                }

                return Velocity + parentPOs;
            }
        }

        public Vector3 WorldAcceleration
        {
            get
            {
                Vector3 parentPOs = Vector3.Zero;

                foreach (PositionedObject po in ParentPOs)
                {
                    parentPOs += po.Acceleration;
                }

                return Acceleration + parentPOs;
            }
        }

        public Vector3 WorldRotation
        {
            get
            {
                Vector3 parentPOs = Vector3.Zero;

                foreach (PositionedObject po in ParentPOs)
                {
                    parentPOs += po.Rotation;
                }

                return Rotation + parentPOs;
            }
        }

        public Vector3 WorldRotationVelocity
        {
            get
            {
                Vector3 parentPOs = Vector3.Zero;

                foreach (PositionedObject po in ParentPOs)
                {
                    parentPOs += po.RotationVelocity;
                }

                return RotationVelocity + parentPOs;
            }
        }

        public Vector3 WorldRotationAcceleration
        {
            get
            {
                Vector3 parentPOs = Vector3.Zero;

                foreach (PositionedObject po in ParentPOs)
                {
                    parentPOs += po.RotationAcceleration;
                }

                return RotationAcceleration + parentPOs;
            }
        }

        public float ElapsedGameTime { get => TheElapsedGameTime; }
        /// <summary>
        /// Scale by percent of original. If base of sprite, used to enlarge sprite.
        /// </summary>
        public float Scale { get => TheScalePercent; set => TheScalePercent = value; }
        /// <summary>
        /// Used for circle collusion. Sets radius of circle.
        /// </summary>
        public float Radius { get => TheRadius; set => TheRadius = value; }
        /// <summary>
        /// Enabled means this class is a parent, and has at least one child.
        /// </summary>
        public bool Parent { get => IsParent; set => IsParent = value; }
        /// <summary>
        /// Enabled means this class is a child to a parent.
        /// </summary>
        public bool Child { get => IsChild; set => IsChild = value; }
        /// <summary>
        /// Enabled tells class hit by another class.
        /// </summary>
        public bool Hit { get => TheHit; set => TheHit = value; }
        /// <summary>
        /// Enabled tells class an explosion is active.
        /// </summary>
        public bool ExplosionActive { get => TheExplosionActive; set => TheExplosionActive = value; }
        /// <summary>
        /// Enabled pauses class update.
        /// </summary>
        public bool Pause { get => IsPaused; set => IsPaused = value; }
        /// <summary>
        /// Enabled will move using velocity and acceleration.
        /// </summary>
        public bool Moveable { get => IsMoveable; set => IsMoveable = value; }
        /// <summary>
        /// Enabled causes the class to update. If base of Sprite, enables sprite to be drawn.
        /// </summary>
        public new bool Enabled
        {
            get => base.Enabled;

            set
            {
                base.Enabled = value;

                foreach (PositionedObject child in ChildrenPOs)
                {
                    if (child.ActiveDependent)
                        child.Enabled = value;
                }
            }
        }
        /// <summary>
        /// Enabled the active bool will mirror that of the parent.
        /// </summary>
        public bool ActiveDependent { get => IsActiveDependent; set => IsActiveDependent = value; }
        /// <summary>
        /// Enabled the position and rotation will always be the same as the parent.
        /// </summary>
        public bool DirectConnection { get => IsDirectConnected; set => IsDirectConnected = value; }
        /// <summary>
        /// Gets or sets the GameModel's AABB
        /// </summary>
        public bool Debug { set => InDebugMode = value; }

        public Vector2 WidthHeight { get => TheHeightWidth; set => TheHeightWidth = value; }

        public float GameScale { get => GameRefScale; set => GameRefScale = value; }

        public Rectangle BoundingBox
        {
            get => new Rectangle((int)Position.X, (int)Position.Y, (int)WidthHeight.X, (int)WidthHeight.Y);
        }

        public float X { get => Position.X; set => Position.X = value; }
        public float Y { get => Position.Y; set => Position.Y = value; }
        public float Z { get => Position.Z; set => Position.Z = value; }
        #endregion
        #region Constructor
        /// <summary>
        /// This is the constructor that gets the Positioned Object ready for use and adds it to the Drawable Components list.
        /// </summary>
        /// <param name="game">The game class</param>
        public PositionedObject(Game game) : base(game)
        {
            ChildrenPOs = new List<PositionedObject>();
            ParentPOs = new List<PositionedObject>();
            Game.Components.Add(this);
        }
        #endregion
        #region Public Methods
        public override void Initialize()
        {
            base.Initialize();
            BeginRun();
        }

        public virtual void BeginRun() { }
        /// <summary>
        /// Allows the game component to be updated.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (IsMoveable)
            {
                base.Update(gameTime);

                TheElapsedGameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                Velocity += Acceleration * TheElapsedGameTime;
                Position += Velocity * TheElapsedGameTime;
                RotationVelocity += RotationAcceleration * TheElapsedGameTime;
                Rotation += RotationVelocity * TheElapsedGameTime;

                Rotation = Helper.WrapAngle(Rotation);
            }

            if (IsChild)
            {
                if (DirectConnection)
                {
                    Position = ParentPO.Position;
                    Rotation = ParentPO.Rotation;
                }
            }

            base.Update(gameTime);
        }
        /// <summary>
        /// Adds child that is not directly connect.
        /// </summary>
        /// <param name="parrent">The parent to this class.</param>
        /// <param name="activeDependent">If this class is active when the parent is.</param>
        public virtual void AddAsChildOf(PositionedObject parrent, bool activeDependent)
        {
            AddAsChildOf(parrent, activeDependent, false);
        }
        /// <summary>
        /// Adds child that is active dependent and not directly connected.
        /// </summary>
        /// <param name="parrent">The Parent to this class.</param>
        public virtual void AddAsChildOf(PositionedObject parrent)
        {
            AddAsChildOf(parrent, true, false);
        }
        /// <summary>
        /// Add PO class or base PO class from AModel or Sprite as child of this class.
        /// Make sure all the parents of the parent are added before the children.
        /// </summary>
        /// <param name="parent">The parent to this class.</param>
        /// <param name="activeDependent">If this class is active when the parent is.</param>
        /// <param name="directConnection">Bind Position and Rotation to child.</param>
        public virtual void AddAsChildOf(PositionedObject parent, bool activeDependent,
            bool directConnection)
        {
            if (ParentPO != null)
                return;

            ActiveDependent = activeDependent;
            DirectConnection = directConnection;
            Child = true;
            ParentPO = parent;
            ParentPO.Parent = true;
            ParentPO.ChildrenPOs.Add(this);
            ParentPOs.Add(parent);

            for (int i = 0; i < ParentPOs.Count; i++)
            {
                if (ParentPOs[i].ParentPO != null && ParentPOs[i].ParentPO != parent)
                {
                    ParentPOs.Add(ParentPOs[i].ParentPO);
                }
            }
        }

        public void ChildLink(bool active)
        {
            if (!active)
            {
                ChildPosition = Position;
                Position = WorldPosition;
                ParentPOs.Remove(ParentPO);
                ParentPO.ChildrenPOs.Remove(this);
            }
            else
            {
                Position = ChildPosition;
                ParentPOs.Add(ParentPO);
                ParentPO.ChildrenPOs.Add(this);

                for (int i = 0; i < ParentPOs.Count; i++)
                {
                    if (ParentPOs[i].ParentPO != null && ParentPOs[i].ParentPO != ParentPO)
                    {
                        ParentPOs.Add(ParentPOs[i].ParentPO);
                    }
                }
            }

            Child = active;
            ParentPO.Parent = active;
        }

        public void Remove()
        {
            Game.Components.Remove(this);
        }


        #endregion
    }
}
