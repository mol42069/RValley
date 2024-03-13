using Microsoft.Xna.Framework.Graphics;
using RValley.Entities;
using RValley.Maps;
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
        private MapManager mapManager;
        private MobManager mobManager;
        private Player player;

        public Client(MobManager mobManager, MapManager mapManager, Player player) {
            this.mapManager = mapManager;
            this.mobManager = mobManager;
            this.player = player;


        }

        public void Update() {

            // HERE WE RUN THINGS LIKE ANIMATION AS WELL AS THE PLAYER.

            return;

        }

        public void Draw(SpriteBatch spriteBatch) {

            // HERE WE DRAW EVERITHING:

            return;
        }

        private void SendToServer() {


            return;
        }
    }
}
