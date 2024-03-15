using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RValley.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RValley.Maps
{
    internal class MapManager
    {

        private Texture2D backgroundSprite;
        private Rectangle mapRectangle;
        private int[] position;

        public MapManager(int[] startingPos) {          // startingPos must be negative.
            this.position = startingPos;
            return;
        }

        public void Update(int[] playerPos , int[] screenSize) {
            // we need to update where the rectangle is so the player is always in the middle of the screen.
            this.CalcMapPosition(playerPos, screenSize);

            // we need to update where the sprite(the background) Rectangle is drawn
            this.mapRectangle.X = this.position[0];
            this.mapRectangle.Y = this.position[1];
            return;
        }

        public void CalcMapPosition(int[] playerPos, int[] screenSize) {

            // following Camera X-Axis:
            if (playerPos[0] <= screenSize[0] / 2) 
            {
                this.position[0] = 0;
            }
            else if (playerPos[0] >= (this.backgroundSprite.Width * -1 + (screenSize[0] / 2)) * -1)
            {
                this.position[0] = -(this.backgroundSprite.Width - screenSize[0]);
            }
            else
            {
                this.position[0] = (playerPos[0] - (screenSize[0] / 2)) * -1;
            }

            // following Camera Y-Axis:
            if (playerPos[1] <= screenSize[1] / 2)
            {
                this.position[1] = 0;
            }
            else if (playerPos[1] >= (this.backgroundSprite.Height * -1 + (screenSize[1] / 2)) * -1)
            {
                this.position[1] = -(this.backgroundSprite.Height - screenSize[1]);
            }
            else
            {
                this.position[1] = (playerPos[1] - (screenSize[1] / 2)) * -1;
            }
        }

        public void LoadContent(Texture2D backgroundSprite) { 
            this.backgroundSprite = backgroundSprite;
            this.mapRectangle = new Rectangle(this.position[0], this.position[1], this.backgroundSprite.Width, this.backgroundSprite.Height);
        }

        public int[] calculateDrawPositionEntity(int[] realPosition) {

            int[] drawPosition = new int[2] {realPosition[0] + this.position[0], realPosition[1] + this.position[1] };

            return drawPosition;
        }


        public SpriteBatch Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.backgroundSprite, this.mapRectangle, Color.White);
            return spriteBatch;
        }
    }
}
