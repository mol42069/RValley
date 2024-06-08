using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RValley
{
    public class enums
    {
        public enum EnemyType 
        {
            GOBLIN,
            MAX
        }
        public enum GoblinClass 
        {
            TORCH,
            TNT,
            MAX
        }
        public enum SkeletonClass 
        {
            BASE,
            MAGE,
            ROGUE,
            WARRIOR,
            MAX
        }
        public enum PlayerClass 
        {
            KNIGHT,
            MAX
        }
        public enum EntityState 
        { 
            RUN_R,
            RUN_L,
            IDLE_R,
            IDLE_L,
            PATTACK_R,
            PATTACK_L,
            MAX = 3,
            MAXA = 6
        }
        public enum GameState
        {
            MENU,
            INGAME,
            
            MAX
        }
        public enum FireBall {
            FIRE,
            FIREBALL,

            MAX
        
        }
    }
}
