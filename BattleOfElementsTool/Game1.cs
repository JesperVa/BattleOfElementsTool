using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using xnaInput = Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace BattleOfElementsTool
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        
        private string FILEPATH = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\BattleOfElements\Game0.txt";

        //Better to read out of %appdata%
        //private readonly string FILEPATH = @"E:\Spelmotordriven\BattleOfElementsTool\BattleOfElementsTool\Content\MatchHistory\Game10.txt";

        private const int SCREENWIDTH = 1280;
        private const int SCREENHEIGHT = 720;
        private const int SCALE = 10;
        private const int CAMERASPEED = 10;


        List<GameObject> platformList;
        Camera2D Camera;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Linedrawer lineDrawer;
        List<Vector2>[] positions;
        SpriteFont font;
        Color[] colors = new Color[4];
        public static Texture2D CircleTexture;
        public static Texture2D PixelTexture;
        public static Texture2D RectangleTexture;


        public Game1()
        {
            IsMouseVisible = true;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            OpenFileDialog FD = new OpenFileDialog();
            FD.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\BattleOfElements\";

            if(FD.ShowDialog() == DialogResult.OK)
            {
                if(!string.IsNullOrEmpty(FD.FileName))
                {
                    FILEPATH = FD.FileName;
                }
            }

            PixelTexture = new Texture2D(GraphicsDevice, 1, 1);
            CircleTexture = Content.Load<Texture2D>("Circle");
            RectangleTexture = Content.Load<Texture2D>("Rectangle");
            font = Content.Load<SpriteFont>("SpriteFont");
            
            colors[0] = Color.Red;
            colors[1] = Color.Orange;
            colors[2] = Color.Blue;
            colors[3] = Color.LightBlue;
            Camera = new Camera2D();
            Camera.Position = new Vector2(690, 340);

            graphics.PreferredBackBufferHeight = SCREENHEIGHT;
            graphics.PreferredBackBufferWidth = SCREENWIDTH;
            graphics.ApplyChanges();

            LoadData();

            lineDrawer = new Linedrawer(GraphicsDevice, positions, colors, SCALE);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (xnaInput.GamePad.GetState(PlayerIndex.One).Buttons.Back == xnaInput.ButtonState.Pressed || xnaInput.Keyboard.GetState().IsKeyDown(xnaInput.Keys.Escape))
                Exit();
            xnaInput.KeyboardState state = xnaInput.Keyboard.GetState();

            if (state.IsKeyDown(xnaInput.Keys.Left))
            {
                Camera.Move(new Vector2(1, 0) * CAMERASPEED);
            }
            if (state.IsKeyDown(xnaInput.Keys.Right))
            {
                Camera.Move(new Vector2(-1, 0) * CAMERASPEED);
            }
            if (state.IsKeyDown(xnaInput.Keys.Up))
            {
                Camera.Move(new Vector2(0, 1) * CAMERASPEED);
            }
            if (state.IsKeyDown(xnaInput.Keys.Down))
            {
                Camera.Move(new Vector2(0, -1) * CAMERASPEED);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            var screenScale = GetScreenScale();
            var viewMatrix = Camera.GetTransform();


            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied,
                                       null, null, null, null, viewMatrix * Matrix.CreateScale(screenScale));

            lineDrawer.Draw(spriteBatch);

            

            foreach (GameObject go in platformList)
            {
                go.Draw(spriteBatch);
            }

            spriteBatch.End();

            spriteBatch.Begin();
            //spriteBatch.DrawString(font, "X:" + Camera.Position.X + " ; Y:" + Camera.Position.Y, Vector2.Zero, Color.Black);
            spriteBatch.DrawString(font, "X:" + (xnaInput.Mouse.GetState().X - Camera.Position.X) + " ; Y:" + (xnaInput.Mouse.GetState().Y + Camera.Position.Y), Vector2.Zero, Color.Black);
            spriteBatch.End();


            base.Draw(gameTime);
        }

        private Vector3 GetScreenScale()
        {
            var scaleX = (float)GraphicsDevice.Viewport.Width / (float)SCREENWIDTH;
            var scaleY = (float)GraphicsDevice.Viewport.Height / (float)SCREENHEIGHT;
            return new Vector3(scaleX, scaleY, 1.0f);
        }


        private void LoadData()
        {
            StreamReader IO = new StreamReader(FILEPATH);
            List<string> mapInfo = new List<string>();

            while (IO.EndOfStream == false)
            {
                mapInfo.Add(IO.ReadLine());
            }
            IO.Close();

            ParseData(mapInfo);

        }

        private void ParseData(List<string> mapInfo)
        {
            string[] splitString = mapInfo[0].Split(':');
            int amountOfPlayers = int.Parse(splitString[1]);
            positions = new List<Vector2>[amountOfPlayers];
            int readLines = 1;

            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = new List<Vector2>();
            }

            for (int i = readLines; i < amountOfPlayers + readLines; i++)
            {
                splitString = mapInfo[i].Split(';');                
                for (int j = 0; j < splitString.Length - 1; j++)
                {
                    string[] temp = splitString[j].Split(',');
                    float X = float.Parse(temp[0]);
                    float Y = float.Parse(temp[1]) * -1;
                    positions[i-readLines].Add(new Vector2(X, Y));
                }
            }
            readLines += amountOfPlayers;

            platformList = new List<GameObject>();
            splitString = mapInfo[readLines].Split(';');
            for (int i = 0; i < splitString.Length - 1; i++)
            {
                
                string[] rectangleInfo = splitString[i].Split(':');
                string[] position = rectangleInfo[0].Split(',');
                string[] size = rectangleInfo[1].Split(',');
                Rectangle rectangle = new Rectangle((int)float.Parse(position[0]) * SCALE, (int)float.Parse(position[1]) * -1 * SCALE, (int)float.Parse(size[0]) * SCALE, (int)float.Parse(size[1]) * SCALE);
                //rectangle.Size = new Point(rectangle.Width * SCALE, rectangle.Height * SCALE);
                //rectangle.Location = new Point(rectangle.X * SCALE, rectangle.Y * SCALE);
                platformList.Add(new GameObject(rectangle, Color.Brown));
            }

            //platformList.Add(new GameObject(new Rectangle(5, 5, 100, 100), Color.Red));
        }
    }
}
