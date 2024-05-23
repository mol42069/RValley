using RValley.Maps;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RValley.Entities.Enemies
{
    public class Enemies : Entities
    {
        protected Player target;
        public int distance, damage;
        public int[] targetOffset;
        private bool alreadyAttacked;
        public Enemies() {

        }

        public virtual void Update(List<Player> player, MapManager mapManager) 
        {
            if (mapManager.backgroundSprite != null)
            {
                base.hitBox.X = base.position[0] + base.hitBoxOffset[0];
                base.hitBox.Y = base.position[1] + base.hitBoxOffset[1];

                if (this.target != null)
                {
                    int distx = ((this.target.hitBox.Center.X + this.targetOffset[0]) - base.hitBox.Center.X);
                    if (distx < 0) distx *= -1;

                    int disty = ((this.target.hitBox.Center.Y + this.targetOffset[1]) - base.hitBox.Center.Y);
                    if (disty < 0) disty *= -1;

                    this.distance = distx + disty;
                }

                if (this.distance < base.reach)
                {
                    // here we will do the attacks.
                    base.lastMovement[0] = 0;
                    base.lastMovement[1] = 0;
                    base.drawPosition = mapManager.calculateDrawPositionEntity(this.position);
                    base.drawBox.X = base.drawPosition[0];
                    base.drawBox.Y = base.drawPosition[1];
                    this.PrimaryAttack(player[0]);

                }
                else
                {

                    base.Movement(this.AI(player), mapManager);

                    base.drawPosition = mapManager.calculateDrawPositionEntity(base.position);

                    base.drawBox.X = base.drawPosition[0];
                    base.drawBox.Y = base.drawPosition[1];
                }
                base.Update(mapManager);

               
            }
        }

        public virtual float[] AI(List<Player> player)
        {
            // here we want to add the most basic ai which we might use multiple times. We want to call this in Update.
            // we just walk straight to the Player.

            if (this.target == null)
            {
                this.target = player[0];

                int distx = ((this.target.hitBox.Center.X + this.targetOffset[0])- base.hitBox.Center.X);
                if (distx < 0) distx *= -1;

                int disty = ((this.target.hitBox.Center.Y + this.targetOffset[1])- base.hitBox.Center.Y);
                if (disty < 0) disty *= -1;

                int distance = distx + disty;

                if (player.Count > 1)           // if there are more than 1 player we want the entity to move to the closest.
                {                               // only usefull if multiplayer is implemented.
                    Player p = player[0];

                    distx = (p.hitBox.Center.X - base.hitBox.Center.X);
                    if (distx < 0) distx *= -1;

                    disty = (p.hitBox.Center.Y - base.hitBox.Center.Y);
                    if (disty < 0) disty *= -1;

                    distance = distx + disty;

                    for (int i = 0; i < player.Count; i++)
                    {
                        distx = (player[i].hitBox.Center.X - base.hitBox.Center.X);
                        if (distx < 0) distx *= -1;
                        disty = (player[i].hitBox.Center.Y - base.hitBox.Center.Y);
                        if (disty < 0) disty *= -1;

                        if (distance > disty + distx) {
                            distance = disty + distx;
                            p = player[i];
                        }
                    }
                    this.target = p;
                }
            }
            // here we move to the actual target.
            int distxs = ((this.target.hitBox.Center.X + this.targetOffset[0]) - base.hitBox.Center.X);
            int distxt = ((this.target.hitBox.Center.X + this.targetOffset[0]) - base.hitBox.Center.X);
            if (distxs < 0) distxs *= -1;

            int distys = ((this.target.hitBox.Center.Y + this.targetOffset[1]) - base.hitBox.Center.Y);
            int distyt = ((this.target.hitBox.Center.Y + this.targetOffset[1]) - base.hitBox.Center.Y);

            if (distys < 0) distys *= -1;

            int distances = distxs + distys;
            this.distance = distances;
            float[] nextMove = new float[2] {0, 0};

            if (distxt != 0 && distyt != 0)
            {
                nextMove[0] = (float)distxt / (float)distances;
                nextMove[1] = (float)distyt / (float)distances;
            }
            else if (distxt == 0)
            {
                if (distyt > 0)
                {
                    nextMove[0] = 0;
                    nextMove[1] = 1;
                }
                else
                {
                    nextMove[0] = 0;
                    nextMove[1] = -1;
                }
            }
            else if (distyt == 0)
            {
                if (distxt > 0)
                {
                    nextMove[0] = 1;
                    nextMove[1] = 0;
                }
                else
                {
                    nextMove[0] = -1;
                    nextMove[1] = 0;
                }
            }
            return nextMove;
        }
        public void PrimaryAttack(List<Player> player) 
        {        
            // we use this of AOE attacks
            
        }
        public void PrimaryAttack(Player player)
        {
            // we use this for single target attacks.

            // first we animate the attack windup and when we are far enough we deal damage.
            base.primaryAttackActive = true;
            if (player.position[0] <= base.position[0])
            {
                base.direction = true;
            }
            else 
            {
                base.direction = false;
            }

            if ((base.aniCount == base.aniCountMax - 1 && base.entityState == enums.EntityState.PATTACK_R) || (base.aniCount == 0 && base.entityState == enums.EntityState.PATTACK_L))      // - 1 can be changed to whatever we just want the damage to be delt before the animation is finished. 
            {
                if (!this.alreadyAttacked)
                {
                    this.alreadyAttacked = true;
                    player.TakeDamage(this.damage);
                }
            }
            else {
                this.alreadyAttacked = false;
            }
        }

        protected bool PrimaryAttackAnimation() {

            return false;
        }
    }
}
