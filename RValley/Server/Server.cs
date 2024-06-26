﻿using RValley.Entities;
using RValley.Maps;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RValley.Server
{
    internal class Server
    {
        public bool running, initialized, stillAliveSignal;
        private Stopwatch sAStopwatch, stopwatch;
        private Thread networkingThread;
        public MobManager mobManager;
        public MapManager mapManager;
        public List<Player> player;
        public long stillAliveTimerMax_ms, frameTime_ms;    // we use these to check if the client is still running.
        public int tickrate;
        public int[] screenSize;
        public Server(int[] screenSize) 
        {
            this.screenSize = screenSize;

            this.running = true;
            this.player = new List<Player>();
            this.player.Add(new Player());
            this.mobManager = new MobManager();
            int[] startingmappos = new int[2] {-100, -100};
            this.mapManager = new MapManager(startingmappos);
            this.stopwatch = new Stopwatch();
            this.sAStopwatch = new Stopwatch();
            this.stillAliveTimerMax_ms = 1000;
            this.stillAliveSignal = false;
            this.tickrate = 128;
            this.frameTime_ms = (long)(1000 / this.tickrate);
        }

        public void Update() {

            this.sAStopwatch.Start();
            this.stopwatch.Start();

            while (this.running)
            {
                // here we check the still alive signal
                this.stopwatch.Start();
                if (this.sAStopwatch.ElapsedMilliseconds >= this.stillAliveTimerMax_ms) {

                    this.sAStopwatch.Stop();

                    if (this.stillAliveSignal)
                    {
                        this.stillAliveSignal = false;
                        this.sAStopwatch.Reset();
                        this.sAStopwatch.Start();
                    }
                    else 
                    {
                        this.running = false;
                        return;
                    }
                }

                // BETWEEN THE LINES GOES THE GAME CODE:
                // ----------------------------------------------------------------------------

                this.mapManager.Update(this.player[0].position, this.screenSize); // here we manage the map position.
                for (int i = 0; i < this.player.Count; i++)
                {
                    this.player[i].drawPosition = this.mapManager.calculateDrawPositionEntity(this.player[i].position);
                }
                this.mobManager.ServerSideUpdate(this.player, this.mapManager); // here we update the mobs(basicly everything except Taking Damage)
                
                // ----------------------------------------------------------------------------

                // here we make the server run on the given tickrate.
                this.stopwatch.Stop();
                if (this.stopwatch.ElapsedMilliseconds < this.frameTime_ms)
                {
                    Thread.Sleep((int)(this.frameTime_ms - this.stopwatch.ElapsedMilliseconds));
                    this.stopwatch.Reset();
                    this.stopwatch.Start();
                }
            }
            return;

        }

        // we want to put networking in a seperate Thread.
        private void Networking() {
        


        }

    }
}
