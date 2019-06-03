using System;
using Minefield.Interfaces;

namespace Minefield.Services
{
    public class ConsoleService : IConsoleService
    {

        public virtual string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
