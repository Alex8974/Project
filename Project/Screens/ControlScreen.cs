using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Screens
{
    public class ControlScreen
    {
        private List<Person> ControlScreenTeam = new List<Person>();

        public void Initilze(ContentManager c)
        {
            ControlScreenTeam.Add(new SwordsMan(c, "Team1"));
            ControlScreenTeam.Add(new Archer(c, "Team1"));
            ControlScreenTeam[0].Position = new Vector2(19, 95);
            ControlScreenTeam[1].Position = new Vector2(19, 135);
            ControlScreenTeam[0].frameRow = SpriteSheetPicker.SwordRight;
            ControlScreenTeam[1].frameRow = SpriteSheetPicker.BowRight;
        }

        public GameScreens Update(GameTime gT, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.E))
            {
                return GameScreens.Start;
            }
            else return GameScreens.Controls;
        }

        public void Draw(GameTime gameTime, SpriteBatch sb, SpriteFont f, Texture2D texture)
        {
            //sb.Draw(texture, new Vector2(0, 0), Color.White);
            sb.DrawString(f, "Controls", new Vector2(50, 50), Color.White);
            sb.DrawString(f, "Press (A) to summon a swordsman", new Vector2(50, 100), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            sb.DrawString(f, "Press (S) to summon a archer", new Vector2(50, 140), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            sb.DrawString(f, "Press (P) to pause the game", new Vector2(50, 180), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            sb.DrawString(f, "Press (E) to return to start menu", new Vector2(50, 220), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            foreach (Person p in ControlScreenTeam) p.Draw(sb, gameTime);
        }
    }
}
