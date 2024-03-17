using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RValley.Entities;
using RValley.Maps;
using System.Threading;

namespace RValley
{
    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Thread serverThread;
        private Server.Server server;
        private Client.Client client;
        private Player player;
        private MobManager mobManager;
        private MapManager mapManager;
        private Texture2D[][] playerSprites;
        public enums.GameState gameState;
        private int[] screenSize;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.screenSize = new int[2] { 1800, 1000 };

            this._graphics.PreferredBackBufferWidth = this.screenSize[0];
            this._graphics.PreferredBackBufferHeight = this.screenSize[1];
            this._graphics.ApplyChanges();

            this.gameState = enums.GameState.MENU;
            this.player = new Player();
            this.mobManager = new MobManager();
            this.server = new Server.Server(this.screenSize);
            this.client = new Client.Client(this.server);

            this.serverThread = new Thread(this.server.Update);
            this.serverThread.Start();
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

                                mobSprites[i][n] = new Texture2D[(int)enums.EntityState.MAXA];
                                // now we load all the spriteSheets.
                                for (int j = 0; j < (int)enums.EntityState.MAXA; j++)
                                {

                                    switch (j)
                                    {
                                        case (int)enums.EntityState.RUN_R:
                                            mobSprites[i][n][j] = Content.Load<Texture2D>(contentTypePath + "Run-Sheet");
                                            break;

                                        case (int)enums.EntityState.RUN_L:
                                            mobSprites[i][n][j] = Content.Load<Texture2D>(contentTypePath + "Run-Sheet");
                                            break;

                                        case (int)enums.EntityState.IDLE_R:
                                            mobSprites[i][n][j] = Content.Load<Texture2D>(contentTypePath + "Idle-Sheet");
                                            break;

                                        case (int)enums.EntityState.IDLE_L:
                                            mobSprites[i][n][j] = Content.Load<Texture2D>(contentTypePath + "Idle-Sheet");
                                            break;

                                        case (int)enums.EntityState.DEATH_R:
                                            mobSprites[i][n][j] = Content.Load<Texture2D>(contentTypePath + "Death-Sheet");
                                            break;

                                        case (int)enums.EntityState.DEATH_L:
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

                                mobSprites[i][n] = new Texture2D[(int)enums.EntityState.MAXA];

                                // now we load all the spriteSheets.

                                for (int j = 0; j < (int)enums.EntityState.MAXA; j++)
                                {

                                    switch (j)
                                    {
                                        case (int)enums.EntityState.RUN_R:
                                            mobSprites[i][n][j] = Content.Load<Texture2D>(contentTypePath + "Run-Sheet");
                                            break;

                                        case (int)enums.EntityState.RUN_L:
                                            mobSprites[i][n][j] = Content.Load<Texture2D>(contentTypePath + "Run-Sheet");
                                            break;

                                        case (int)enums.EntityState.IDLE_R:
                                            mobSprites[i][n][j] = Content.Load<Texture2D>(contentTypePath + "Idle-Sheet");
                                            break;

                                        case (int)enums.EntityState.IDLE_L:
                                            mobSprites[i][n][j] = Content.Load<Texture2D>(contentTypePath + "Idle-Sheet");
                                            break;

                                        case (int)enums.EntityState.DEATH_R:
                                            mobSprites[i][n][j] = Content.Load<Texture2D>(contentTypePath + "Death-Sheet");
                                            break;

                                        case (int)enums.EntityState.DEATH_L:
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
                this.server.mobManager.LoadContent(mobSprites);
            }
            
            // here we load the maps (currently only one)
            {
                Texture2D mapSprite = Content.Load<Texture2D>("Environment/Maps/grass");
                this.server.mapManager.LoadContent(mapSprite);
            }

            // here we create an sprite-sheet array for all Heroes.
            {
                // TODO: change the paths when we change the sprites


                this.playerSprites = new Texture2D[(int)enums.PlayerClass.MAX][];

                for (int i = 0; i < (int)enums.PlayerClass.MAX; i++) {

                    string contentPath = "Heroes/";
                    this.playerSprites[i] = new Texture2D[(int)enums.EntityState.MAXA];

                    // we change the path so we get every PlayerClass
                    switch (i) {
                        case (int)enums.PlayerClass.KNIGHT:
                            contentPath += "Knight/";
                            break;

                            // TODO: ADD OTHER CHARACTERS

                        default:
                            break;
                    }
                    // then we load for every PlayerClass all EntityStates.
                    for (int j = 0; j < (int)enums.EntityState.MAXA; j++) {

                        switch (j) {
                            case (int)enums.EntityState.RUN_R:
                                this.playerSprites[i][j] = Content.Load<Texture2D>(contentPath + "Run-Sheet");
                                break;

                            case (int)enums.EntityState.RUN_L:
                                this.playerSprites[i][j] = Content.Load<Texture2D>(contentPath + "RunL-Sheet");
                                break;

                            case (int)enums.EntityState.IDLE_R:
                                this.playerSprites[i][j] = Content.Load<Texture2D>(contentPath + "Idle-Sheet");
                                break;

                            case (int)enums.EntityState.IDLE_L:
                                this.playerSprites[i][j] = Content.Load<Texture2D>(contentPath + "IdleL-Sheet");
                                break;

                            case (int)enums.EntityState.DEATH_R:
                                this.playerSprites[i][j] = Content.Load<Texture2D>(contentPath + "Death-Sheet");
                                break;

                            case (int)enums.EntityState.DEATH_L:
                                this.playerSprites[i][j] = Content.Load<Texture2D>(contentPath + "DeathL-Sheet");
                                break;

                            default:
                                    break;
                        }                    
                    }
                }
                // here we add those sprites
                this.server.player[0].LoadContent(this.playerSprites[(int)enums.PlayerClass.KNIGHT]);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.server.running = false;
                Exit();
            }

            // THIS HERE WE WANT TO DO WHEN WE CHOOSE PLAYERCLASS INGAME!!!

            switch (this.gameState) {
                case enums.GameState.MENU:

                    break;

                case enums.GameState.INGAME:
                    this.client.Update();
                    break;            
            }

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

            switch (this.gameState) 
            {
                case enums.GameState.INGAME:
                    _spriteBatch = this.InGameDraw(_spriteBatch);
                    break;

                case enums.GameState.MENU:
                    if (this.server.player[0].spriteSheets != null) this.gameState = enums.GameState.INGAME;
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private SpriteBatch InGameDraw(SpriteBatch spriteBatch) 
        {

            spriteBatch = this.client.Draw(spriteBatch);

            return spriteBatch;
        }
    }
}
