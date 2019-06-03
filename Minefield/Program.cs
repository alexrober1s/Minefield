using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Minefield.Classes;
using Minefield.Helpers;
using Minefield.Interfaces;
using Minefield.Services;

namespace Minefield
{
    class Program
    {
        static void Main(string[] args)
        {
            // setup ioc
            var container = new WindsorContainer();
            container.Register(Component.For<IConsoleService>().ImplementedBy<ConsoleService>().LifestyleSingleton());
            Ioc.Container = container;

            // run the game
            Game.Init(container.Resolve<IConsoleService>(), new GameController()).Run();
        }
    }
}
