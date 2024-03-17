using RValley.Maps;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RValley.Entities.Enemies
{
    internal class Enemies : Entities
    {
        protected Player target;
        public int distance;
        public Enemies() {

        }
        public virtual void Update(List<Player> player, MapManager mapManager) {
            if (mapManager.backgroundSprite != null)
            {
                if (this.distance < base.reach)
                {
                    // here we will do the attacks.
                    this.AI(player);
                }
                else
                {
                    base.Movement(this.AI(player), mapManager);
                }
                base.drawPosition = mapManager.calculateDrawPositionEntity(base.position);

            }
        }

        public virtual float[] AI(List<Player> player)
        {
            // here we want to add the most basic ai which we might use multiple times. We want to call this in Update.
            // we just walk straight to the Player.

            

            if (this.target == null)
            {
                this.target = player[0];

                int distx = (this.target.position[0] - base.position[0]);
                if (distx < 0) distx *= -1;

                int disty = (this.target.position[1] - base.position[1]);
                if (disty < 0) disty *= -1;

                int distance = distx + disty;

                if (player.Count > 1)           // if there are more than 1 player we want the entity to move to the closest.
                {                               // only usefull if multiplayer is implemented.
                    Player p = player[0];

                    distx = (p.position[0] - base.position[0]);
                    if (distx < 0) distx *= -1;

                    disty = (p.position[1] - base.position[1]);
                    if (disty < 0) disty *= -1;

                    distance = distx + disty;

                    for (int i = 0; i < player.Count; i++)
                    {
                        distx = (player[i].position[0] - base.position[0]);
                        if (distx < 0) distx *= -1;
                        disty = (player[i].position[1] - base.position[1]);
                        if (disty < 0) disty *= -1;

                        if (distance > disty + distx) {
                            distance = disty + distx;
                            p = player[i];
                        }
                    }
                    this.distance = distance;
                    this.target = p;
                }
            }
            // here we move to the actual target.
            int distxs = (this.target.position[0] - base.position[0]);
            int distxt = (this.target.position[0] - base.position[0]);
            if (distxs < 0) distxs *= -1;

            int distys = (this.target.position[1] - base.position[1]);
            int distyt = (this.target.position[1] - base.position[1]);
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
    }
}
