using Microsoft.Xna.Framework.Graphics;
using RValley.Entities.Enemies;
using RValley.Items.Projectiles;
using RValley.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RValley.Items
{
    public class MagicStaffFire : Item
    {
        protected List<FireBall> projectiles;
        public MagicStaffFire() {
            
            this.projectiles = new List<FireBall>();   
            
        }

        public override void PrimaryAttack(List<Enemies> enemies)
        {
            base.PrimaryAttack(enemies);
        }

        public override void Update(List<Enemies> enemies) {
            for (int i = 0; i < projectiles.Count; i++)
            {
                if (this.projectiles[i].Update(enemies)) 
                {
                    for (int j = 0; j < enemies.Count; j++) {

                        int distx = enemies[j].position[0] - this.projectiles[i].position[0];
                        if (distx < 0) {
                            distx *= -1;
                        }

                        int disty = enemies[j].position[1] - this.projectiles[i].position[1];
                        if (disty < 0)
                        {
                            disty *= -1;
                        }
                        int distance = distx + disty;

                        if (distance <= this.projectiles[i].range)
                        {
                            enemies[j].TakeDamage(this.projectiles[i].damage);
                        }
                    }
                    this.projectiles.RemoveAt(i);
                }
            }
        }

        public override void PrimaryAttack(List<Enemies> enemies, int[] targetPosition, MapManager mapManager, Texture2D[] sprite, int[] playerPos)
        {

            this.projectiles.Add(new FireBall(100, targetPosition, sprite, playerPos));
        
        }

        public override SpriteBatch Draw(SpriteBatch spriteBatch, MapManager mapManager) {
            if (this.projectiles == null) return spriteBatch;
            for (int i = 0; i < this.projectiles.Count; i++)    
            {
                spriteBatch = this.projectiles[i].Draw(spriteBatch, mapManager);
            }

            return spriteBatch;
        }



    }
}
