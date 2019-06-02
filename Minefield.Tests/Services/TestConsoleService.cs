using System.Collections.Generic;
using Minefield.Interfaces;
using Minefield.Services;

namespace Tests.Services
{
    // can be used to test Game I/O
    public class TestConsoleService : ConsoleService, IConsoleService
    {
        Queue<string> _lines;

        public TestConsoleService(List<string> lines)
        {
            _lines = new Queue<string>(lines);
        }

        public override string ReadLine()
        {
            if (_lines.Count > 0)
            {
                return _lines.Dequeue();
            }

            return string.Empty;
        }
    }
}
