using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System;
using Panther;
using Raid_on_Bungleing_Bay.Entities;
using Raid_on_Bungleing_Bay.Controllers;

// Hide/Show regions.
// Ctrl-M, Ctrl-O will collapse all of the code to its definitions.
// Ctrl-M, Ctrl-L will expand all of the code(actually, this one toggles it).
public enum GameState
{
    Over,
    InPlay,
    HighScore,
    MainMenu
};

namespace Raid_on_Bungleing_Bay
{
    public class GameLogic : GameComponent
    {
        Camera _camera;

        GameState _mode = GameState.InPlay;
        public NumberGenerator _score;

        public Land _land;
        Factories _factories;
        Tanks _tanks;
        Guns _guns;
        Radars _radars;
        JetPlane TheJet;
        public Player _player;
        bool _devMode = true;
        MapCross _mapCross;

        public GameState CurrentMode { get => _mode; }

        public GameLogic(Game game, Camera camera) : base(game)
        {
            _camera = camera;

            // Ctrl+M, Ctrl+L to show regions.
            // Screen resolution is 1200 X 900.
            // [Y] positive is Up.
            // [X] positive is right of window when camera is at rotation zero.
            // [Z] positive is towards the camera when at rotation zero.
            // [X] and [Y] zero is center of screen.
            // Positive rotation rotates CCW. Zero has front facing X positive. Pi/2 on Y faces Z negative.

            _score = new NumberGenerator(game);
            _land = new Land(game, camera, this);
            _player = new Player(game, camera, this);
            _factories = new Factories(game, camera, this);
            _guns = new Guns(game, camera, this);
            _radars = new Radars(game, camera, this);
            _tanks = new Tanks(game, camera, this);
            TheJet = new JetPlane(game, camera, this);

            _mapCross = new MapCross(game, camera);

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
            Helper.BeginKeyPressed();

            if (Helper.KeyPressed(Keys.O))
            {
                _player.Enabled = !_player.Enabled;
                _mapCross.Enabled = !_mapCross.Enabled;
                _land.ToggleMirror(_player.Enabled);
            }

            Helper.EndKeyPressed();
            base.Update(gameTime);
        }
    }
}
