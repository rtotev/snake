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
            byte up = 3;
            int lastFoodTime = 0;
            int foodDissapearTime = 8000;
            int negativePosints = 0; 
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
            lastFoodTime = Environment.TickCount;

            Console.SetCursorPosition(food.col, food.row);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("@");
            Console.ForegroundColor = ConsoleColor.DarkGreen;

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
                        if (direction != up) direction = down;
                    }
                    if (userInput.Key == ConsoleKey.UpArrow)
                    {
                        if (direction != down) direction = up;
                    }
                }
                Position snakeHead = snakeElements.Last();
                Position newDirection = directions[direction];
                Position snakeNewHead = new Position(snakeHead.row + newDirection.row, snakeHead.col + newDirection.col);

                if (snakeNewHead.col < 0) snakeNewHead.col = Console.WindowWidth - 1;
                if (snakeNewHead.row < 0) snakeNewHead.row = Console.WindowHeight - 1;
                if (snakeNewHead.col >= Console.WindowWidth) snakeNewHead.col = 0;
                if (snakeNewHead.row >= Console.WindowHeight) snakeNewHead.row = 0;


                //if (snakeNewHead.col < 0 ||
                //    snakeNewHead.row < 0 ||
                //    snakeNewHead.row >= Console.WindowHeight ||
                //    snakeNewHead.col >= Console.WindowWidth ||
                if (snakeElements.Contains(snakeNewHead) )
                {
                    Console.SetCursorPosition(0, 0);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Game Over!");
                    Console.WriteLine();
                    int score = ((snakeElements.Count() - 6) * 100);
                    if (score >= negativePosints)
                    {
                        score -= negativePosints;
                    }                     
                    Console.WriteLine("Your score are: {0}", score);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    return;
                }

                Console.SetCursorPosition(snakeHead.col, snakeHead.row);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("*");

                snakeElements.Enqueue(snakeNewHead);
                Console.SetCursorPosition(snakeNewHead.col, snakeNewHead.row);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("*");

                if (snakeNewHead.col == food.col && snakeNewHead.row == food.row)
                {
                    // feeding snake
                    do
                    {
                        food = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight),
                            randomNumbersGenerator.Next(0, Console.WindowWidth));
                    } while (snakeElements.Contains(food));
                    lastFoodTime = Environment.TickCount;
                    Console.SetCursorPosition(food.col, food.row);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("@");
                    Console.ForegroundColor = ConsoleColor.White; ;
                    speedSnake--;
                }
                else
                {

                    // moving ...
                    //snakeElements.Dequeue();
                    Position lastTailElement =  snakeElements.Dequeue();
                    Console.SetCursorPosition(lastTailElement.col, lastTailElement.row);
                    Console.Write(" "); 
                }



                //Console.Clear();
                /*
                foreach (Position postion in snakeElements)
                {
                    Console.SetCursorPosition(postion.col, postion.row);
                    Console.Write("*");
                }
                 */

                if (Environment.TickCount - lastFoodTime >= foodDissapearTime)
                {
                    negativePosints += 20;
                    Console.SetCursorPosition(food.col, food.row);
                    Console.Write(" ");

                    do
                    {
                        food = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight),
                            randomNumbersGenerator.Next(0, Console.WindowWidth));
                    } while (snakeElements.Contains(food));
                    lastFoodTime = Environment.TickCount;
                   
                }
                
                Console.SetCursorPosition(food.col, food.row);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("@");
                Console.ForegroundColor = ConsoleColor.White;


                Thread.Sleep(speedSnake);
            }
        }
    }
}
