using System;
using SFML.Graphics;
using System.Windows.Forms;

namespace LonelyMaze
{
    class Util
    {
        public static Image LoadImage(string filepath)
        {
            bool failed;
            return LoadImage(filepath, out failed);
        }

        public static Image LoadImage(string filepath, out bool Failed)
        {
            Image Temp;

            try
            {
                Temp = new Image(filepath);
            }
            catch
            {
                MessageBox.Show("Failed to load file: " + filepath);
                Failed = true;
                return new Image(16, 16, Color.Black);
            }

            Failed = false;
            return Temp;
        }
    }
}
