using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Threading;

namespace GTE
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region FIELDS
        public bool IsMousevisible { get; set;}

        public int screenheight, screenwidth;
        Texture2D background;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Player player;
        public Bullet bullet;
        public Enemy enemy;
        public List<Enemy> Enemy_List;
        public List<Particle> Particle_list;
        public Particle particle;
        public Camera camera;
        public Map map1;

        //Weapons
        Weapons weapon;

        //Gestion des GameState
        public enum GameState
        {Intro, Menu, Options, Selection, Play, Multi, Arena, Pause}
        public GameState CurrentGameState = GameState.Menu;
        public KeyboardState oldState;

        //Sons
        public SoundEffect
        seffect_sniper, seffect_rifle, seffect_gun, seffect_bazooka, seffect_reload;
        public Song
        song_menu; //song_selection, song_play, song_arena;

        //Buttons
        Button /*Menu*/buttonPlay, buttonMulti, buttonOptions, buttonExit,
               /*Multi*/buttonPlayOnline, buttonPlayLocal,
               /*Options*/buttonVolume, buttonResolution, buttonReturn;
        #endregion

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
            camera = new Camera(GraphicsDevice.Viewport, this);
            map1 = new Map();
            map1.Generate(map1.Create_Map(0), 32);
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

            #region Resources des ecrans du jeu
            //Menu
            buttonPlay = new Button(Content.Load<Texture2D>("Jouer"), graphics.GraphicsDevice);
            buttonPlay.setPosition(new Vector2((int)screenwidth / 4, (int)3 * screenheight / 10));
            buttonMulti = new Button(Content.Load<Texture2D>("Multijoueur"), graphics.GraphicsDevice);
            buttonMulti.setPosition(new Vector2((int)screenwidth / 4, (int)2 * screenheight / 5 + screenheight / 14));
            buttonOptions = new Button(Content.Load<Texture2D>("Option"), graphics.GraphicsDevice);
            buttonOptions.setPosition(new Vector2((int)screenwidth / 4, (int)2 * screenheight / 5 + 2 * screenheight / 7));
            buttonExit = new Button(Content.Load<Texture2D>("Quitter"), graphics.GraphicsDevice);
            buttonExit.setPosition(new Vector2((int)3 * screenwidth / 4, (int)4 * screenheight / 5));

            background = Content.Load<Texture2D>("GrandTheftEpicMainTitle");


            //Multi
            buttonPlayOnline = new Button(Content.Load<Texture2D>("En ligne"), graphics.GraphicsDevice);
            buttonPlayOnline.setPosition(new Vector2((int)screenwidth / 4, (int)3 * screenheight / 10));
            buttonPlayLocal = new Button(Content.Load<Texture2D>("Local"), graphics.GraphicsDevice);
            buttonPlayLocal.setPosition(new Vector2((int)screenwidth / 4, (int)2 * screenheight / 5 + screenheight / 14));

            //Pause

            //Options
            buttonResolution = new Button(Content.Load<Texture2D>("Resolution"), graphics.GraphicsDevice);
            buttonResolution.setPosition(new Vector2((int)screenwidth / 4, (int)3 * screenheight / 10));
            buttonVolume = new Button(Content.Load<Texture2D>("Volume"), graphics.GraphicsDevice);
            buttonVolume.setPosition(new Vector2((int)screenwidth / 4, (int)2 * screenheight / 5 + screenheight / 14));
            buttonReturn = new Button(Content.Load<Texture2D>("Retour"), graphics.GraphicsDevice);
            buttonReturn.setPosition(new Vector2((int)3 * screenwidth / 4, (int)6 * screenheight / 10));
            #endregion

            #region Resources des songs et soundeffects
            //Load des Sons
            song_menu = Content.Load<Song>("song_menu");

            //Load des Effects
            seffect_gun = Content.Load<SoundEffect>("seffect_gun");
            //seffect_reload = Content.Load<SoundEffect>("effect_reload");
            #endregion

            #region Musique du jeu
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

                case GameState.Play:
                    {
                        //MediaPlayer.Play();
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
            #endregion
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

            // TODO: Add your update logic here
            switch (CurrentGameState)
            {
                #region Menu
                case GameState.Menu:
                    {
                        this.IsMouseVisible = true;

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
                #endregion

                #region Options
                case GameState.Options:
                    {
                        this.IsMouseVisible = true;

                        buttonResolution.Update(mouse);
                        buttonVolume.Update(mouse);
                        buttonReturn.Update(mouse);

                        if (buttonReturn.isClicked == true)
                            CurrentGameState = GameState.Menu;

                        base.Update(gameTime);
                        break;
                    }
                #endregion

                #region Play
                case GameState.Play:
                    {
                        var newState = new KeyboardState(Keys.Escape);
                        this.IsMouseVisible = false;

                        if (Keyboard.GetState().IsKeyDown(Keys.Escape) == true && newState.IsKeyDown(Keys.Down) && !oldState.IsKeyDown(Keys.Down))
                            CurrentGameState = GameState.Pause;

                        player.Update(Mouse.GetState(), Keyboard.GetState());
                        bullet.Update();
                        enemy.Update();
                        particle.Update();
                        weapon.Update(gameTime);
                        camera.Update(gameTime, this);
                        base.Update(gameTime);
                        break;
                    }
                #endregion

                #region Multi
                case GameState.Multi:
                    {
                        this.IsMouseVisible = true;

                        buttonPlayOnline.Update(mouse);
                        buttonReturn.Update(mouse);

                        if (buttonPlayOnline.isClicked == true)
                            CurrentGameState = GameState.Arena;
                        if (buttonReturn.isClicked == true)
                            CurrentGameState = GameState.Menu;

                        base.Update(gameTime);
                        break;
                    }
                #endregion

                #region Pause
                case GameState.Pause:
                    {
                        var newState = new KeyboardState(Keys.Escape);
                        this.IsMouseVisible = true;

                        if (Keyboard.GetState().IsKeyDown(Keys.Escape) == true && newState.IsKeyDown(Keys.Down) && !oldState.IsKeyDown(Keys.Down))
                            CurrentGameState = GameState.Play;
                        if (buttonReturn.isClicked == true)
                            CurrentGameState = GameState.Menu;

                        base.Update(gameTime);
                        break;
                    }
                #endregion
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
                #region Menu
                case GameState.Menu:
                    {
                        spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
                        spriteBatch.Draw(background, new Rectangle(0,0,screenwidth, screenheight), Color.White);
                        spriteBatch.End();

                        spriteBatch.Begin();
                        buttonPlay.Draw(spriteBatch);
                        buttonMulti.Draw(spriteBatch);
                        buttonOptions.Draw(spriteBatch);
                        buttonExit.Draw(spriteBatch);
                        spriteBatch.End();

                        base.Draw(gameTime);
                        break;
                    }
                #endregion

                #region Options
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
                #endregion

                #region Play
                case GameState.Play:
                    {
                        GraphicsDevice.Clear(Color.Black);

                        spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,null,null,null,null,camera.Transform);
                        map1.Draw(spriteBatch);
                        player.Draw(spriteBatch);
                        bullet.Draw(spriteBatch);
                        enemy.Draw(spriteBatch);
                        particle.Draw(spriteBatch);
                        spriteBatch.End();

                        base.Draw(gameTime);
                        break;
                    }
                #endregion

                #region Multi
                case GameState.Multi:
                    {
                        GraphicsDevice.Clear(Color.Red);

                        spriteBatch.Begin();
                        buttonPlayOnline.Draw(spriteBatch);
                        buttonPlayLocal.Draw(spriteBatch);
                        buttonReturn.Draw(spriteBatch);
                        spriteBatch.End();

                        base.Draw(gameTime);
                        break;
                    }
                #endregion

                #region Arena
                case GameState.Arena:
                    {
                        spriteBatch.Begin();
                        spriteBatch.End();

                        base.Draw(gameTime);
                        break;
                    }
                #endregion

                #region Pause
                case GameState.Pause:
                    {
                        GraphicsDevice.Clear(Color.Gray);

                        spriteBatch.Begin();
                        buttonReturn.Draw(spriteBatch);
                        spriteBatch.End();

                        base.Draw(gameTime);
                        break;
                    }
                #endregion
            }
        }
    }
}
