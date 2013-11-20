using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GTE
{
    class Resources
    {
        // STATIC FIELDS
        public static Texture2D Player;

        // LOAD CONTENT
        public static void LoadContent(ContentManager Content)
        {
            Player = Content.Load<Texture2D>("player");
        }
    }
}
