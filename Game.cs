
using System.ComponentModel.DataAnnotations;
using Raylib_cs;
using System.Numerics;
using System.Timers;
namespace tetris
{
    internal class Tetris
    {
        private const int Width = 480;
        private const int Height = 480;
        private float pixel = 40f;
        private List<TetroManager> TetroHold = new List<TetroManager>();
        private int times;
        private float speed = 50f;
        private int boost;
        private bool land = false;
        private bool debugMostLeft = false;
        private bool debugMostRight = false;
        private float mostRightX = 0f;

        public void Start()
        {
            Raylib.InitWindow(Width, Height, "tetris");
            Tetrominoes("X");
            while (!Raylib.WindowShouldClose())
            {
                Update();
                Render();
                switch (debugMostLeft)
                {
                    case true:
                        DebugMostLeft();
                        break;
                }

                switch (debugMostRight)
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
            foreach (TetroManager tetroholds in TetroHold)
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
            Vector2 V = new Vector2(pixel, pixel);
            Raylib.DrawRectangleV(Position, V, Color);
        }

        void DebugMostLeft()
        {
            for (int i = 0; i < TetroHold.Count; i++)
            {
                var tetro = TetroHold[i];
                switch (tetro.IsActive)
                {
                    case true:
                        switch (tetro.MostLeft)
                        {
                            case true:
                                float x = tetro.Position.X;
                                Raylib.DrawText(x.ToString(), 100, 0, 60, Color.Black);
                                break;
                        }
                        break;
                }
            }
        }

        void DebugMostRight()
        {
            for (int i = 0; i < TetroHold.Count; i++)
            {
                var tetro = TetroHold[i];
                switch (tetro.IsActive)
                {
                    case true:
                        switch (tetro.MostRight)
                        {
                            case true:
                                 mostRightX = tetro.Position.X;
                                break;
                        }
                        break;
                }
                Raylib.DrawText(mostRightX.ToString(), 100, 0, 60, Color.Black);
            }
        }

        void Grid()
        {
            for (Vector2 V = new Vector2(0, 0); V.X <= Width; V.X += pixel)
            {
                Vector2 posEnd = new Vector2(V.X, Height);
                Raylib.DrawLineV(V, posEnd, Color.LightGray);
            }

            for (Vector2 v = new Vector2(0, 0); v.Y <= Height; v.Y += pixel)
            {
                Vector2 posEnd = new Vector2(Width, v.Y);
                Raylib.DrawLineV(v, posEnd, Color.LightGray);
            }
        }

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
            Vector2 pos2 = new Vector2(pos.X, pos.Y + pixel);
            Vector2 pos3 = new Vector2(pos.X, pos2.Y + pixel);
            Vector2 pos4 = new Vector2(pos.X + pixel, pos3.Y);
            TetroHold.Add(new TetroManager(color, pos, isVisible1,isActive,mostLeft = true, mostRight));
            TetroHold.Add(new TetroManager(color, pos2, isVisible2,isActive,mostLeft = true, mostRight));
            TetroHold.Add(new TetroManager(color, pos3, isVisible3,isActive,mostLeft = true, mostRight));
            TetroHold.Add(new TetroManager(color, pos4, isVisible4,isActive,mostLeft, mostRight = true));
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
            Vector2 pos2 = new Vector2(pos.X, pos.Y + pixel);
            Vector2 pos3 = new Vector2(pos.X, pos2.Y + pixel);
            Vector2 pos4 = new Vector2(pos.X, pos3.Y + pixel);
            TetroHold.Add(new TetroManager(color, pos, isVisible1,isActive,mostLeft, mostRight));
            TetroHold.Add(new TetroManager(color, pos2, isVisible2,isActive,mostLeft, mostRight));
            TetroHold.Add(new TetroManager(color, pos3, isVisible3,isActive,mostLeft, mostRight));
            TetroHold.Add(new TetroManager(color, pos4, isVisible4,isActive,mostLeft, mostRight));
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
            Vector2 pos2 = new Vector2(pos.X + pixel, pos.Y);
            Vector2 pos3 = new Vector2(pos.X, pos2.Y + pixel);
            Vector2 pos4 = new Vector2(pos.X + pixel, pos3.Y);
            TetroHold.Add(new TetroManager(color, pos, isVisible1,isActive,mostLeft, mostRight = true));
            TetroHold.Add(new TetroManager(color, pos2, isVisible2,isActive,mostLeft = true, mostRight));
            TetroHold.Add(new TetroManager(color, pos3, isVisible3,isActive,mostLeft, mostRight = true));
            TetroHold.Add(new TetroManager(color, pos4, isVisible4,isActive,mostLeft = true, mostRight));
        }
        
        
    }

    public class TetroManager
    {
         public TetroManager(Color color, Vector2 position, bool isVisible, bool isActive,bool mostLeft,bool mostRight)
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
            switch (debugMostLeft)
            {
                case true:
                    debugMostLeft = false;
                    break;
                
                case false:
                    debugMostLeft = true;
                    break;
            } 
        }

        if (Raylib.IsKeyPressed(KeyboardKey.LeftBracket))
        {
            switch (debugMostRight)
            {
                case true:
                    debugMostRight = false;
                    break;
                case false:
                    debugMostRight = true;
                    break;
            }
        }
        
        for (int i = 0; i < TetroHold.Count; i++)
        {
            System.Timers.Timer timer;
            var tetro = TetroHold[i];
            switch (tetro.IsActive)
            {
                case true:
                    
                        timer = new System.Timers.Timer(2000);
                        timer.AutoReset = false;
                        timer.Enabled = true;
                        timer.Elapsed += Move;
                        
                        //landing detection
                        for (int i2 = 0; i2 < TetroHold.Count; i2++)
                        {
                            var tetromino = TetroHold[i2];
                            float landingPos = Height - pixel;
                            if (land == true)
                            {
                                tetromino.IsActive = false;
                            }

                            if (tetromino.IsActive == true)
                            {
                                if (tetromino.Position.Y >= landingPos)
                                {
                                    land = true;
                                    if (tetromino.Position.Y < landingPos)
                                    {
                                        float y = landingPos;
                                        tetromino.Position = new Vector2(tetromino.Position.X, y);
                                    }
                                }
                            }
                        }
                        
                        TetroManager TetroMost(string what)
                        {
                            what.ToUpper();
                            for (int i = 0; i < TetroHold.Count; i++)
                            {
                                var tetro = TetroHold[i];
                                switch (tetro.IsActive)
                                {
                                    case true:
                                        switch (what)
                                        {
                                            case "LEFT":
                                                switch (tetro.MostLeft)
                                                {
                                                    case true:
                                                        var mostLeft = TetroHold[i];
                                                        return mostLeft;
                                                        break;
                                                }
                                                break;
                                            case  "RIGHT":
                                                switch (tetro.MostLeft)
                                                {
                                                    case true:
                                                        var mostRight = TetroHold[i];
                                                        return  mostRight;
                                                }
                                                break;
                                        }

                                        break;
                                }
                            }
                             TetroManager tetroManage = new TetroManager(Color.Red,Vector2.Zero,false,false,false,false);
                            return tetroManage;
                        }
                        
                //Movement
                void Move(Object source, ElapsedEventArgs e)
                {
                    switch (land)
                    {
                        case false:
                        float y = tetro.Position.Y;
                        y += pixel / speed;
                        tetro.Position = new Vector2(tetro.Position.X, y);
                        times++;
                        break;
                    }
                }

                if (Raylib.IsKeyPressed(KeyboardKey.A))
                {
                    float X = tetro.Position.X;
                    X -= pixel;
                    tetro.Position = new Vector2(X, tetro.Position.Y);
                }
                        
                if (Raylib.IsKeyPressed(KeyboardKey.D))
                {
                    float X = tetro.Position.X;
                    X += pixel;
                    tetro.Position = new Vector2(X, tetro.Position.Y);
                }
                        
                break;
            }
        }
    }
    }
}