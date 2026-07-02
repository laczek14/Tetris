
using System.ComponentModel;
using Raylib_cs;
using System.Numerics;

namespace tetris
{
    internal class Tetris
    {
        private const int Width = 480;
        private const int Height = 480;
        private float _pixel = 40f;
        private List<TetroManager> _tetroHold = new List<TetroManager>();
        private int _times;
        private float _speed = 50f;
        private int _boost;
        private bool _land;
        private bool _debugMostLeft;
        private bool _debugMostRight;

        public void Start()
        {
            Raylib.InitWindow(Width, Height, "tetris");
            Tetrominoes("X");
            while (!Raylib.WindowShouldClose())
            {
                Update();
                Render();
                switch (_debugMostLeft)
                {
                    case true:
                        DebugMostLeft();
                        break;
                }

                switch (_debugMostRight)
                {
                    case true:
                        DebugMostRight();
                        break;
                }
            }

            Raylib.WindowShouldClose();
        }

        void Render()
        {
            Raylib.SetTargetFPS(60);
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);
            Grid();
            foreach (TetroManager tetroholds in _tetroHold)
            {
                Pixel(tetroholds);
            }

            Raylib.EndDrawing();
        }

        void Pixel(TetroManager TetroManager)
        {
            Color Color = TetroManager.Color;
            Vector2 Position = new Vector2(TetroManager.Position.X, TetroManager.Position.Y);
            bool IsVisible = TetroManager.IsVisible;
            Vector2 V = new Vector2(_pixel, _pixel);
            Raylib.DrawRectangleV(Position, V, Color);
        }

        void DebugMostLeft()
        {
            string str = "LEFT";
            Raylib.DrawText(TetroMost(str).Position.X.ToString(), 100, 0, 60, Color.Black);
        }

        void DebugMostRight()
        {
            string str = "RIGHT";
            Raylib.DrawText(TetroMost(str).Position.ToString(), 100, 0, 60, Color.Black);
        }

        void Grid()
        {
            for (Vector2 v = new Vector2(0, 0); v.X <= Width; v.X += _pixel)
            {
                Vector2 posEnd = new Vector2(v.X, Height);
                Raylib.DrawLineV(v, posEnd, Color.LightGray);
            }

            for (Vector2 v = new Vector2(0, 0); v.Y <= Height; v.Y += _pixel)
            {
                Vector2 posEnd = new Vector2(Width, v.Y);
                Raylib.DrawLineV(v, posEnd, Color.LightGray);
            }
        }

// Drawing script for each tetromino
// TODO: make rotation inside tetromioes or through another way and left/right oriented variants
        void Tetrominoes(string tetromino)
        {
            tetromino = tetromino.ToUpper();
            switch (tetromino)
            {
                case "L":
                    L();
                    break;

                case "I":
                    I();
                    break;

                //square
                case "X":
                    X();
                    break;

            }

            void L()
            {
                Color color = Color.Red;
                bool isVisible1 = true;
                bool isVisible2 = true;
                bool isVisible3 = true;
                bool isVisible4 = true;
                bool isActive = true;
                bool mostLeft = false;
                bool mostRight = false;
                Vector2 pos = new Vector2(0, 0);
                Vector2 pos2 = new Vector2(pos.X, pos.Y + _pixel);
                Vector2 pos3 = new Vector2(pos.X, pos2.Y + _pixel);
                Vector2 pos4 = new Vector2(pos.X + _pixel, pos3.Y);
                _tetroHold.Add(new TetroManager(color, pos, isVisible1, isActive, mostLeft = true, mostRight));
                _tetroHold.Add(new TetroManager(color, pos2, isVisible2, isActive, mostLeft = true, mostRight));
                _tetroHold.Add(new TetroManager(color, pos3, isVisible3, isActive, mostLeft = true, mostRight));
                _tetroHold.Add(new TetroManager(color, pos4, isVisible4, isActive, mostLeft, mostRight = true));
            }

            void I()
            {
                Color color = Color.Yellow;
                bool isVisible1 = true;
                bool isVisible2 = true;
                bool isVisible3 = true;
                bool isVisible4 = true;
                bool isActive = true;
                bool mostLeft = true;
                bool mostRight = true;
                Vector2 pos = new Vector2(0, 0);
                Vector2 pos2 = new Vector2(pos.X, pos.Y + _pixel);
                Vector2 pos3 = new Vector2(pos.X, pos2.Y + _pixel);
                Vector2 pos4 = new Vector2(pos.X, pos3.Y + _pixel);
                _tetroHold.Add(new TetroManager(color, pos, isVisible1, isActive, mostLeft, mostRight));
                _tetroHold.Add(new TetroManager(color, pos2, isVisible2, isActive, mostLeft, mostRight));
                _tetroHold.Add(new TetroManager(color, pos3, isVisible3, isActive, mostLeft, mostRight));
                _tetroHold.Add(new TetroManager(color, pos4, isVisible4, isActive, mostLeft, mostRight));
            }

            void X()
            {
                Color color = Color.DarkGreen;
                bool isVisible1 = true;
                bool isVisible2 = true;
                bool isVisible3 = true;
                bool isVisible4 = true;
                bool isActive = true;
                bool mostLeft = false;
                bool mostRight = false;
                Vector2 pos = new Vector2(0, 0);
                Vector2 pos2 = new Vector2(pos.X + _pixel, pos.Y);
                Vector2 pos3 = new Vector2(pos.X, pos2.Y + _pixel);
                Vector2 pos4 = new Vector2(pos.X + _pixel, pos3.Y);
                _tetroHold.Add(new TetroManager(color, pos, isVisible1, isActive, mostLeft, mostRight = true));
                _tetroHold.Add(new TetroManager(color, pos2, isVisible2, isActive, mostLeft = true, mostRight));
                _tetroHold.Add(new TetroManager(color, pos3, isVisible3, isActive, mostLeft, mostRight = true));
                _tetroHold.Add(new TetroManager(color, pos4, isVisible4, isActive, mostLeft = true, mostRight));
            }


        }

        // a class for managing tetrominoes it's used in creating a private List<Tetromanager> _tetroHold
        //TLDR: stores tetrominoes states for simplicity
        public class TetroManager
        {
            public TetroManager(Color color, Vector2 position, bool isVisible, bool isActive, bool mostLeft,
                bool mostRight)
            {
                Color = color;
                Position = position;
                IsVisible = isVisible;
                IsActive = isActive;
                mostLeft = MostLeft;
                mostRight = MostRight;
            }

            public Color Color { get; }
            public Vector2 Position { get; set; }
            public bool IsVisible { get; set; }
            public bool IsActive { get; set; }
            public bool MostLeft { get; }
            public bool MostRight { get; }
        }

        // game logic
        void Update()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.RightBracket))
            {
                switch (_debugMostLeft)
                {
                    case true:
                        _debugMostLeft = false;
                        break;

                    case false:
                        _debugMostLeft = true;
                        break;
                }
            }

            if (Raylib.IsKeyPressed(KeyboardKey.LeftBracket))
            {
                switch (_debugMostRight)
                {
                    case true:
                        _debugMostRight = false;
                        break;
                    case false:
                        _debugMostRight = true;
                        break;
                }
            }

            for (int i = 0; i < _tetroHold.Count; i++)
            {
                System.Timers.Timer timer;
                var tetro = _tetroHold[i];
                switch (tetro.IsActive)
                {
                    case true:

                        timer = new System.Timers.Timer(2000);
                        timer.AutoReset = false;
                        timer.Enabled = true;
                        timer.Elapsed += Move;

                        //landing detection
                        for (int i2 = 0; i2 < _tetroHold.Count; i2++)
                        {
                            var tetromino = _tetroHold[i2];
                            float landingPos = Height - _pixel;
                            if (_land == true)
                            {
                                tetromino.IsActive = false;
                            }

                            if (tetromino.IsActive == true)
                            {
                                if (tetromino.Position.Y >= landingPos)
                                {
                                    _land = true;
                                    if (tetromino.Position.Y < landingPos)
                                    {
                                        float y = landingPos;
                                        tetromino.Position = new Vector2(tetromino.Position.X, y);
                                    }
                                }
                            }
                        }
                        /*//collision
                        //TODO: make it work
                        TetroManager TetroMost(string what)
                        {
                            what.ToUpper();
                            for (int i = 0; i < _tetroHold.Count; i++)
                            {
                                var tetro = _tetroHold[i];
                                switch (tetro.IsActive)
                                {
                                    case true:
                                        switch (what)
                                        {
                                            case "LEFT":
                                                switch (tetro.MostLeft)
                                                {
                                                    case true:
                                                        var mostLeft = _tetroHold[i];
                                                        return mostLeft;
                                                }
                                                break;
                                            case  "RIGHT":
                                                switch (tetro.MostLeft)
                                                {
                                                    case true:
                                                        var mostRight = _tetroHold[i];
                                                        return  mostRight;
                                                }
                                                break;
                                        }

                                        break;
                                }
                            }
                             TetroManager tetroManagerNull = new TetroManager(Color.Red,Vector2.Zero,false,false,false,false);
                            return tetroManagerNull;
                        }*/

                        //Movement
                        void Move(Object source, System.Timers.ElapsedEventArgs e)
                        {
                            switch (_land)
                            {
                                case false:
                                    float y = tetro.Position.Y;
                                    y += _pixel / _speed;
                                    tetro.Position = new Vector2(tetro.Position.X, y);
                                    _times++;
                                    break;
                            }
                        }

                        if (Raylib.IsKeyPressed(KeyboardKey.A) && TetroMost("RIGHT").Position.X > 0 - _pixel)
                        {
                            float X = tetro.Position.X;
                            X -= _pixel;
                            tetro.Position = new Vector2(X, tetro.Position.Y);
                        }

                        if (Raylib.IsKeyPressed(KeyboardKey.D))
                        {
                            float X = tetro.Position.X;
                            X += _pixel;
                            tetro.Position = new Vector2(X, tetro.Position.Y);
                        }

                        break;
                }
            }
        }

        //base for collision in my tetris clone
        //TODO: make it work
        TetroManager TetroMost(string what)
        {
            what.ToUpper();
            TetroManager returnval;
            for (int i = 0;i<_tetroHold.Count; i++)
            {
                var tetro = _tetroHold[i];
                if (!tetro.IsActive) break;

                if (what == "RIGHT" && tetro.MostRight)
                    return new TetroManager(tetro.Color, tetro.Position, tetro.IsVisible, tetro.IsActive, tetro.MostLeft, tetro.MostRight);

                if (what == "LEFT" && tetro.MostLeft)
                    return new TetroManager(tetro.Color, tetro.Position, tetro.IsVisible, tetro.IsActive, tetro.MostLeft, tetro.MostRight);
            }
            // nothing matched
            return new TetroManager(Color.Red, Vector2.Zero, false, false, false, false);
        }
    }
}