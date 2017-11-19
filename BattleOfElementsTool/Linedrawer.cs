using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfElementsTool
{
    class Linedrawer
    {

        Texture2D tex;
        List<Vector2>[] positions;
        Color[] playerColors;
        private readonly int SCALE;

        public Linedrawer(GraphicsDevice gd, List<Vector2>[] positions)
        {
            tex = Game1.PixelTexture;


            this.positions = positions;
        }

        public Linedrawer(GraphicsDevice gd, List<Vector2>[] positions, Color[] playerColors, int scale = 1)
        {
            tex = new Texture2D(gd, 1, 1);

            this.positions = positions;
            this.playerColors = playerColors;
            SCALE = scale;
        }

        public void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                int temp = 0;
                for (int j = 0; j < positions[i].Count-1; j++)
                {
                    sb.Draw(Game1.CircleTexture, positions[i][j] * SCALE, null, playerColors[i], 0f, new Vector2(7.5f, 7.5f), 1f, SpriteEffects.None, 1f);
                    Primitives2D.DrawLine(sb, positions[i][j]* SCALE, positions[i][j + 1]* SCALE, playerColors[i], 5.0f);
                    temp = j;
                }
                sb.Draw(Game1.CircleTexture, positions[i][temp+1] * SCALE, null, playerColors[i], 0f, new Vector2(7.5f, 7.5f), 1f, SpriteEffects.None, 1f);
            }
        }
    }
}
