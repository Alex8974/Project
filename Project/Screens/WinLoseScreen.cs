using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ParticleSystemExample;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Screens
{
    public class WinLoseScreen
    {
        bool win;
        bool lose;
        double timer = 0;

        public bool Update(GameTime gameTime, KeyboardState keyboardState, FireworkParticleSystem _fireworks)
        {
            timer += gameTime.ElapsedGameTime.TotalSeconds;
            
            if(timer > 0.5f)
            {
                _fireworks.PlaceFireWork(new Vector2(RandomHelper.Next(10, 700), RandomHelper.Next(10, 400)));
            }
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
