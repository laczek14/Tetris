using Raylib_cs;
using System.Numerics;
namespace tetris
{
    internal class Tetris 
    {
        public const int Width = 480;
        public const int Height = 480;
        public int pixel = 40;
        public List<TetroManager> TetroHold = new List<TetroManager>();
        public void Start()
        {
            Raylib.InitWindow(Width, Height, "tetris");
            while (!Raylib.WindowShouldClose())
            {
                Render();
            }
            Raylib.WindowShouldClose();
        }

        void Render() 
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);
            Grid();
            foreach (TetroManager tetroholds in TetroHold)
            {
                Pixel(tetroholds);   
            }
            Raylib.EndDrawing();
        }

        void Pixel(TetroManager Info = new TetroManager())
        {
            Color Color = Info.Color;
            Vector2 Position = new Vector2(Info.Position.X, Info.Position.Y);
            bool IsVisible = Info.IsVisible;
            Vector2 V = new Vector2(pixel, pixel);
            Raylib.DrawRectangleV(Position, V, Color);
        }

        void Grid()
        {
            for (Vector2 V = new Vector2(0, 0); V.X <= Width; V.X += pixel)
            {
                Vector2 Pend = new Vector2(V.X, Height);
                Raylib.DrawLineV(V, Pend, Color.LightGray);
            }

            for (Vector2 V = new Vector2(0, 0); V.Y <= Height; V.Y += pixel)
            {
                Vector2 Pend = new Vector2(Width, V.Y);
                Raylib.DrawLineV(V, Pend, Color.LightGray);
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
               TetroHold.Add(new TetroManager(color,pos,isVisible1,isActive));
               TetroHold.Add(new TetroManager(color,pos2,isVisible2,isActive));
               TetroHold.Add(new TetroManager(color,pos3,isVisible3,isActive));
               TetroHold.Add(new TetroManager(color,pos4,isVisible4,isActive));
            }
        }

        public struct TetroManager
        {
            public TetroManager(Color color,Vector2 position,bool isVisible, bool isActive)
            {
                Color = color;
                Position = position;
                IsVisible = isVisible;
                IsActive = isActive;
            }

            public Color Color{get;}
            public Vector2 Position { get; }
            public bool IsVisible { get; }
            public bool IsActive { get; }
        }
    }
}
