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

namespace GTE
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameMain Main;

        public Game1() // Constructeur de la classe.
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight= 480;
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true; // Affiche la souris;
            //graphics.IsFullScreen = true; // met en full screen
        }

        protected override void Initialize()
        {
            base.Initialize();
        } // On s'en moque on ne touche pas

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Resources.LoadContent(Content);
            Main = new GameMain();
        } // Charge les contents

        protected override void UnloadContent()
        {
            Content.Unload();
        } // On s'en moque on ne touche pas.


        protected override void Update(GameTime gameTime)
        {
           Main.Updtate(Mouse.GetState(), Keyboard.GetState()); 
           base.Update(gameTime);
        } // Sert à mettre à jouer le jeu

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            Main.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        } // Dessine.
    }
}
