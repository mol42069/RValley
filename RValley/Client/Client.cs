using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RValley.Entities;
using RValley.Maps;
using RValley.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;




namespace RValley.Client
{
    internal class Client
    {
        // WE MAINLY USE VARIABLES FROM THE SERVER CLASS 
        private Server.Server server;
        private Thread keyHandler;
        private float[] move;
        private long stillAliveTimerMax_ms;
        private bool stillAliveSignal, running;
        private Stopwatch stopwatch;

        public Client(Server.Server server) {
            this.running = true;
            this.stillAliveSignal = true;
            this.stillAliveTimerMax_ms = 1000;
            this.stopwatch = new Stopwatch();
            this.server = server;
            this.keyHandler = new Thread(KeyHandler);
            this.keyHandler.Start();

        }

        public void Update() {
            // here we send the server a still-alive msg.
            this.server.stillAliveSignal = true;
            this.stillAliveSignal = true;
            // HERE WE RUN THINGS LIKE ANIMATION AS WELL AS THE PLAYER.
            this.server.player[0].Update();
            this.server.player[0].Movement(this.move, this.server.mapManager);

            // we do the animations.
            this.server.player[0].Animation();
            this.server.mobManager.Animation();

            return;
        }

        public void KeyHandler() {
            // here we send the KeyStrokes and we 
            // we poll the keyboard.
            this.move = new float[2] {0, 0};
            this.stopwatch.Start();
            while (this.running)
            {
                if (this.stopwatch.ElapsedMilliseconds >= this.stillAliveTimerMax_ms)
                {
                    // here we check the still alive signal
                    this.stopwatch.Stop();

                    if (this.stillAliveSignal)
                    {
                        this.stillAliveSignal = false;
                        this.stopwatch.Reset();
                        this.stopwatch.Start();
                    }
                    else
                    {
                        this.running = false;
                        return;
                    }
                }


                try
                {
                    KeyboardState state = Keyboard.GetState();

                    /*if (state.IsKeyDown(Keys.Escape))
                    {

                    }*/
                    if (state.IsKeyDown(Keys.A) && !(state.IsKeyDown(Keys.D)))
                    {
                        this.move[0] = -1;
                    }
                    else if (state.IsKeyDown(Keys.D) && !(state.IsKeyDown(Keys.A)))
                    {
                        this.move[0] = 1;
                    }
                    else
                    {
                        this.move[0] = 0;
                    }
                    if (state.IsKeyDown(Keys.W) && !(state.IsKeyDown(Keys.S)))
                    {
                        this.move[1] = -1;

                    }
                    else if (state.IsKeyDown(Keys.S) && !(state.IsKeyDown(Keys.W)))
                    {
                        this.move[1] = 1;
                    }
                    else
                    {
                        this.move[1] = 0;
                    }
                } catch {
                    
                }
            }
            /*var mouseState = Mouse.GetState();
            if (this.clicked)
                this.player.sAttackTrigger = true;
            */
        }
    

        public SpriteBatch Draw(SpriteBatch spriteBatch) {

            // HERE WE DRAW EVERITHING:
            spriteBatch = this.server.mapManager.Draw(spriteBatch);
            spriteBatch = this.server.mobManager.Draw(spriteBatch);
            for (int i = 0; i < this.server.player.Count; i++)
            {
                spriteBatch = this.server.player[i].Draw(spriteBatch);

            }

            return spriteBatch;
        }
    }
}
