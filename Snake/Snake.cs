using System;
using System.Collections.Generic;

namespace Snake
{
    public class Snake
    {
        private readonly ConsoleColor _headcolor;
        private readonly ConsoleColor _bodycolor;

        public Snake(int InitialX, int InitialY, ConsoleColor headcolor, ConsoleColor bodycolor, int bodyLength = 3)
        {
            _headcolor = headcolor;
            _bodycolor = bodycolor;

            Head = new Pixel(InitialX, InitialY, _headcolor);

            for (int i = 1; i <= bodyLength; i++)
            {
                Body.Enqueue(new Pixel(InitialX - i, InitialY, _bodycolor));
            }
            Draw();
        }

        public Pixel Head { get; private set; }

        public Queue<Pixel> Body { get; } = new Queue<Pixel>();

        public void Move(Direction direction, bool eat = false)
        {
            Clear();

            if (!eat) 
            Body.Enqueue(new Pixel(Head.X, Head.Y, _bodycolor));

            if (Body.Count > Body.Count)
            {
                Body.Dequeue();
            }

            Head = direction switch
            {
                Direction.Right => new Pixel(Head.X + 1, Head.Y, _headcolor),
                Direction.Left => new Pixel(Head.X - 1, Head.Y, _headcolor),
                Direction.Up => new Pixel(Head.X, Head.Y - 1, _headcolor),
                Direction.Down => new Pixel(Head.X, Head.Y + 1, _headcolor),
            };
            Draw();
        }

        public void Draw()
        {
            Head.Draw();
            foreach (Pixel pixel in Body)
            {
                pixel.Draw();
            }
        }

        public void Clear()
        {
            Head.Clear();
            foreach (Pixel pixel in Body)
            {
                pixel.Clear();
            }
        }
    }
}

