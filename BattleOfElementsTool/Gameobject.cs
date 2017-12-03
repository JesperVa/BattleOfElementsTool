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
        #region Member variables
        private Point OFFSET = new Point(80, 10);

        private Rectangle hitBox;
        private Color color;
        private Texture2D tex;
        private Vector2 origin;
        #endregion


        #region Constructor + Public methods
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
            Rectangle drawRect = new Rectangle(new Point(hitBox.Location.X - OFFSET.X, hitBox.Y - OFFSET.Y), hitBox.Size);

            sb.Draw(tex, drawRect, null, color, 0f, origin, SpriteEffects.None, 0f);
            
        }
        #endregion

    }
}
