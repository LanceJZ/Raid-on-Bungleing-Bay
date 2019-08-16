using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System;
using Panther;
using Raid_on_Bungleing_Bay.Entities;

public enum GameState
{
    Over,
    InPlay,
    HighScore,
    MainMenu
};

namespace Raid_on_Bungleing_Bay
{
    class GameLogic : GameComponent
    {
        Camera CameraRef;

        GameState GameMode = GameState.InPlay;
        KeyboardState OldKeyState;

        Player ThePlayer;
        Land TheLand;

        public GameState CurrentMode { get => GameMode; }

        public GameLogic(Game game, Camera camera) : base(game)
        {
            CameraRef = camera;

            // Screen resolution is 1200 X 900.
            // Y positive is Up.
            // X positive is right of window when camera is at rotation zero.
            // Z positive is towards the camera when at rotation zero.
            // Positive rotation rotates CCW. Zero has front facing X positive. Pi/2 on Y faces Z negative.

            ThePlayer = new Player(game, camera, this);
            TheLand = new Land(game, camera, this);

            game.Components.Add(this);
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public void LoadContent()
        {

        }

        public void BeginRun()
        {

        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState KBS = Keyboard.GetState();

            if (KBS != OldKeyState)
            {

            }


            OldKeyState = Keyboard.GetState();

            base.Update(gameTime);
        }
    }
}
