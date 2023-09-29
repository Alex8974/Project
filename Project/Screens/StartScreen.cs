using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Screens
{
    public class StartScreen
    {
        private List<Person> StartscreenTeam = new List<Person>();

        public void Initilze(ContentManager c)
        {
            StartscreenTeam.Add(new SwordsMan(c, "Team1"));
            StartscreenTeam.Add(new SwordsMan(c, "Team2"));
            StartscreenTeam[0].Position = new Vector2(400, 327);
            StartscreenTeam[1].Position = new Vector2(450, 327);
        }

        public GameScreens Update(GameTime gT, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                return GameScreens.Running;
            }
            else return GameScreens.Start;
        }

        public void Draw(GameTime gameTime, SpriteBatch sb, SpriteFont f, Texture2D texture)
        {
            sb.Draw(texture, new Vector2(0, 0), Color.White);
            sb.DrawString(f, "Castle Crusaders", new Vector2(250, 150), Color.Black);
            sb.DrawString(f, "Press 'Enter' to start", new Vector2(300, 200), Color.Black, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            foreach (Person p in StartscreenTeam) p.Draw(sb, gameTime);
        }
    }
}
