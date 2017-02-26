using System;

namespace YetiAdventure.Desktop
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Initalize();
            using (var game = new YetiGame(bootstrapper.Container))
                game.Run();
        }

        

    }
}
