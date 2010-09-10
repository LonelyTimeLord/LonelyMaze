using System;
using System.Collections.Generic;
using SFML.Graphics;

namespace LonelyMaze
{
    class Level
    {
        private Image wallimage, floorimage, spawnimage;
        private List<Tile> myTiles;
        private int myHeight, myWidth, myTileWidth, myTileHeight;

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

            wallimage = new Image(@"resources\solid.png");
            floorimage = new Image(@"resources\floor.png");
            spawnimage = new Image(@"resources\spawn.png");
        }

        public Tile GetTile(int x, int y)
        {
            return myTiles[x + myWidth * y];
        }

        public void LoadFromFile(string filepath)
        {
            Image LevelImage = new Image(filepath);

            for (int x = 0; x < LevelImage.Width; x++)
            {
                for (int y = 0; y < LevelImage.Height; y++)
                {
                    Color colour = LevelImage.GetPixel((uint)x, (uint)y);

                    if (colour.Equals(Color.Black))
                    {
                        myTiles[x + myWidth*y].Solid = true;
                        myTiles[x + myWidth * y].SetSprite(new Sprite(wallimage));
                    }
                    else if (colour.Equals(Color.White))
                    {
                        myTiles[x + myWidth * y].Solid = false;
                        myTiles[x + myWidth * y].SetSprite(new Sprite(floorimage));
                    }
                    else if (colour.Equals(Color.Green))
                    {
                        myTiles[x + myWidth * y].Solid = false;
                        myTiles[x + myWidth * y].SetSprite(new Sprite(spawnimage));
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
        }
    }
}
