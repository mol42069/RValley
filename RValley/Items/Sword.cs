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
    public class Sword : Item
    {
        public Sword()
        {
            this.projectiles = new List<FireBall>();
        }

        public override void PrimaryAttack(List<Enemies> enemies)
        {
            base.PrimaryAttack(enemies);
        }

        public override void Update(List<Enemies> enemies, List<Player> players)
        {

        }

        public override void PrimaryAttack(List<Enemies> enemies, int[] targetPosition, MapManager mapManager, Texture2D[] sprite, int[] playerPos)
        {
                
        }

        public override SpriteBatch Draw(SpriteBatch spriteBatch, MapManager mapManager)
        {
            return spriteBatch;
        }
    }
}
