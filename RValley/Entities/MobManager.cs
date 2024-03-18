using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RValley.Entities.Enemies;
using RValley.Entities;
using RValley.Maps;
using System.Numerics;
using System.Threading;

namespace RValley
{
    internal class MobManager
    {
        public List<Entities.Enemies.Enemies> enemies;
        private Texture2D[][][] sprites;
        private Rectangle[][][][] sourceRectangle;
        private Random rand;
        
        public MobManager() {
            this.enemies = new List<Enemies> { };
            this.rand = new Random();
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
                        this.sourceRectangle[i][j][k] = new Rectangle[(int)(this.sprites[i][j][k].Width / spriteSize)];

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

        public void ServerSideUpdate(List<Player> player, MapManager mapManager)
        {
            if (this.sprites == null) return;
            // here we Update all the mobs and let their AI move them.
            for (int i = 0; i < this.enemies.Count; i++)
            {
                this.enemies[i].Update(player, mapManager);

                if (this.enemies[i].hp <= 0) this.enemies.RemoveAt(i);

            }
            this.Spawn(player, mapManager);

        }
        public void ClientSideUpdate()
        {
            // here we basicly only do damage to the enemies using the player.

        }

        private void Spawn(List<Player> player, MapManager mapManager) {
            // here we spawn the enemies for now we do this manualy so we need to change this when we have rooms.
            if (mapManager.backgroundSprite == null) return;

            if (this.enemies.Count < 8) {
                int x = this.rand.Next(0, 10);
                // int[] newPos = new int[2] {this.rand.Next(0, 1000), this.rand.Next(0, 800) };
                int[] newPos = new int[2] {this.rand.Next(0, mapManager.backgroundSprite.Width), this.rand.Next(0, mapManager.backgroundSprite.Height) };
                switch (x) {
                    
                    case 0:
                        this.enemies.Add(new Zombie(newPos, new int[2] { this.rand.Next(-50, 50), this.rand.Next(-50, 50) }, this.rand.Next(0, 4)));
                        this.enemies[this.enemies.Count - 1].LoadContent(this.sprites[(int)enums.EnemyType.GOBLIN][(int)enums.GoblinClass.TORCH], this.sourceRectangle[(int)enums.EnemyType.GOBLIN][(int)enums.GoblinClass.TORCH]);
                        break;

                     default:
                        this.enemies.Add(new Zombie(newPos, new int[2] { this.rand.Next(-50, 50), this.rand.Next(-50, 50)}, this.rand.Next(0, 4)));
                        this.enemies[this.enemies.Count - 1].LoadContent(this.sprites[(int)enums.EnemyType.GOBLIN][(int)enums.GoblinClass.TORCH], this.sourceRectangle[(int)enums.EnemyType.GOBLIN][(int)enums.GoblinClass.TORCH]);
                        break;
                }
            }
        }

        public SpriteBatch Draw(SpriteBatch spriteBatch) {

            // TODO: maybe add damage / hp / particles for the enemies.

            for (int i = 0; i < this.enemies.Count; i++)
            {
                spriteBatch = this.enemies[i].Draw(spriteBatch);
            }
            
            return spriteBatch; 
        }
        public void Animation() {
            for (int i = 0; i < this.enemies.Count; i++) {
                this.enemies[i].Animation();
            }
            return;
        }
    }
}
