using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GTE
{
    public class Resources
    {
        //STATIC FIELDS
        public static Texture2D texture_player, texture_pointer;
        //RESOURCES
        public static void LoadContent(ContentManager Content)
        {
            texture_pointer = Content.Load<Texture2D>("green_pointer");
            texture_player = Content.Load<Texture2D>("joueur_ct1");
        }
    }
}
