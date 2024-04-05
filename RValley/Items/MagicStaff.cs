using Microsoft.Xna.Framework.Graphics;
using RValley.Entities;
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

        public override void Update(List<Enemies> enemies, List<Player> players) {
            int distx = 0;
            int disty = 0;
            int distance = 0;

            for (int i = 0; i < projectiles.Count; i++)
            {
                if (this.projectiles[i].Update(enemies)) // projectiles. update returns true if the fireball explodes. so we then have to calculate who gets damaged thats what we do here.
                {
                    for (int j = 0; j < enemies.Count; j++) {

                        distx = enemies[j].hitBox.Center.X - this.projectiles[i].rectangle.Center.X;
                        if (distx < 0) {
                            distx *= -1;
                        }

                        disty = enemies[j].hitBox.Center.Y - this.projectiles[i].rectangle.Center.Y;
                        if (disty < 0)
                        {
                            disty *= -1;
                        }
                        distance = distx + disty;

                        if (distance <= this.projectiles[i].range)
                        {
                            enemies[j].TakeDamage(this.projectiles[i].damage);
                        }
                    }
                    
                    // this is for teamdamage as well as to damage yourself.

                    for (int z = 0; z < players.Count; z++)
                    {

                        distx = players[z].hitBox.Center.X - this.projectiles[i].rectangle.Center.X;
                        if (distx < 0)
                        {
                            distx *= -1;
                        }

                        disty = players[z].hitBox.Center.Y - this.projectiles[i].rectangle.Center.Y;

                        if (disty < 0)
                        {
                            disty *= -1;
                        }
                        distance = distx + disty;

                        if (distance <= this.projectiles[i].range)
                        {
                            players[z].TakeDamage(this.projectiles[i].damage);
                        }
                    }

                    if (this.projectiles[i].aniCount >= this.projectiles[i].aniCountMax)
                    {
                        this.projectiles.RemoveAt(i);
                    }
                }
            }
        }

        public override void PrimaryAttack(List<Enemies> enemies, int[] targetPosition, MapManager mapManager, Texture2D[] sprite, int[] playerPos)
        {
            this.projectiles.Add(new FireBall(10, targetPosition, sprite, playerPos));
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
