using Microsoft.Xna.Framework.Graphics;
using RValley.Entities.Enemies;
using RValley.Items.Projectiles;
using RValley.Maps;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RValley.Items
{
    public class Item
    {
        protected int damage, attackSpeed, targetAmount, reach, weaponRange;
        protected List<FireBall> projectiles;

        public Item() {
        
            this.damage = 50;
            this.targetAmount = 2000;
            this.reach = 800;
            this.weaponRange = 200;

        }

        public virtual void Update(List<Enemies> enemies)
        {
        
        }


        public virtual void PrimaryAttack(List<Enemies> enemies) {
            // Auto attacks.
            

        }

        public virtual SpriteBatch Draw(SpriteBatch spriteBatch, MapManager mapManager)
        {

            
            return spriteBatch;
        }

        public virtual void PrimaryAttack(List<Enemies> enemies, int[] targetPosition, MapManager mapManager, Texture2D[] projectileSprites, int[] playerPos) {
            // manual Attacks.
            List<Enemies> targets = this.findTargetsManual(enemies, targetPosition, mapManager);

            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].TakeDamage(this.damage);
            }
            targets.Clear();

        }

        protected List<Enemies> findTargetsManual(List<Enemies> enemies, int[] targetPosition, MapManager mapManager) { 
            List<Enemies> targets = new List<Enemies>();

            for (int i = 0; i < enemies.Count; i++) 
            {
                if (enemies[i].distance <= this.reach) 
                {
                    int[] drawHitBox = new int[2] { enemies[i].hitBox.Center.X, enemies[i].hitBox.Center.Y };
                    drawHitBox = mapManager.calculateDrawPositionEntity(drawHitBox);

                    int mouseDistanceX = targetPosition[0] - drawHitBox[0];
                    if (mouseDistanceX < 0) mouseDistanceX *= -1;

                    int mouseDistanceY = targetPosition[1] - drawHitBox[1];
                    if (mouseDistanceY < 0) mouseDistanceY *= -1;

                    if (mouseDistanceX + mouseDistanceY <= this.weaponRange) 
                    {                    
                        targets.Add(enemies[i]);
                    }                
                }            
            }
            return targets;        
        }
    }
}
