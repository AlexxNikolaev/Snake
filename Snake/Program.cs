﻿using System;
using System.Diagnostics;
using System.Linq; // Добавлено для использования метода Any
using static System.Console;

namespace Snake
{
    class Program
    {
        private const int MapWidht = 30;
        private const int MapHeight = 20;

        private const int ScreenWidth = MapWidht * 3;
        private const int ScreenHeight = MapHeight * 3;

        private const int FrameMs = 200;

        private const ConsoleColor BorderColor = ConsoleColor.Green;

        private const ConsoleColor HeadColor = ConsoleColor.Blue;
        private const ConsoleColor BodyColor = ConsoleColor.Yellow;
        private const ConsoleColor foodColor = ConsoleColor.Green;

        private static readonly Random Random = new Random();

        static void Main()
        {
            SetWindowSize(ScreenWidth, ScreenHeight);
            SetBufferSize(ScreenWidth, ScreenHeight);

            CursorVisible = false;

            while (true)
            {
                StartGame();

                Thread.Sleep(1000);
                ReadKey();
            }
        }
        
        static void StartGame()
        {
            Clear();

            DrawBorder();

            Direction currentMovement = Direction.Right;

            var snake = new Snake(10, 5, HeadColor, BodyColor);

            Pixel food = GenFood(snake);
            food.Draw();

            Stopwatch sw = new Stopwatch();

            while (true)
            {
                sw.Restart();

                Direction oldMovement = currentMovement;

                while (sw.ElapsedMilliseconds <= FrameMs)
                {
                    if (currentMovement == oldMovement)
                    {
                        currentMovement = ReadMovement(currentMovement);
                    }
                }
                
                if (snake.Head.X == food.X && snake.Head.Y == food.Y)
                {
                    snake.Move(currentMovement, true);

                    food=GenFood(snake);
                    food.Draw();
                }
                else
                {
                    snake.Move(currentMovement);
                }

                if (snake.Head.X == MapWidht - 1
                || snake.Head.Y == MapHeight - 1
                || snake.Head.X == 0
                || snake.Head.Y == 0
                || snake.Body.Any(b => b.X == snake.Head.X && b.Y == snake.Head.Y))
                    break;
            }

            snake.Clear();

            SetCursorPosition(ScreenWidth / 3, ScreenHeight / 3);
            WriteLine("Game Over Press Enter");
        }

        static Pixel GenFood(Snake snake)
        {
            Pixel food;

            do
            {
                food = new Pixel(Random.Next(1, MapWidht - 2), Random.Next(1, MapHeight - 2), foodColor);
            } while (snake.Head.X == food.X && snake.Head.Y == food.Y
              || snake.Body.Any(b=>b.X== food.X && b.Y == food.Y));

            return food;
        }
        static Direction ReadMovement(Direction currentDirection)
        {
            if (!KeyAvailable)
                return currentDirection;

            ConsoleKeyInfo keyInfo = ReadKey(true);

            currentDirection = keyInfo.Key switch
            {
                ConsoleKey.UpArrow when currentDirection != Direction.Down => Direction.Up,
                ConsoleKey.DownArrow when currentDirection != Direction.Up => Direction.Down,
                ConsoleKey.LeftArrow when currentDirection != Direction.Right => Direction.Left,
                ConsoleKey.RightArrow when currentDirection != Direction.Left => Direction.Right,
                _ => currentDirection
            };

            return currentDirection;
        }

        static void DrawBorder()
        {
            for (int i = 0; i < MapWidht; i++)
            {
                new Pixel(i, 0, BorderColor).Draw();
                new Pixel(i, MapHeight - 1, BorderColor).Draw();
            }

            for (int i = 0; i < MapHeight; i++)
            {
                new Pixel(0, i, BorderColor).Draw();
                new Pixel(MapWidht - 1, i, BorderColor).Draw();
            }
        }
    }
}

