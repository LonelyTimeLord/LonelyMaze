using System;
using SFML.Graphics;

namespace LonelyMaze
{
    class Tile
    {
        private Sprite mySprite;

        public bool Solid { get; set; }

        public Tile(bool SetSolid = false)
        {
            Solid = SetSolid;
        }

        public void Draw(RenderWindow window, int x, int y)
        {
            if (mySprite == null)
                return;

            mySprite.Position = new Vector2(x, y);
            window.Draw(mySprite);
        }

        public void SetSprite(Sprite newSprite)
        {
            mySprite = newSprite;
        }
    }
}
