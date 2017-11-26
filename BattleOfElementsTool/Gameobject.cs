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
        private Vector2 origin;


        public GameObject(Rectangle hitBox, Color color)
        {
            this.hitBox = hitBox;
            this.color = color;
            this.tex = Game1.RectangleTexture;
            //origin = new Vector2(hitBox.Width / 2, hitBox.Height);
            origin = Vector2.Zero;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, hitBox, null, color, 0f, origin, SpriteEffects.None, 0f);
            
        }

    }
}
