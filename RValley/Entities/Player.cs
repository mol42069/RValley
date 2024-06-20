using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RValley.Client.UI;
using RValley.Items;
using RValley.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static RValley.Client.UI.UIEnums;
using HealthBar = RValley.Client.UI.HealthBar;

namespace RValley.Entities
{
    public class Player : Entities
    {
        public bool mouseReleased, mousePress;
        public List<Item> item;
        public HealthBar healthBar;
        public Texture2D[] FireBallSprites, explosiveBallSprites;
        public int autoAttackCounter, autoAttackCounterMax;
        public Player() 
        {
            this.healthBar = new HealthBar();

            this.autoAttackCounter = 0;
            this.autoAttackCounterMax = 30;

            this.mouseReleased = false;
            this.mousePress = false;

            base.primaryAttackActive = false;
            base.primaryAttackAnimationCount = 0;

            base.speed = 20;
            base.hpMax = 100;
            base.hp = base.hpMax;
            base.spriteScale = 1;

            base.position = new int[2] {200, 200};base.drawPosition = base.position;
            
            
            base.hitBoxOffset = new int[2] { (int)(56 * base.spriteScale), (int)(67 * base.spriteScale) };

            base.hitBox = new Rectangle(base.position[0] + base.hitBoxOffset[0], base.position[1] + base.hitBoxOffset[1], base.spriteSize - base.hitBoxOffset[0] * 2, base.spriteSize - base.hitBoxOffset[1]);

            base.hitBox.Width = (int)(base.spriteSize * base.spriteScale - base.hitBoxOffset[0]);
            base.hitBox.Height = (int)(base.spriteSize * base.spriteScale - base.hitBoxOffset[1]);

            base.aniCount = 0;
            base.entityState = enums.EntityState.IDLE_L;

            // HERE WE SET ALL THE ANIMATION VARIABLES.
            base.animationTimer = new System.Diagnostics.Stopwatch();
            base.animationTimer.Start();
            base.aniCount = 0;
            base.aniCountMax = 0;
            base.aniTimerMax = new long[(int)enums.EntityState.MAX] {50, 50, 50}; // 0 = RUN | 1 = IDLE | 2 = DEATH | (see enums.EntityState)
            base.lastMovement = new float[2] {0, 0};
            this.direction = false;

            this.item = new List<Item>();    // we want this to be an List in case we want the player to have multiple weapons.
            this.item.Add( new MagicStaffFire());

        }
        public void LoadContent(Texture2D[] spriteSheets, Texture2D[] uiHBElements, Texture2D[] ProjectileSprites, Texture2D[] explosiveBallSprite)
        {
            this.FireBallSprites = ProjectileSprites;
            this.healthBar.LoadContent(uiHBElements);
            this.explosiveBallSprites = explosiveBallSprite;
            base.LoadContent(spriteSheets);
        }

        public void Update(MapManager mapManager, List<Enemies.Enemies> enemies)
        {
            
            base.Update(mapManager);

            base.hitBox.X = base.position[0] + base.hitBoxOffset[0];
            base.hitBox.Y = base.position[1] + base.hitBoxOffset[1];


            base.drawBox.X = base.drawPosition[0];
            base.drawBox.Y = base.drawPosition[1];

            base.hitBox.Width =(int)(base.spriteSize * base.spriteScale - base.hitBoxOffset[0] * 2);
            base.hitBox.Height = (int)(base.spriteSize * base.spriteScale - base.hitBoxOffset[1]);
            List <Player> list = new List<Player>();
            list.Add(this);
            this.item[0].Update(enemies, list);
        }

        public void PrimaryAttack(List<Entities> entities)
        {
            // this for auto-attacks

        }
        public override SpriteBatch Draw(SpriteBatch spriteBatch, MapManager mapManager) { 

            this.item[0].Draw(spriteBatch, mapManager);

            return base.Draw(spriteBatch, mapManager);
        }

        public void PrimaryAttack(List<Enemies.Enemies> enemies, int[] targetPos, MapManager mapManager)
        {
            // this for manual attacks.

            targetPos = mapManager.calculateRealPositionEntity(targetPos);
            int[] temp = { base.hitBox.Center.X, base.hitBox.Center.Y };
            this.item[0].PrimaryAttack(enemies, targetPos, mapManager, this.explosiveBallSprites, base.position);

        }

        public void AutoAttack(List<Enemies.Enemies> enemies, MapManager mapManager) {

            this.autoAttackCounter++;
            if (this.autoAttackCounter > this.autoAttackCounterMax)
            {
                this.item[0].AutoAttack(enemies, mapManager, this.FireBallSprites, base.position);
                this.autoAttackCounter = 0;
            }
        }

    }
}


