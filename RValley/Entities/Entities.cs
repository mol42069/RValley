using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RValley.Entities
{

// WE WANT TO DO EVERYTHING (WHAT WE DO IN CHILDREN) WE CAN IN THIS CLASS:
    internal class Entities
    {
        protected int[] position, spriteSize;
        protected Rectangle HitBox;   // if we decide to add headshots or other stuff we might want to add this here... or save those somewhere else.
        

        public Entities() {

        }

        public virtual void Update() {

        }
        public virtual SpriteBatch Draw(SpriteBatch spriteBatch){


            return spriteBatch;
        }
        public virtual void Movement(int[] move)
        {

        }
        public void LoadContent() { 
        
        }
        public void Animation() {
        
        
        }
    }
}
