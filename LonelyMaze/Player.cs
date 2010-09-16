using System;
using SFML.Graphics;

namespace LonelyMaze
{
    class Player
    {
        private const int mySpeed = 4;
        private int myX, myY;
        private int myTargetX, myTargetY;
        private Sprite mySprite;
        
        public Player(Sprite sprite, int x, int y)
        {
            mySprite = sprite;
            SetPosition(x, y);
        }

        public void SetPosition(int x, int y)
        {
            //myX = x;
            //myY = y;
            myTargetX = x;
            myTargetY = y;
        }

        public int GetX()
        {
            return myTargetX;
        }

        public int GetY()
        {
            return myTargetY;
        }

        public int GetRealX()
        {
            return myX;
        }

        public int GetRealY()
        {
            return myY;
        }

        public void Move(int x, int y)
        {
            myTargetX += x;
            myTargetY += y;
        }

        public void Draw(RenderWindow Window)
        {
            mySprite.Position = new Vector2((float)myX, (float)myY);
            Window.Draw(mySprite);
        }

        public void Update()
        {
            if (myX < myTargetX)
                myX += mySpeed;
            else if (myX > myTargetX)
                myX -= mySpeed;

            if (myY < myTargetY)
                myY += mySpeed;
            else if (myY > myTargetY)
                myY -= mySpeed;
        }
    }
}
