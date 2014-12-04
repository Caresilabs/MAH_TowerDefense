using System;
using System.Windows.Forms;

namespace MAH_TowerDefense
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();

            using (Start game = new Start())
            {
                game.Run();
            }
        }
    }
#endif
}

