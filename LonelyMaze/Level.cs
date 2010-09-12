using System;
using System.Windows.Forms;
using System.Collections.Generic;
using SFML.Graphics;

namespace LonelyMaze
{
    class Level
    {
        private Image wallimage, floorimage, spawnimage, playerimage;
        private Sprite wallsprite, floorsprite, spawnsprite;
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
            for (int i = 0; i < xTiles * yTiles; i++ )
            {
                myTiles.Add(new Tile());
            }
            
            wallimage = Util.LoadImage(@"resources\solid.png");
            wallsprite = new Sprite(wallimage);
            floorimage = Util.LoadImage(@"resources\floor.png");
            floorsprite = new Sprite(floorimage);
            spawnimage = Util.LoadImage(@"resources\spawn.png");
            spawnsprite = new Sprite(spawnimage);
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
                        myTiles[x + myWidth*y].Solid = true;
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
                    else
                    {
                        myTiles[x + myWidth*y].Solid = false;
                        //myTiles[((int)x + myWidth) * (int)y].SetSprite(errorSprite);
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

        public void HandleInput(KeyEventArgs e)
        {
               
        }
    }
}
