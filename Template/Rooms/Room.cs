using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Sprites.WorldGen;

namespace Template.Rooms
{
    class Room
    {
        public List<Tree> Trees { get; set; }
        public List<Wall> Walls { get; set; }
        public Telepad_Base Telepad_Base { get; set; }
        public Telepad_Crystal Telepad_Crystal { get; set; }
        public int[,] Map { get; set; }

        Texture2D treeTex;

        public virtual void PlaceWalls()
        {
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map.GetValue(i, j) == 1)
                    {
                        walls.Add(new Wall(wallTex, new Vector2(i * 60, j * 60), new Point(60, 60)));
                    }
                }
            }
        }
        public virtual void PlaceObjects()
        {
            Random random = new Random();
            int entity;

            for (int i = 0; i < Map.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < Map.GetLength(1) - 1; j++)
                {
                    if (Map.GetValue(i, j) == 1)
                    {
                        walls.Add(new Wall(wallTex, new Vector2(i * 60, j * 60), new Point(60, 60)));
                    }
                    else
                    {
                        entity = random.Next(20);
                        switch (entity)
                        {
                            case 0:
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                            case 10:
                                break;

                            case 11:
                                trees.Add(new Tree(treeTex, new Vector2(i * 60, j * 60), new Point(60, 60)));
                                break;

                            case 12:
                                break;
                            case 13:
                                break;
                            case 15:
                                break;
                            case 16:
                                break;
                            case 17:
                                break;
                            case 18:
                                break;
                            case 19:
                                break;
                            case 20:
                                break;
                        }
                    }
                }
            }
        }
    }
}
