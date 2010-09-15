using System;
using SFML.Graphics;
using SFML.Window;

namespace LonelyMaze
{
    class Program
    {
        private static Level TestLevel = new Level(512 / 16, 512 / 16, 16, 16);

        static void OnClose(object sender, EventArgs e)
        {
            ((RenderWindow)sender).Close();
        }

        static void Render(RenderWindow window)
        {
            window.Clear();
            TestLevel.Draw(window);
            window.Display();
        }

        static void KeyPress(object sender, KeyEventArgs e)
        {
            TestLevel.HandleInput(e);
        }

        static void Main(string[] args)
        {
            RenderWindow window = new RenderWindow(new VideoMode(512, 512), "LonelyMaze", Styles.Close);

            // init
            window.Closed += new EventHandler(OnClose);
            window.KeyPressed += new EventHandler<KeyEventArgs>(KeyPress);

            TestLevel.LoadFromFile(@"resources\testmap.png");

            while (window.IsOpened())
            {
                window.DispatchEvents();

                TestLevel.Update();

                Render(window);
            }
        }
    }
}
