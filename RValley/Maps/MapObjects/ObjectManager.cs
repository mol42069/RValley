using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RValley.Maps.MapObjects
{
    enum ObjectType
    {
        Stone = 0
    }

    public class ObjectManager
    {
        private List<Object> objects;
        private int objectCountMax = 100;
        public int[] screenSize;
        private Texture2D[][] objectSprites;

        public ObjectManager(int[] screenSize)
        {
            this.objects = new List<Object> { };
            this.screenSize = screenSize;
        }

        public void Update() 
        { 
            for (int i = 0; i < this.objects.Count; i++)
            {
                if (this.objects[i].Update())
                {
                    this.objects.RemoveAt(i);
                    i--;
                }
            }
        }

        public void LoadContent(Texture2D[][] objectSprites)
        {
            this.objectSprites = objectSprites;
            this.SpawnObjects();
        }

        public SpriteBatch Draw(SpriteBatch spriteBatch, MapManager mapManager) 
        { 
            for (int i = 0; i < this.objects.Count; i++)
            {
                spriteBatch = this.objects[i].Draw(spriteBatch, mapManager);
            }

            return spriteBatch;
        }

        public void BreakObj(int[] breakPos) {
            for (int i = 0; i < this.objects.Count; i++) 
            {
                this.objects[i].BreakObj(1, breakPos);
            }
        }

        public void SpawnObjects()
        { 
            while(this.objects.Count < this.objectCountMax) this.Spawn();
        }

        private void Spawn()
        {
            Random rnd = new Random();

            int rng = rnd.Next(0, 0);

            switch (rng)
            {
                case 0:
                    // spawns Stone
                    this.objects.Add(new Object(new int[2] {-10000, 0}, this.objectSprites[(int)ObjectType.Stone]));
                    break;

                default:
                    // default -> spawns Stone so that we dont move random objects around.
                    this.objects.Add(new Object(new int[2] {-10000, 0}, this.objectSprites[(int)ObjectType.Stone]));
                    break;
            }

            // Change the position of the object so that it is not colliding with other objects
            this.objects[this.objects.Count - 1].pos = this.FindPos(new int[2] { this.objects[this.objects.Count - 1].rect.Width, this.objects[this.objects.Count - 1].rect.Height});
        }

        private int[] FindPos(int[] size) {
            // find a position for the object that is not colliding with other objects
            Random rnd = new Random();
            int[] pos = new int[2] { rnd.Next(0, this.screenSize[0]), rnd.Next(0, this.screenSize[1]) };
            pos = new int[2] { 200, 200 };
            Rectangle rect = new Rectangle(pos[0], pos[1], size[0], size[1]);

            // find a position for the object that is not colliding with other objects
            while (this.CollisionCheck(rect)) {
                // pos = new int[2] { rnd.Next(0, this.screenSize[0]), rnd.Next(0, this.screenSize[1]) };
                pos = new int[2] { 2000, 2000 };
                rect.X = pos[0];
                rect.Y = pos[1];
            }
            return pos;            
        }

        public bool CollisionCheck(Rectangle rect)
        {
            for (int i = 0; i < this.objects.Count; i++)
            {
                if (this.objects[i].CollisionCheck(rect)) return true;
            }
            return false;
        }
    }
}
