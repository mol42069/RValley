using RValley.Maps;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RValley.Server
{
    internal class Server
    {
        public bool running, initialized;
        private Thread networkingThread;
        private MobManager mobManager;
        private MapManager mapManager;

        Server() {
            this.running = true;


        }

        public void Update() {
            
            
            while (this.running)
            {
                //  HERE WE UPDATE EVERYTHING THE SERVER HANDLES.


            }
            return;

        }

        // we want to put networking in a seperate Thread.
        private void Networking() {
        


        }

    }
}
