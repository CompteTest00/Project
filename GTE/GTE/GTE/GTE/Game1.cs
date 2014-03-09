
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public bool IsMousevisible { get; set;}

        public int screenheight, screenwidth;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Player player;
        public Bullet bullet;
        public Enemy enemy;
        public List<Enemy> Enemy_List;
        public List<Particle> Particle_list;
        public Particle particle;

        //Weapons
        Weapons weapon;

        //Gestion des GameState
        public enum GameState
        {Intro, Menu, Options, Selection, Play, Multi, Arena, Pause}
        public GameState CurrentGameState = GameState.Menu;

        //Sons
        SoundEffect
        effect_sniper, effect_rifle, effect_gun, effect_bazooka, effect_shotgun, effect_reload;
        Song
        song_menu; //song_selection, song_play, song_arena;

        //Buttons
        Button /*Menu*/buttonPlay, buttonMulti, buttonOptions, buttonExit,
               /*Multi*/buttonPlayMulti,
               /*Options*/buttonVolume, buttonResolution, buttonReturn;


        public Game1()
        {
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
            // TODO: Add your initialization logic here

            base.Initialize();
            player = new Player(this);
            bullet = new Bullet(this);
            weapon = new Weapons(this);
            particle = new Particle(this);
            Enemy_List = new List<Enemy>();
            Particle_list = new List<Particle>();
            enemy = new Enemy(this);
            player.Initialize();
            bullet.Initialize();
            enemy.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            screenwidth = graphics.GraphicsDevice.Viewport.Width;
            screenheight = graphics.GraphicsDevice.Viewport.Height;
            Resources.LoadContent(Content);

            //Menu
            buttonPlay = new Button(Content.Load<Texture2D>("green_pointer"), graphics.GraphicsDevice);
            buttonPlay.setPosition(new Vector2((int)screenwidth / 4, (int)3 * screenheight / 10));
            buttonMulti = new Button(Content.Load<Texture2D>("green_pointer"), graphics.GraphicsDevice);
            buttonMulti.setPosition(new Vector2((int)screenwidth / 4, (int)2 * screenheight / 5 + screenheight / 14));
            buttonOptions = new Button(Content.Load<Texture2D>("green_pointer"), graphics.GraphicsDevice);
            buttonOptions.setPosition(new Vector2((int)screenwidth / 4, (int)2 * screenheight / 5 + 2 * screenheight / 7));
            buttonExit = new Button(Content.Load<Texture2D>("green_pointer"), graphics.GraphicsDevice);
            buttonExit.setPosition(new Vector2((int)3 * screenwidth / 4, (int)4 * screenheight / 5));

            //Multi
            buttonPlayMulti = new Button(Content.Load<Texture2D>("green_pointer"), graphics.GraphicsDevice);
            buttonPlayMulti.setPosition(new Vector2((int)screenwidth / 4, (int)3 * screenheight / 10));

            //Pause

            //Options
            buttonResolution = new Button(Content.Load<Texture2D>("green_pointer"), graphics.GraphicsDevice);
            buttonResolution.setPosition(new Vector2((int)screenwidth / 4, (int)3 * screenheight / 10));
            buttonVolume = new Button(Content.Load<Texture2D>("green_pointer"), graphics.GraphicsDevice);
            buttonVolume.setPosition(new Vector2((int)screenwidth / 4, (int)2 * screenheight / 5 + screenheight / 14));
            buttonReturn = new Button(Content.Load<Texture2D>("green_pointer"), graphics.GraphicsDevice);
            buttonReturn.setPosition(new Vector2((int)3 * screenwidth / 4, (int)4 * screenheight / 5));

            //Load des Sons
            song_menu = Content.Load<Song>("song_menu");

            //Lecture des Sons
            MediaPlayer.IsRepeating = true;
            switch (CurrentGameState)
            {

                case GameState.Intro:
                    {
                        //MediaPlayer.Play(video_intro);
                        break;
                    }

                case GameState.Menu:
                    {
                        MediaPlayer.Play(song_menu);
                        break;
                    }

                case GameState.Multi:
                    {
                        //MediaPlayer.Play(song_multi);
                        break;
                    }

                case GameState.Options:
                    {
                        MediaPlayer.Play(song_menu);
                        break;
                    }

                case GameState.Pause:
                    {
                        break;
                    }
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            MouseState mouse = Mouse.GetState();
            this.IsMousevisible = true;

            // Allows the game to exit
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) == true && CurrentGameState != (GameState.Play) && CurrentGameState != (GameState.Arena))
                //this.Exit();

            // TODO: Add your update logic here
            switch (CurrentGameState)
            {
                case GameState.Menu:
                    {
                        buttonPlay.Update(mouse);
                        buttonMulti.Update(mouse);
                        buttonOptions.Update(mouse);
                        buttonExit.Update(mouse);

                        if (buttonPlay.isClicked == true)
                            CurrentGameState = GameState.Play;
                        if (buttonMulti.isClicked == true)
                            CurrentGameState = GameState.Multi;
                        if (buttonOptions.isClicked == true)
                            CurrentGameState = GameState.Options;
                        if (buttonExit.isClicked == true)
                            this.Exit();

                        base.Update(gameTime);
                        break;
                    }

                case GameState.Options:
                    {
                        buttonResolution.Update(mouse);
                        buttonVolume.Update(mouse);
                        buttonReturn.Update(mouse);

                        if (buttonReturn.isClicked == true)
                            CurrentGameState = GameState.Menu;

                        base.Update(gameTime);
                        break;
                    }

                case GameState.Play:
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Escape) == true)
                            CurrentGameState = GameState.Pause;

                        player.Update(Mouse.GetState(), Keyboard.GetState());
                        bullet.Update();
                        enemy.Update();
                        particle.Update();
                        weapon.Update(gameTime);

                        base.Update(gameTime);
                        break;
                    }

                case GameState.Multi:
                    {
                        buttonPlayMulti.Update(mouse);
                        buttonReturn.Update(mouse);

                        if (buttonPlayMulti.isClicked == true)
                            CurrentGameState = GameState.Arena;
                        if (buttonReturn.isClicked == true)
                            CurrentGameState = GameState.Menu;

                        break;
                    }

                case GameState.Pause:
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Escape) == true)
                            CurrentGameState = GameState.Play;
                        if (buttonReturn.isClicked == true)
                            CurrentGameState = GameState.Menu;

                        base.Update(gameTime);
                        break;
                    }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
          // TODO: Add your drawing code here
            switch (CurrentGameState)
            {
                case GameState.Menu:
                    {
                        GraphicsDevice.Clear(Color.Black);

                        spriteBatch.Begin();
                        buttonPlay.Draw(spriteBatch);
                        buttonMulti.Draw(spriteBatch);
                        buttonOptions.Draw(spriteBatch);
                        buttonExit.Draw(spriteBatch);
                        spriteBatch.End();

                        base.Draw(gameTime);
                        break;
                    }

                case GameState.Options:
                    {
                        GraphicsDevice.Clear(Color.Purple);

                        spriteBatch.Begin();
                        buttonResolution.Draw(spriteBatch);
                        buttonVolume.Draw(spriteBatch);
                        buttonReturn.Draw(spriteBatch);
                        spriteBatch.End();

                        base.Draw(gameTime);
                        break;
                    }

                case GameState.Play:
                    {
                        GraphicsDevice.Clear(Color.CornflowerBlue);

                        spriteBatch.Begin();
                        player.Draw(spriteBatch);
                        bullet.Draw(spriteBatch);
                        enemy.Draw(spriteBatch);
                        particle.Draw(spriteBatch);
                        spriteBatch.End();

                        base.Draw(gameTime);
                        break;
                    }

                case GameState.Multi:
                    {
                        GraphicsDevice.Clear(Color.Red);

                        spriteBatch.Begin();
                        buttonPlayMulti.Draw(spriteBatch);
                        buttonReturn.Draw(spriteBatch);
                        spriteBatch.End();

                        base.Draw(gameTime);
                        break;
                    }

                case GameState.Arena:
                    {
                        spriteBatch.Begin();
                        spriteBatch.End();

                        base.Draw(gameTime);
                        break;
                    }

                case GameState.Pause:
                    {
                        GraphicsDevice.Clear(Color.Gray);

                        spriteBatch.Begin();
                        buttonReturn.Draw(spriteBatch);
                        spriteBatch.End();

                        base.Draw(gameTime);
                        break;
                    }
            }
        }
    }
}
