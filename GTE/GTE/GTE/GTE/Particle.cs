using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GTE
{
    public class Particle
    {
        #region FIELDS

        Random rand;
        private Game1 game;
        private Texture2D texture;
        public Texture2D Texture  // Texture Utilisée
        { get { return texture; } set { texture = value; } }

        private Vector2 position;
        public Vector2 Position
        { get { return position; } set { position = value; } }

        private Vector2 velocity;
        public Vector2 Velocity { get { return velocity; } set { velocity = value ;} }        // Vitesse

        private float angle;
        public float Angle { get { return angle; } set { angle = value; } }            // Angle de rotation

        private float angularvelocity;
        public float AngularVelocity { get { return angularvelocity; } set { angularvelocity = value; } }    // Nouvelle vitesse   

        private Vector2 EmitterLocation;

        private int tdv;
        public int TDV { get { return tdv; } set { tdv = value; } }                // Temps de vie de la particule

        #endregion

        public Particle(Game1 game)
        {
            this.game = game;
        }


        public void Update()
        {
            foreach (Particle particle in game.Particle_list)
            {
                particle.TDV--;
                particle.position += particle.velocity;
                particle.angle += particle.angularvelocity;
            }

            for (int i = 0; i < game.Particle_list.Count; i++)
            {
                Particle particle = game.Particle_list[i];
                if (particle.TDV == 0)
                {
                    game.Particle_list.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particle particle in game.Particle_list)
            {
                spriteBatch.Draw(particle.texture, particle.position, null, Color.White, particle.angle, 
                    Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
        } 
    }
}
