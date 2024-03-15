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
            ORC,
            SKELETON,
            MAX
        }
        public enum OrcClass 
        {
            BASE,
            ROGUE,
            SHAMAN,
            WARRIOR,
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
            DEATH_R,
            DEATH_L,
            MAX = 3,
            MAXA = 6
        }
        public enum GameState
        {
            MENU,
            INGAME,
            
            MAX
        }
    }
}
