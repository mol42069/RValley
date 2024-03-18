﻿using RValley.Entities.Enemies;
using RValley.Maps;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RValley.Items
{
    internal class Item
    {
        protected int damage, attackSpeed, targetAmount, reach, weaponRange;

        public Item() {
        
            this.damage = 50;
            this.targetAmount = 2;
            this.reach = 800;
            this.weaponRange = 50;

        }

        public virtual void primaryAttack(List<Enemies> enemies) {
            // Auto attacks.
            

        }

        public virtual void primaryAttack(List<Enemies> enemies, int[] targetPosition, MapManager mapManager) {
            // manual Attacks.
            List<Enemies> targets = this.findTargetsManual(enemies, targetPosition, mapManager);

            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].TakeDamage(this.damage);
            }


        }

        protected List<Enemies> findTargetsManual(List<Enemies> enemies, int[] targetPosition, MapManager mapManager) { 
            List<Enemies> targets = new List<Enemies>();

            for (int i = 0; i < enemies.Count; i++) 
            {
                if (enemies[i].distance <= this.reach) 
                {
                    int[] drawHitBox = new int[2] { enemies[i].hitBox.X, enemies[i].hitBox.Y };
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
