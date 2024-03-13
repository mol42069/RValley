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
            RUN,
            IDLE,
            DEATH,
            MAX
        }
    }
}
