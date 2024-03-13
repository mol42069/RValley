using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RValley.Entities.Enemies;

namespace RValley
{
    internal class MobManager
    {
        private List<Entities.Enemies.Enemies> enemies;
        private Texture2D[][][] sprites;
        
        public MobManager() {
        
        
        }

        public void ServerSideUpdate() {
        
        
        }
        public void ClientSideUpdate() {
        
        
        }
        public void LoadContent(Texture2D[][][] sprites) {
            this.sprites = sprites;

        }

        public SpriteBatch Draw(SpriteBatch spriteBatch) {
        
            
            return spriteBatch; 
        }
        public void Animation() {

            return;
        }
    }
}
