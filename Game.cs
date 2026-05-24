
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
     private int speed = 50;
     private int boost;
     private bool land = false;
     private bool debug = false;
    public void Start()
    {
        Raylib.InitWindow(Width, Height, "tetris");
        Tetrominoes("X");
        while (!Raylib.WindowShouldClose())
        {
            Update();
            Render();
            if (debug == true)
            {
              Debug();  
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

    void Debug()
    {
        for (int i = 0; i < TetroHold.Count; i++)
        {
            var tetro = TetroHold[i];
            Vector2 pos = tetro.Position;
            Raylib.DrawText(pos.Y.ToString(),100,0,60,Color.Black);
        }
    }
    
    void Grid()
    {
        for (Vector2 V = new Vector2(0, 0); V.X <= Width; V.X += pixel)
        {
            Vector2 Pend = new Vector2(V.X, Height);
            Raylib.DrawLineV(V, Pend, Color.LightGray);
        }

        for (Vector2 v = new Vector2(0, 0); v.Y <= Height; v.Y += pixel)
        {
            Vector2 pend = new Vector2(Width, v.Y);
            Raylib.DrawLineV(v, pend, Color.LightGray);
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
            Vector2 pos = new Vector2(0, 0);
            Vector2 pos2 = new Vector2(pos.X, pos.Y + pixel);
            Vector2 pos3 = new Vector2(pos.X, pos2.Y + pixel);
            Vector2 pos4 = new Vector2(pos.X + pixel, pos3.Y);
            TetroHold.Add(new TetroManager(color, pos, isVisible1,isActive));
            TetroHold.Add(new TetroManager(color, pos2, isVisible2,isActive));
            TetroHold.Add(new TetroManager(color, pos3, isVisible3,isActive));
            TetroHold.Add(new TetroManager(color, pos4, isVisible4,isActive));
        }
        
        void I()
        {
            Color color = Color.Yellow;
            bool isVisible1 = true;
            bool isVisible2 = true;
            bool isVisible3 = true;
            bool isVisible4 = true;
            bool isActive = true;
            Vector2 pos = new Vector2(0, 0);
            Vector2 pos2 = new Vector2(pos.X, pos.Y + pixel);
            Vector2 pos3 = new Vector2(pos.X, pos2.Y + pixel);
            Vector2 pos4 = new Vector2(pos.X, pos3.Y + pixel);
            TetroHold.Add(new TetroManager(color, pos, isVisible1,isActive));
            TetroHold.Add(new TetroManager(color, pos2, isVisible2,isActive));
            TetroHold.Add(new TetroManager(color, pos3, isVisible3,isActive));
            TetroHold.Add(new TetroManager(color, pos4, isVisible4,isActive));
        }

        void X()
        {
            Color color = Color.DarkGreen;
            bool isVisible1 = true;
            bool isVisible2 = true;
            bool isVisible3 = true;
            bool isVisible4 = true;
            bool isActive = true;
            Vector2 pos = new Vector2(0, 0);
            Vector2 pos2 = new Vector2(pos.X + pixel, pos.Y);
            Vector2 pos3 = new Vector2(pos.X, pos2.Y + pixel);
            Vector2 pos4 = new Vector2(pos.X + pixel, pos3.Y);
            TetroHold.Add(new TetroManager(color, pos, isVisible1,isActive));
            TetroHold.Add(new TetroManager(color, pos2, isVisible2,isActive));
            TetroHold.Add(new TetroManager(color, pos3, isVisible3,isActive));
            TetroHold.Add(new TetroManager(color, pos4, isVisible4,isActive));
        }
    }

    public class TetroManager
    {
        public TetroManager(Color color, Vector2 position, bool isVisible, bool isActive)
        {
            Color = color;
            Position = position;
            IsVisible = isVisible;
            IsActive = isActive;
        }
        
        
        public Color Color { get; }
        public Vector2 Position { get; set; }
        public bool IsVisible { get; set; }
        public bool IsActive { get; set; }
    }
    //problem with implementing a timer 
    void Update() 
    {
        if (Raylib.IsKeyPressed(KeyboardKey.RightBracket))
        {
            switch (debug)
            {
                case true:
                    debug = false;
                    break;
                
                case false:
                    debug = true;
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
                            if (tetromino.IsActive)
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
                        
                //Movement
                void Move(Object source, ElapsedEventArgs e)
                {
                    if (land == false)
                    {
                        float y = tetro.Position.Y;
                        y += pixel/speed;
                        tetro.Position = new Vector2(tetro.Position.X, y);
                        times++;
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