using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RValley.Client.UI;
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
        private bool stillAliveSignal, running, mouseClicked, pastMouseClicked;
        private Stopwatch stopwatch;
        private int[] mousePosition;

        public Client(Server.Server server)
        {
            this.mouseClicked = false;
            this.pastMouseClicked = false;

            this.running = true;
            this.stillAliveSignal = true;
            this.stillAliveTimerMax_ms = 1000;
            this.stopwatch = new Stopwatch();
            this.server = server;
            this.keyHandler = new Thread(KeyHandler);
            this.keyHandler.Start();
            this.mousePosition = new int[2] { 0, 0 };
        }

        public void Update() {
            // here we send the server a still-alive msg.
            this.server.stillAliveSignal = true;
            this.stillAliveSignal = true;
            
            // here we update the mouse position.
            
            // HERE WE RUN THINGS LIKE ANIMATION AS WELL AS THE PLAYER.

            this.server.player[0].Update(this.server.mapManager, this.server.mobManager.enemies);
            this.server.player[0].Movement(this.move, this.server.mapManager);

            // here we do the attacks.


            if (!this.server.player[0].primaryAttackActive && this.server.player[0].primaryAttackFinished)
            {
                this.server.player[0].primaryAttackFinished = false;
                this.server.player[0].PrimaryAttack(this.server.mobManager.enemies, this.mousePosition, this.server.mapManager);

            }

            this.server.player[0].AutoAttack(this.server.mobManager.enemies, this.server.mapManager);

            var mouseState = Mouse.GetState();



            if (mouseState.LeftButton == ButtonState.Released)
            {
                this.mouseClicked = false;
            }
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                this.mouseClicked = true;
            }



            if (this.pastMouseClicked && !this.mouseClicked)
            {
                // here we want to do things which are on mouse-click release.

                this.server.player[0].mouseReleased = true;
                this.server.player[0].mousePress = false;
            }
            else if (this.mouseClicked)
            {
                // here we want to do things which are on mouse-click press

                this.server.player[0].mouseReleased = false;
                this.server.player[0].mousePress = true;
            }
            else
            {
                // we need to set those variables back to false.

                this.server.player[0].mouseReleased = false;
                this.server.player[0].mousePress = false;
            }

            if (this.pastMouseClicked && !this.mouseClicked)
            {
                this.mousePosition = new int[2] { mouseState.X, mouseState.Y };
                if (this.mousePosition[0] < this.server.player[0].drawBox.Center.X)
                {
                    this.server.player[0].direction = true;
                }
                else 
                {
                    this.server.player[0].direction = false;
                }
                this.server.player[0].primaryAttackActive = true;
            }

            this.pastMouseClicked = this.mouseClicked;

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

                   

                } 
                catch 
                {
                    
                }
            }
            /*var mouseState = Mouse.GetState();
            if (this.clicked)
                this.player.sAttackTrigger = true;
            */
        }
    

        public SpriteBatch Draw(SpriteBatch spriteBatch, EnemyHealthBar enemyHealthBar) {

            // HERE WE DRAW EVERITHING:
            spriteBatch = this.server.mapManager.Draw(spriteBatch);
            spriteBatch = this.server.mobManager.Draw(spriteBatch, this.server.mapManager, enemyHealthBar);
            for (int i = 0; i < this.server.player.Count; i++)
            {
                spriteBatch = this.server.player[i].Draw(spriteBatch, this.server.mapManager);

            }

            spriteBatch = this.server.player[0].healthBar.Draw(spriteBatch, this.server.player[0]);    // we have to make sure somewhere that player[0] is the player which is running on this machine

            return spriteBatch;
        }
    }
}
