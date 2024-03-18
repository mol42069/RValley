using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RValley.Items;
using RValley.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RValley.Entities
{
    internal class Player : Entities
    {
        public bool mouseReleased, mousePress;
        public List<Item> item;

        public Player() 
        {
            this.mouseReleased = false;
            this.mousePress = false;

            base.primaryAttackActive = false;
            base.primaryAttackAnimationCount = 0;

            base.speed = 20;
            base.hpMax = 100;
            base.hp = base.hpMax;
            base.position = new int[2] {200, 200};base.drawPosition = base.position;
            base.hitBox = new Rectangle(base.position[0] + 10, base.position[1] + 10, base.spriteSize - 20, base.spriteSize - 20);

            this.hitBox.Width = this.spriteSize - 100;
            this.hitBox.Height = this.spriteSize - 100;

            base.spriteScale = 1;
            base.aniCount = 0;
            base.entityState = enums.EntityState.IDLE_L;

            // HERE WE SET ALL THE ANIMATION VARIABLES.
            base.animationTimer = new System.Diagnostics.Stopwatch();
            base.animationTimer.Start();
            base.aniCount = 0;
            base.aniCountMax = 0;
            base.aniTimerMax = new long[(int)enums.EntityState.MAX] {50, 50, 100}; // 0 = RUN | 1 = IDLE | 2 = DEATH | (see enums.EntityState)
            base.lastMovement = new float[2] {0, 0};
            this.direction = false;

            this.item = new List<Item>();    // we want this to be an List in case we want the player to have multiple weapons.
            this.item.Add( new Item());

        }

        public override void Update(MapManager mapManager)
        {
            base.hitBox.X = base.position[0] + 75;
            base.hitBox.Y = base.position[1] + 150;

            base.hitBox.Width = base.spriteSize - 142;
            base.hitBox.Height = base.spriteSize - 142;
            
            base.Update(mapManager);

            base.drawBox.X = base.drawPosition[0];
            base.drawBox.Y = base.drawPosition[1];

            base.position[0] = base.hitBox.X - 75;
            base.position[1] = base.hitBox.Y - 150;

            base.hitBox.Width = base.spriteSize - 142;
            base.hitBox.Height = base.spriteSize - 142;



        }

        public void PrimaryAttack(List<Entities> entities)
        {
            // this for auto-attacks

        }

        public void PrimaryAttack(List<Enemies.Enemies> enemies, int[] targetPos, MapManager mapManager)
        {
            // this for manual attacks.
            this.item[0].PrimaryAttack(enemies, targetPos, mapManager);

        }


    }
}
