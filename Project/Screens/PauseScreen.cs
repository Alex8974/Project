using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Screens
{
    public class PauseScreen
    {
        public bool Update(GameTime gameTime, KeyboardState keboardState)
        {
            if (keboardState.IsKeyDown(Keys.Escape)) return true;
            else return false;                
        }

        public void Draw(GameTime gT, SpriteBatch sb, SpriteFont sf)
        {
            sb.DrawString(sf, "Game Paused", new Vector2(250, 150), Color.Black);
            sb.DrawString(sf, "(P) to Unpause", new Vector2(325, 200), Color.Black, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            sb.DrawString(sf, "(ESC) to Quit", new Vector2(325, 225), Color.Black, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
        }
    }
}
