using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Screens
{
    public class WinLoseScreen
    {
        bool win;
        bool lose;

        public bool Update(GameTime gameTime, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Escape)) return true;
            else return false;
        }

        public bool[] CheckforWin(List<Person> team1, List<Person> team2)
        {
            bool[] winLoss = new bool[2];
            winLoss[0] = false;
            winLoss[0] = false;
            foreach (Person p in team1) if (p.Position.X > 750 && p.IsAlive == true) winLoss[0] = true;
            foreach (Person p in team2) if (p.Position.X < 0 && p.IsAlive == true) winLoss[1] = true;
            return winLoss;
        }
    }
}
