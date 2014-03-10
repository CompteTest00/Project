using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GTE
{
   public class Map 
    {

        private List<CollisionTiles> collisionTiles = new List<CollisionTiles>();

        public List<CollisionTiles> CollisionTiles
        {
            get { return collisionTiles; }
        }
        int[,] new_map;
        FileStream fs;
        StreamReader sr;
        string result_array;
        private int width, height;
        public int Width
        {
            get { return width; }
        }
        public int Height
        {
            get { return height;}
        }

        public Map()
        {
            string path = Environment.CurrentDirectory + "\\Content\\map1.txt";
            new_map = new int[10,20] ;
            fs = new FileStream(path, FileMode.Open,FileAccess.Read);
            sr = new StreamReader(fs);
            result_array = sr.ReadToEnd();
        }
        public int[,] Create_Map(int index)
        {
            int x = 0;
            int y = 0;
            for (int i = index; i < result_array.Length; i++)
            {
                if (x == new_map.GetLength(0) )
                {
                    break;
                }
                if (result_array[i] == ' ' || result_array[i] == Convert.ToChar(13) || result_array[i] == Convert.ToChar(10))
                {
                    continue;
                }
                if (result_array[i] != Convert.ToChar(92) )
                {
                    new_map[x, y] = int.Parse(result_array[i].ToString());
                }
                if (y == new_map.GetLength(1) - 1)
                {
                    y = 0;
                    x++;
                }
                else
                    y++;
            }
            return new_map;
        }
        public void Generate(int[,] map, int size)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];
                        collisionTiles.Add(new CollisionTiles(number,new Rectangle((x*size),(y*size),size,size)));
                    width = (x+1) * size;
                    height = (y+1) * size;
                }
            }
		}

        public void Draw(SpriteBatch sb)
        {
            foreach (CollisionTiles tile in collisionTiles)
                tile.Draw(sb);
        }
    }

}
