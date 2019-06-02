using System;
using Minefield.Model;

namespace Minefield.Classes
{   
    public class Game
    {
        const string EXIT = "exit";
        const string UP = "u";
        const string DOWN = "d";
        const string LEFT = "l";
        const string RIGHT = "r";
        const string HELP = "To move position type: \r\nu (up)\r\nd (down)\r\nl (left)\r\nr (right).";

        readonly GameController _gameController;

        static Game _instance;
        public static Game Instance => _instance = _instance ?? new Game();
    
        Game()
        {
            _gameController = new GameController();
            PrintWelcomeMessage();
        }

        public void Run()
        {
            PrintHelp();
            PrintPlayerPosition();
            string line;
            while((line = Console.ReadLine()) != null)
            {
                switch(line.ToLower())
                {
                    case EXIT:
                        Exit();
                        return;
                    case UP:
                        Console.Write(_gameController.Move(GameController.Movement.Up));
                        break;
                    case DOWN:
                        Console.Write(_gameController.Move(GameController.Movement.Down));
                        break;
                    case LEFT:
                        Console.Write(_gameController.Move(GameController.Movement.Left));
                        break;
                    case RIGHT:
                        Console.Write(_gameController.Move(GameController.Movement.Right));
                        break;
                    default:
                        PrintHelp();
                        PrintPlayerPosition();
                        break;
                }

                Console.WriteLine();

                if (_gameController.GameOver)
                    break;
            }

            Exit();
        }

        void Exit()
        {
            Console.WriteLine(string.Format("Score:{0}", _gameController.Score));
            Console.WriteLine("Goodbye");
            Environment.Exit(1);
        }

        void PrintWelcomeMessage()
        {
            Console.WriteLine("********************************************************************");
            Console.WriteLine("WELCOME TO MINEFIELD!\r\n");
            Console.WriteLine("Starting at the bottom, move to the top of the board without hitting any mines.\r\n");
            Console.WriteLine("********************************************************************");
        }

        void PrintHelp()
        {
            Console.WriteLine();
            Console.WriteLine(HELP);
        }

        void PrintPlayerPosition()
        {
            Console.WriteLine();
            Console.WriteLine(string.Format("Player at position: {0}", BoardSquare.ToDisplayPosition(_gameController.PlayerPosition)));
        }
    }
}
