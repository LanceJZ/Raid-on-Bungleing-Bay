using System;

namespace Raid_on_Bungleing_Bay
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
        static void Main()
        {
            using (Panther game = new Panther())
                game.Run();
        }
    }
}
