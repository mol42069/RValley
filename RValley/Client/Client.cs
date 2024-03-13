using Microsoft.Xna.Framework.Graphics;
using RValley.Entities;
using RValley.Maps;
using RValley.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;




namespace RValley.Client
{
    internal class Client
    {
        // WE MAINLY USE VARIABLES FROM THE SERVER CLASS 
        private Server.Server server;   

        public Client(Server.Server server) {
            this.server = server;

        }

        public void Update() {
            // here we send the server a still-alive msg.
            this.server.stillAliveSignal = true;
            // HERE WE RUN THINGS LIKE ANIMATION AS WELL AS THE PLAYER.
            this.server.player.i++;
            
            return;
        }

        public void KeyHandler() {
            // here we send the KeyStrokes and we 
        
        }

        public SpriteBatch Draw(SpriteBatch spriteBatch) {

            // HERE WE DRAW EVERITHING:

            spriteBatch = this.server.mobManager.Draw(spriteBatch);
            spriteBatch = this.server.player.Draw(spriteBatch);

            return spriteBatch;
        }
    }
}
