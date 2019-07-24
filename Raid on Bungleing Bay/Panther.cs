﻿#region Using
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
        GraphicsDeviceManager GDM;
        SpriteBatch SB;
        GameLogic TheGame;
        Camera TheCamera;
        Timer FPSTimer;
        KeyboardState OldKeyState;
        float FPSFrames = 0;
        bool PauseGame;
        bool NotFirstFrame;

        public Panther()
        {
            GDM = new GraphicsDeviceManager(this);
            GDM.SynchronizeWithVerticalRetrace = true; //When true, 60FSP refresh rate locked.
            GDM.GraphicsProfile = GraphicsProfile.HiDef;
            GDM.PreferredBackBufferWidth = 800;
            GDM.PreferredBackBufferHeight = 600;
            GDM.PreferMultiSampling = true; //Error in MonoGame 3.6 for DirectX, fixed in version 3.7.
            GDM.PreparingDeviceSettings += SetMultiSampling;
            GDM.ApplyChanges();
            GDM.GraphicsDevice.RasterizerState = new RasterizerState(); //Must be after Apply Changes.
            IsFixedTimeStep = true; //When true, 60FSP refresh rate locked.

            Content.RootDirectory = "Content";

            Helper.Initialize(this, GDM);
            FPSTimer = new Timer(this, 1);

            TheCamera = new Camera(this, new Vector3(0, 0, 1082), new Vector3(0, MathHelper.Pi, 0),
                GraphicsDevice.Viewport.AspectRatio, 900f, 1200f);

            TheGame = new GameLogic(this, TheCamera);
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
            Helper.ScreenHeight = 600;
            Helper.SreenWidth = 800;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SB = new SpriteBatch(GraphicsDevice);

            TheGame.LoadContent();
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
            TheGame.BeginRun();
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

            if (TheGame.CurrentMode == GameState.InPlay)
            {
                if (!OldKeyState.IsKeyDown(Keys.P) && KBS.IsKeyDown(Keys.P))
                    PauseGame = !PauseGame;
            }

            OldKeyState = Keyboard.GetState();

            if (!PauseGame)
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
            GraphicsDevice.Clear(Color.Black);

            if (NotFirstFrame)
            {
                base.Draw(gameTime);
            }
            else
            {
                NotFirstFrame = true;
            }
        }
    }
}
