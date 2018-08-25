using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Threading;

namespace SnakeGame
{
    struct Position
    {
        public int row;
        public int col;
        public Position(int col, int row)
        {
            this.row = col;
            this.col = row;
        }
    }
    class snake
    {
        static void Main(string[] args)
        {
            byte right = 0;
            byte left = 1;
            byte down = 2;
            byte top = 3;

            Position[] directions = new Position[]
            {
                new Position(0, 1), // right
                new Position(0, -1), // left
                new Position(1, 0), // down
                new Position(-1, 0) // top
            };

            int speedSnake = 100;
            int direction = right; //0
            Random randomNumbersGenerator = new Random();
            Console.BufferHeight = Console.WindowHeight;
            Position food = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight),
                randomNumbersGenerator.Next(0, Console.WindowWidth));

            Console.SetCursorPosition(food.col, food.row);
            Console.Write("@");
            //Console.BufferWidth = Console.WindowWidth;

            Queue<Position> snakeElements = new Queue<Position>();
            for (int i = 0; i <= 5; i++)
            {
                snakeElements.Enqueue(new Position(0, i)); //stroke snake start position
            }

            foreach (Position postion in snakeElements)
            {
                Console.SetCursorPosition(postion.col, postion.row);
                Console.Write("*");
            }

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userInput = Console.ReadKey();
                    if (userInput.Key == ConsoleKey.LeftArrow)
                    {
                        if (direction != right) direction = left;
                    }
                    if (userInput.Key == ConsoleKey.RightArrow)
                    {
                        if (direction != left) direction = right;
                    }
                    if (userInput.Key == ConsoleKey.DownArrow)
                    {
                        if (direction != top) direction = down;
                    }
                    if (userInput.Key == ConsoleKey.UpArrow)
                    {
                        if (direction != down) direction = top;
                    }
                }
                Position snakeHead = snakeElements.Last();
                Position newDirection = directions[direction];
                Position snakeNewHead = new Position(snakeHead.row + newDirection.row, snakeHead.col + newDirection.col);

                if (snakeNewHead.col < 0 ||
                    snakeNewHead.row < 0 ||
                    snakeNewHead.row >= Console.WindowHeight ||
                    snakeNewHead.col >= Console.WindowWidth ||
                    snakeElements.Contains(snakeNewHead)
                    )
                {
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("Game Over!");
                    Console.WriteLine("Your score are: {0}", (snakeElements.Count() - 6) * 100);
                    return;
                }

                snakeElements.Enqueue(snakeNewHead);
                Console.SetCursorPosition(snakeNewHead.col, snakeNewHead.row);
                Console.Write("*");

                if (snakeNewHead.col == food.col && snakeNewHead.row == food.row)
                {
                    // feeding snake
                    food = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight),
               randomNumbersGenerator.Next(0, Console.WindowWidth));
                    //Console.SetCursorPosition(food.col, food.row);
                    //Console.Write("@");
                    speedSnake--;
                }
                else
                {

                    // moving ...
                    snakeElements.Dequeue();
                    //Position lastTailElement =  snakeElements.Dequeue();
                    //Console.SetCursorPosition(lastTailElement.col, lastTailElement.row);
                    //Console.Write(" "); 
                }



                Console.Clear();

                foreach (Position postion in snakeElements)
                {
                    Console.SetCursorPosition(postion.col, postion.row);
                    Console.Write("*");
                }

                Console.SetCursorPosition(food.col, food.row);
                Console.Write("@");


                Thread.Sleep(speedSnake);
            }
        }
    }
}
