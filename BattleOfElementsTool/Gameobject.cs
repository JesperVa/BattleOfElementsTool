using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfElementsTool
{
    class GameObject
    {
        private Rectangle hitBox;
        private Color color;
        private Texture2D tex;


        public GameObject(Rectangle hitBox, Color color)
        {
            this.hitBox = hitBox;
            this.color = color;
            this.tex = Game1.RectangleTexture;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, hitBox, color);
            
        }

    }
}
