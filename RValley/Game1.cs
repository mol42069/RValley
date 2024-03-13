using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace RValley
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Thread serverThread;
        private Server.Server server;
        private MobManager mobManager;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.mobManager = new MobManager();
            // here we start the server thread:
            // this.serverThread = new Thread(new ThreadStart(this.server.Update));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            

            // here we create an sprite-sheet array for all mobs.
            {
                Texture2D[][][] mobSprites = new Texture2D[(int)enums.EnemyType.MAX][][];
                string contentTypePath = "";

                for (int i = 0; i < (int)enums.EnemyType.MAX; i++)
                {
                    string contentPath = "Enemy/";

                    // we change the path so we get every EnemyType
                    switch (i)
                    {
                        case (int)enums.EnemyType.ORC:
                            contentPath += "Orc Crew/";
                            break;

                        case (int)enums.EnemyType.SKELETON:
                            contentPath += "Skeleton Crew/";
                            break;

                        default:
                            break;
                    }

                    // now we append the enemyType to the path of the spriteSheet
                    switch (i){
                        case (int)enums.EnemyType.ORC:

                            mobSprites[i] = new Texture2D[(int)enums.OrcClass.MAX][];
                            contentPath += "Orc";

                            // now we append the enemyClass to the path of the spriteSheet
                            for (int n = 0; n < (int)enums.OrcClass.MAX; n++) {
                                contentTypePath = contentPath;

                                switch (n)
                                {
                                    case (int)enums.OrcClass.BASE:
                                        contentTypePath += "/";
                                        break;

                                    case (int)enums.OrcClass.ROGUE:
                                        contentTypePath += " - Rogue/";
                                        break;

                                    case (int)enums.OrcClass.SHAMAN:
                                        contentTypePath += " - Shaman/";
                                        break;

                                    case (int)enums.OrcClass.WARRIOR:
                                        contentTypePath += " - Warrior/";
                                        break;

                                    default:
                                        break;
                                }

                                mobSprites[i][n] = new Texture2D[(int)enums.EntityState.MAX];
                                // now we load all the spriteSheets.
                                for (int j = 0; j < (int)enums.EntityState.MAX; j++)
                                {

                                    switch (j)
                                    {
                                        case (int)enums.EntityState.RUN:
                                            mobSprites[i][n][j] = Content.Load<Texture2D>(contentTypePath + "Run-Sheet");
                                            break;

                                        case (int)enums.EntityState.IDLE:
                                            mobSprites[i][n][j] = Content.Load<Texture2D>(contentTypePath + "Idle-Sheet");
                                            break;

                                        case (int)enums.EntityState.DEATH:
                                            mobSprites[i][n][j] = Content.Load<Texture2D>(contentTypePath + "Death-Sheet");
                                            break;

                                        default:
                                            break;
                                    }
                                }
                            }
                            break;

                        case (int)enums.EnemyType.SKELETON:

                            mobSprites[i] = new Texture2D[(int)enums.SkeletonClass.MAX][];
                            contentPath += "Skeleton - ";

                            // now we append the enemyClass to the path of the spriteSheet

                            for (int n = 0; n < (int)enums.SkeletonClass.MAX; n++)
                            {
                                contentTypePath = contentPath;
                                switch (n)
                                {
                                    case (int)enums.SkeletonClass.BASE:
                                        contentTypePath += "Base/";
                                        break;

                                    case (int)enums.SkeletonClass.MAGE:
                                        contentTypePath += "Rogue/";
                                        break;

                                    case (int)enums.SkeletonClass.ROGUE:
                                        contentTypePath += "Mage/";
                                        break;

                                    case (int)enums.SkeletonClass.WARRIOR:
                                        contentTypePath += "Warrior/";
                                        break;

                                    default:
                                        break;
                                }

                                mobSprites[i][n] = new Texture2D[(int)enums.EntityState.MAX];

                                // now we load all the spriteSheets.

                                for (int j = 0; j < (int)enums.EntityState.MAX; j++)
                                {

                                    switch (j)
                                    {
                                        case (int)enums.EntityState.RUN:
                                            mobSprites[i][n][j] = Content.Load<Texture2D>(contentTypePath + "Run-Sheet");
                                            break;

                                        case (int)enums.EntityState.IDLE:
                                            mobSprites[i][n][j] = Content.Load<Texture2D>(contentTypePath + "Idle-Sheet");
                                            break;

                                        case (int)enums.EntityState.DEATH:
                                            mobSprites[i][n][j] = Content.Load<Texture2D>(contentTypePath + "Death-Sheet");
                                            break;

                                        default:
                                            break;
                                    }
                                }
                            }
                            break;                            
                    }
                }
                // now we give these spritesheets to the mob manager.
                this.mobManager.LoadContent(mobSprites);
            }

            // here we create an sprite-sheet array for all Heroes.
            {
                // TODO: change the paths when we change the sprites


                Texture2D[][] playerSprites = new Texture2D[(int)enums.PlayerClass.MAX][];

                for (int i = 0; i < (int)enums.PlayerClass.MAX; i++) {

                    string contentPath = "Heroes/";

                    playerSprites[i] = new Texture2D[(int)enums.EntityState.MAX];


                    // we change the path so we get every PlayerClass
                    switch (i) {
                        case (int)enums.PlayerClass.KNIGHT:
                            contentPath += "Knight/";
                            break;

                        default:
                            break;

                    }
                    // then we load for every PlayerClass all EntityStates.
                    for (int j = 0; j < (int)enums.EntityState.MAX; j++) {

                        switch (j) {
                            case (int)enums.EntityState.RUN:
                                playerSprites[i][j] = Content.Load<Texture2D>(contentPath + "Run-Sheet");
                                break;

                            case (int)enums.EntityState.IDLE:
                                playerSprites[i][j] = Content.Load<Texture2D>(contentPath + "Idle-Sheet");
                                break;

                            case (int)enums.EntityState.DEATH:
                                playerSprites[i][j] = Content.Load<Texture2D>(contentPath + "Death-Sheet");
                                break;

                            default:
                                    break;
                        }                    
                    }
                }
            
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            // we start the server thread after the server and all its stuff is initialized and running.
            if (!this.server.running && this.server.initialized) this.serverThread.Start();



            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            // TODO: Add your drawing code here
            

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
