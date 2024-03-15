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

            base.Movement(this.AI(player), mapManager);
            if (this.distance < base.range)
            {
                // here we will do the attacks.
            }
        }

        public virtual int[] AI(List<Player> player)
        {
            // here we want to add the most basic ai which we might use multiple times. We want to call this in Update.
            // we just walk straight to the Player.

            int[] nextMove = new int[2] {0, 0};

            if (this.target == null)
            {
                if (player.Count > 1)           // if there are more than 1 player we want the entity to move to the closest.
                {                               // only usefull if multiplayer is implemented.
                    Player p = player[0];

                    int distx = (p.position[0] - base.position[0]);
                    if (distx < 0) distx *= -1;

                    int disty = (p.position[1] - base.position[1]);
                    if (disty < 0) disty *= -1;

                    int distance = distx + disty;

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
                // here we move to the actual target.
                if (this.target.position[0] > base.position[0])
                {
                    nextMove[0] = 1;
                }
                else if (this.target.position[0] < base.position[0])
                {
                    nextMove[0] = -1;
                }
                else 
                {
                    nextMove[0] = 0;
                }

                if (this.target.position[1] > base.position[1])
                {
                    nextMove[1] = 1;
                }
                else if (this.target.position[1] < base.position[1])
                {
                    nextMove[1] = -1;
                }
                else
                {
                    nextMove[1] = 0;
                }
            }
            return nextMove;
        }
    }
}
