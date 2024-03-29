﻿#region Using
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System;
#endregion
namespace Panther
{
    public class ModelEntity : Entity
    {
        #region Fields
        string _modelFileName;
        List<ModelEntity> _children = new List<ModelEntity>();
        bool _modelLoaded;
        protected Model _model;
        protected Dictionary<string, Matrix> _baseTransforms = new Dictionary<string, Matrix>();
        protected Dictionary<string, Matrix> _currentTransforms = new Dictionary<string, Matrix>();
        protected Matrix[] _boneTransforms;
        protected Vector3 _minVector;
        protected Vector3 _maxVector;
        public Vector3 ModelScaleVelocity = Vector3.Zero;
        public Vector3 ModelScaleAcceleration = Vector3.Zero;
        public float Alpha = 1;
        #endregion
        #region Properties
        public new Camera _camera { get => base._camera; }

        public Vector3 WorldPosition
        {
            get => _PO.WorldPosition;
        }

        public Vector3 WorldVelocity
        {
            get => _PO.WorldVelocity;
        }


        public Vector3 WorldAcceleration
        {
            get => _PO.WorldAcceleration;
        }

        public Vector3 WorldRotation
        {
            get => _PO.WorldRotation;
        }

        public Vector3 WorldRotationVelocity
        {
            get => _PO.WorldRotationVelocity;
        }

        public Vector3 WorldRotationAcceleration
        {
            get => _PO.WorldRotationAcceleration;
        }

        public Model ModelRef { get => _model; }

        public BoundingBox Bounds
        {
            get
            {
                Matrix scaleMatrix = Matrix.CreateScale(ScaleAll);
                Matrix rotate = RotateMatrix(Rotation);
                Matrix translate = Matrix.CreateTranslation(Position);

                Matrix transform = scaleMatrix * rotate * translate;

                Vector3 v1 = Vector3.Transform(_minVector, transform);
                Vector3 v2 = Vector3.Transform(_maxVector, transform);
                Vector3 boxMin = Vector3.Min(v1, v2);
                Vector3 boxMax = Vector3.Max(v1, v2);

                return new BoundingBox(boxMin, boxMax);
            }
        }

        public BoundingSphere Sphere
        {
            get
            {
                BoundingSphere sphere = _model.Meshes[0].BoundingSphere;
                sphere.Radius *= 0.75f;
                sphere = sphere.Transform(_world);
                return sphere;
            }
        }

        public new bool Visible
        {
            get => base.Visible;
            set
            {
                base.Visible = value;

                foreach (ModelEntity child in _children)
                {
                    child.Visible = value;
                }
            }
        }

        public string ModelFileName { get => _modelFileName; set => _modelFileName = value; }
        #endregion
        #region Constructor
        public ModelEntity(Game game, Camera camera) : base(game, camera)
        {
        }

        public ModelEntity(Game game, Camera camera, Model model) : base(game, camera)
        {
            SetModel(model);
        }

        public ModelEntity(Game game, Camera camera, string modelFileName) : base(game, camera)
        {
            _modelFileName = modelFileName;
        }

        public ModelEntity(Game game, Camera camera, Model model, Vector3 position) : base(game, camera)
        {
            _PO.Position = position;
            SetModel(model);
        }

        public ModelEntity(Game game, Camera camera, string modelFileName, Vector3 position) : base(game, camera)
        {
            _PO.Position = position;
            _modelFileName = modelFileName;
        }

        #endregion
        #region Initialize-Dispose-Remove-Unload
        public override void Initialize()
        {
            base.Initialize();

            if (_model == null && _modelFileName != null)
                LoadModel(_modelFileName);
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Dispose(bool disposing)
        {
            _model = null;

            base.Dispose(disposing);
        }

        public void Remove()
        {
            Enabled = false;
            Game.Components.Remove(this);
            Dispose();
        }
        #endregion
        #region Update and Draw
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (PO.Moveable || !_modelLoaded)
            {
                MatrixUpdate();
            }
        }

        public void MatrixUpdate()
        {
            if (_model != null && _boneTransforms != null)
            {
                _model.Root.Transform = Matrix.CreateScale(Scale) *
                    RotateMatrix(Rotation) * Matrix.CreateTranslation(Position);

                if (_PO.Child)
                {
                    foreach (PositionedObject po in _PO.ParentPOs)
                    {
                        _model.Root.Transform *=
                            RotateMatrix(po.Rotation) * Matrix.CreateTranslation(po.Position);
                    }
                }

                foreach (string keys in _currentTransforms.Keys)
                {
                    _model.Bones[keys].Transform = _currentTransforms[keys];
                }

                _model.CopyAbsoluteBoneTransformsTo(_boneTransforms);
                _world = _model.Root.Transform;

                _modelLoaded = true;
            }
        }
        public override void Draw(GameTime gameTime)
        {
            if (!Visible)
                return;

            if (_model == null)
                return;

            if (base._camera == null)
            {
                System.Diagnostics.Debug.WriteLine("The Camera is not setup (null) on the class. " + this);
                return;
            }

            if (_PO.Child)
            {
                if (!_PO.ParentPO.Enabled)
                    return;
            }

            Game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            foreach (ModelMesh mesh in _model.Meshes)
            {
                foreach (BasicEffect basicEffect in mesh.Effects)
                {
                    basicEffect.World = _boneTransforms[mesh.ParentBone.Index];
                    basicEffect.View = base._camera.View;
                    basicEffect.Projection = base._camera.Projection;
                    basicEffect.DiffuseColor = _diffuseColor;
                    basicEffect.EmissiveColor = EmissiveColor;
                    basicEffect.Alpha = Alpha;
                    basicEffect.EnableDefaultLighting();
                }

                mesh.Draw();
            }

            base.Draw(gameTime);
        }
        #endregion
        #region Helper Methods
        /// <summary>
        /// Parents of any children must be added first or they will not be seen
        /// by the children added later. Active Dependent by default.
        /// </summary>
        /// <param name="parent">Parent Entity</param>
        public void AddAsChildOf(ModelEntity parent)
        {
            AddAsChildOf(parent, true);
        }
        /// <summary>
        /// Parents of any children must be added first or they will not be seen
        /// by the children added later.
        /// </summary>
        /// <param name="parent">Parent Entity</param>
        /// <param name="activeDepedent">True if child active state depends on parent.</param>
        public void AddAsChildOf(ModelEntity parent, bool activeDepedent)
        {
            parent._children.Add(this);
            _PO.AddAsChildOf(parent._PO, activeDepedent);
        }
        /// <summary>
        /// Sets the model from a loaded XNA Model.
        /// </summary>
        /// <param name="model">Loaded model.</param>
        public void SetModel(Model model)
        {
            if (model != null)
            {
                _model = model;
            }
        }
        /// <summary>
        /// Loads and sets the model from the file name.
        /// </summary>
        /// <param name="modelFileName">The file name located at content/Models</param>
        public void LoadModel(string modelFileName)
        {
            _model = Helper.LoadModel(modelFileName);
            SetModel(_model); //TODO: Set sphere from model mesh.

            if (_model != null)
            {
                BoundingBox bbox = GetBounds(_model);

                bbox.Max.X = Math.Abs(bbox.Max.X);
                bbox.Max.Y = Math.Abs(bbox.Max.Y);
                bbox.Max.Z = Math.Abs(bbox.Max.Z);
                bbox.Min.X = Math.Abs(bbox.Min.X);
                bbox.Min.Y = Math.Abs(bbox.Min.Y);
                bbox.Min.Z = Math.Abs(bbox.Min.Z);

                Rectangle bounds = new Rectangle();
                bounds.Width = (int)bbox.Max.X + (int)bbox.Min.X;
                bounds.Height = (int)bbox.Max.Y + (int)bbox.Min.Y;
                PO.BoundingBox = bounds;

                float max = 0;

                if (bbox.Max.X > max)
                    max = bbox.Max.X;

                if (bbox.Max.Y > max)
                    max = bbox.Max.Y;

                if (bbox.Max.Z > max)
                    max = bbox.Max.Z;

                if (bbox.Min.X > max)
                    max = bbox.Min.X;

                if (bbox.Min.Y > max)
                    max = bbox.Min.Y;

                if (bbox.Min.Z > max)
                    max = bbox.Min.Z;

                PO.Radius = max;
            }

            if (_model != null)
            {
                _boneTransforms = new Matrix[_model.Bones.Count];

                for (int i = 1; i < _model.Bones.Count; i++)
                {
                    _baseTransforms[_model.Bones[i].Name] = _model.Bones[i].Transform;
                    _currentTransforms[_model.Bones[i].Name] = _model.Bones[i].Transform;
                }

                _minVector = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
                _maxVector = new Vector3(float.MinValue, float.MinValue, float.MinValue);

                foreach (ModelMesh mesh in _model.Meshes)
                {
                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        VertexPositionNormalTexture[] vertexData = new
                        VertexPositionNormalTexture[part.VertexBuffer.VertexCount];
                        part.VertexBuffer.GetData<VertexPositionNormalTexture>(vertexData);

                        for (int i = 0; i < part.VertexBuffer.VertexCount; i++)
                        {
                            _minVector = Vector3.Min(_minVector, vertexData[i].Position);
                            _maxVector = Vector3.Max(_maxVector, vertexData[i].Position);
                        }
                    }
                }

                Game.SuppressDraw();
                //Enabled = true;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("The Model was null for this Entity. " + this);
            }
        }
        /// <summary>
        /// Return a Model that is loaded from the filename.
        /// </summary>
        /// <param name="modelFileName">The file name located at content/Models</param>
        /// <returns></returns>
        public Model Load(string modelFileName)
        {
            return Helper.LoadModel(modelFileName);
        }

        public bool IsCollision(Model model1, Matrix world1, Model model2, Matrix world2)
        {
            for (int meshIndex1 = 0; meshIndex1 < model1.Meshes.Count; meshIndex1++)
            {
                BoundingSphere sphere1 = model1.Meshes[meshIndex1].BoundingSphere;
                sphere1 = sphere1.Transform(world1);

                for (int meshIndex2 = 0; meshIndex2 < model2.Meshes.Count; meshIndex2++)
                {
                    BoundingSphere sphere2 = model2.Meshes[meshIndex2].BoundingSphere;
                    sphere2 = sphere2.Transform(world2);

                    if (sphere1.Intersects(sphere2))
                        return true;
                }
            }
            return false;
        }
        #endregion
        #region Private methods
        public BoundingBox GetBounds(Model model)
        {
            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {
                    int vertexStride = meshPart.VertexBuffer.VertexDeclaration.VertexStride;
                    int vertexBufferSize = meshPart.NumVertices * vertexStride;

                    int vertexDataSize = vertexBufferSize / sizeof(float);
                    float[] vertexData = new float[vertexDataSize];
                    meshPart.VertexBuffer.GetData<float>(vertexData);

                    for (int i = 0; i < vertexDataSize; i += vertexStride / sizeof(float))
                    {
                        Vector3 vertex = new Vector3(vertexData[i], vertexData[i + 1], vertexData[i + 2]);
                        min = Vector3.Min(min, vertex);
                        max = Vector3.Max(max, vertex);
                    }
                }
            }

            return new BoundingBox(min, max);
        }
        #endregion
    }
}
