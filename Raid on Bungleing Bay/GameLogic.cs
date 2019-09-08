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
        Camera _camera;

        GameState _mode = GameState.InPlay;
        KeyboardState _oldKeyState;
        NumberGenerator _score;

        Land _land;
        Factory TheFactory;
        Tank TheTank;
        Machinegun TheMachinegun;
        Radar TheRadar;
        JetPlane TheJet;
        public Player _player;

        public GameState CurrentMode { get => _mode; }

        public GameLogic(Game game, Camera camera) : base(game)
        {
            _camera = camera;

            // Screen resolution is 1200 X 900.
            // [Y] positive is Up.
            // [X] positive is right of window when camera is at rotation zero.
            // [Z] positive is towards the camera when at rotation zero.
            // [X] and [Y] zero is center of screen.
            // Positive rotation rotates CCW. Zero has front facing X positive. Pi/2 on Y faces Z negative.

            _score = new NumberGenerator(game);
            _land = new Land(game, camera, this);
            _player = new Player(game, camera, this);
            TheFactory = new Factory(game, camera, this);
            TheTank = new Tank(game, camera, this);
            TheMachinegun = new Machinegun(game, camera, this);
            TheRadar = new Radar(game, camera, this);
            TheJet = new JetPlane(game, camera, this);

            game.Components.Add(this);
        }

        public override void Initialize()
        {
            _score.Position.Y = 300;
            _score.Position.X = -125;

            base.Initialize();
        }

        public void LoadContent()
        {

        }

        public void BeginRun()
        {
            _score.Number = 666;

        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState KBS = Keyboard.GetState();

            if (KBS != _oldKeyState)
            {
                if (Helper.KeyPressed(Keys.P))
                {
                    System.Diagnostics.Debug.WriteLine("Location is " + _player.Position.ToString());
                }

            }

            _oldKeyState = Keyboard.GetState();

            base.Update(gameTime);
        }
    }
}
