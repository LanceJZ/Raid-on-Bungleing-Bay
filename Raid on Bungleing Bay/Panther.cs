#region Using
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Panther;

#endregion
namespace Raid_on_Bungleing_Bay
{
    public class Panther : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        GameLogic _game;
        Camera _camera;
        Timer _FPSTimer;
        KeyboardState _oldKeyState;
        //float FPSFrames = 0;
        bool _pauseGame;
        bool _notFirstFrame;

        public Panther()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.SynchronizeWithVerticalRetrace = true; //When true, 60FSP refresh rate locked.
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.PreferMultiSampling = true; //Error in MonoGame 3.6 for DirectX, fixed in version 3.7.
            _graphics.PreparingDeviceSettings += SetMultiSampling;
            _graphics.ApplyChanges();
            _graphics.GraphicsDevice.RasterizerState = new RasterizerState(); //Must be after Apply Changes.
            IsFixedTimeStep = true; //When true, 60FSP refresh rate locked.

            Content.RootDirectory = "Content";

            Helper.Initialize(this, _graphics);
            _FPSTimer = new Timer(this, 1);

            _camera = new Camera(this, new Vector3(0, 0, 100), new Vector3(0, MathHelper.Pi, 0),
                GraphicsDevice.Viewport.AspectRatio, 1f, 1200f);

            _game = new GameLogic(this, _camera);
        }

        void SetMultiSampling(object sender, PreparingDeviceSettingsEventArgs eventArgs)
        {
            PresentationParameters PresentParm = eventArgs.GraphicsDeviceInformation.PresentationParameters;
            PresentParm.MultiSampleCount = 4;
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Helper.ScreenHeight = 900;
            Helper.SreenWidth = 1200;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _game.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void BeginRun()
        {
            base.BeginRun();
            _game.BeginRun();
        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState KBS = Keyboard.GetState();

            if (_game.CurrentMode == GameState.InPlay)
            {
                if (!_oldKeyState.IsKeyDown(Keys.P) && KBS.IsKeyDown(Keys.P))
                    _pauseGame = !_pauseGame;
            }

            _oldKeyState = Keyboard.GetState();

            if (!_pauseGame)
                base.Update(gameTime);

            //FPSFrames++;

            //if (FPSTimer.Elapsed)
            //{
            //    FPSTimer.Reset();
            //    System.Diagnostics.Debug.WriteLine("FPS " + FPSFrames.ToString());
            //    FPSFrames = 0;
            //}
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);


            if (_notFirstFrame)
            {
                base.Draw(gameTime);
            }
            else
            {
                _notFirstFrame = true;
            }
        }
    }
}
