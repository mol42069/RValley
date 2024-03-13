using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RValley.Entities.Enemies;

namespace RValley
{
    internal class MobManager
    {
        private List<Entities.Enemies.Enemies> enemies;
        private Texture2D[][][] sprites;
        private Rectangle[][][][] sourceRectangle;
        
        public MobManager() {
            enemies = new List<Enemies> { };
        }

        private void CreateSourceRectangles() {
            // Here we create our sourceRectangles so we can switch between sprites.

            // we create an array for all the different EnemyClasses:
            this.sourceRectangle = new Rectangle[this.sprites.Length][][][];

            // !!! SPRITES FOR ENEMIES MUST BE THE SAME HEIGHT AS WIDTH !!!
            int spriteSize;

            for (int i = 0; i < this.sprites.Length; i++)
            {   
                // we create an array for all the different EnemyClasses
                this.sourceRectangle[i] = new Rectangle[this.sprites[i].Length][][];

                for (int j = 0; j < this.sprites[i].Length; j++)
                {   
                    // we create an array for all the different EntityStates
                    this.sourceRectangle[i][j] = new Rectangle[this.sprites[i][j].Length][];

                    for (int k = 0; k < this.sprites[i][j].Length; k++)
                    {   
                        spriteSize = this.sprites[i][j][k].Height;
                        // we create an array for all the actual Rectangles.                     
                        this.sourceRectangle[i][j][k] = new Rectangle[(int)(this.sprites[i][j][k].Width / spriteSize) + 1];

                        for (int n = 0; n < (this.sprites[i][j][k].Width / spriteSize);  n++) 
                        {
                            // we create the sourcerectangles and add them to our array in the same configuration as the spriteSheets are.                            
                            this.sourceRectangle[i][j][k][n] = new Rectangle(n * spriteSize, 0, spriteSize, spriteSize);                        
                        }
                    }                
                }            
            }
            return;
        }

        public void LoadContent(Texture2D[][][] sprites) {
            this.sprites = sprites;
            this.CreateSourceRectangles();
            return;
        }

        public void ServerSideUpdate()
        {


        }
        public void ClientSideUpdate()
        {


        }

        public SpriteBatch Draw(SpriteBatch spriteBatch) {
        
            
            return spriteBatch; 
        }
        public void Animation() {

            return;
        }
    }
}
