using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;

namespace LonelyMaze
{
    class Level
    {
        private Image wallimage, floorimage, spawnimage, playerimage, errorimage, keyimage;
        private Sprite wallsprite, floorsprite, spawnsprite, errorsprite, keysprite;
        private List<Tile> myTiles;
        private int myHeight, myWidth, myTileWidth, myTileHeight;
        private Player myPlayer;

        public Level(int xTiles, int yTiles, int TileWidth, int TileHeight)
        {
            myWidth = xTiles;
            myHeight = yTiles;
            myTileWidth = TileWidth;
            myTileHeight = TileHeight;

            myTiles = new List<Tile>();
            for (int i = 0; i < xTiles * yTiles; i++)
            {
                myTiles.Add(new Tile());
            }

            wallimage = Util.LoadImage(@"resources\solid.png");
            wallsprite = new Sprite(wallimage);
            floorimage = Util.LoadImage(@"resources\floor.png");
            floorsprite = new Sprite(floorimage);
            spawnimage = Util.LoadImage(@"resources\spawn.png");
            spawnsprite = new Sprite(spawnimage);
            errorimage = Util.LoadImage(@"resources\error.png");
            errorsprite = new Sprite(errorimage);
            keyimage = Util.LoadImage(@"resources\key.png");
            keysprite = new Sprite(keyimage);
            playerimage = Util.LoadImage(@"resources\player.png");

            myPlayer = new Player(new Sprite(playerimage), 0, 0);
        }

        public Tile GetTile(int x, int y)
        {
            return myTiles[x + myWidth * y];
        }

        public void LoadFromFile(string filepath)
        {
            bool failed;
            Image LevelImage = Util.LoadImage(filepath, out failed);

            if (failed)
            {
                return;
            }

            for (int x = 0; x < LevelImage.Width; x++)
            {
                for (int y = 0; y < LevelImage.Height; y++)
                {
                    Color colour = LevelImage.GetPixel((uint)x, (uint)y);

                    if (colour.Equals(Color.Black))
                    {
                        myTiles[x + myWidth * y].Solid = true;
                        myTiles[x + myWidth * y].SetSprite(wallsprite);
                    }
                    else if (colour.Equals(Color.White))
                    {
                        myTiles[x + myWidth * y].Solid = false;
                        myTiles[x + myWidth * y].SetSprite(floorsprite);
                    }
                    else if (colour.Equals(Color.Green))
                    {
                        myTiles[x + myWidth * y].Solid = false;
                        myTiles[x + myWidth * y].SetSprite(spawnsprite);
                        myPlayer.SetPosition(x * myTileWidth, y * myTileHeight);
                    }
                    else if (colour.Equals(Color.Blue))
                    {
                        myTiles[x + myWidth * y].Solid = false;
                        myTiles[x + myWidth * y].SetSprite(keysprite);
                    }
                    else
                    {
                        myTiles[x + myWidth * y].Solid = false;
                        myTiles[x + myWidth * y].SetSprite(errorsprite);
                    }
                }
            }
        }

        public void Draw(RenderWindow window)
        {
            int x = 0;
            int y = 0;
            for (int i = 0; i < myTiles.Count; i++)
            {
                y = (int)Math.Floor((double)(i/myWidth));
                x = i - (y * myWidth);

                x *= myTileWidth;
                y *= myTileHeight;

                myTiles[i].Draw(window, x, y);
            }

            myPlayer.Draw(window);
        }

        public void Update()
        {
            myPlayer.Update();
        }

        public void KeyPress(KeyEventArgs e)
        {
            if (e.Code == KeyCode.W && !GetTile(myPlayer.GetX()/myTileWidth, (myPlayer.GetY()/myTileHeight) - 1).Solid)
            {
                myPlayer.Move(0, -myTileHeight);
            }
            else if (e.Code == KeyCode.A && !GetTile((myPlayer.GetX() / myTileWidth) - 1, myPlayer.GetY() / myTileHeight).Solid)
            {
                myPlayer.Move(-myTileWidth, 0);
            }
            else if (e.Code == KeyCode.S && !GetTile(myPlayer.GetX() / myTileWidth, (myPlayer.GetY() / myTileHeight) + 1).Solid)
            {
                myPlayer.Move(0, myTileHeight);
            }
            else if (e.Code == KeyCode.D && !GetTile((myPlayer.GetX() / myTileWidth) + 1, myPlayer.GetY() / myTileHeight).Solid)
            {
                myPlayer.Move(myTileHeight, 0);
            }
        }

        public void KeyUp(KeyEventArgs e)
        {
            //if (e.Code == KeyCode.W || e.Code == KeyCode.A || e.Code == KeyCode.S || e.Code == KeyCode.D)
                //myPlayer.SetPosition(myPlayer.GetRealX() / myTileWidth, myPlayer.GetRealY() / myTileHeight);
        }
    }
}
