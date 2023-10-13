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
        /// <summary>
        /// if win true
        /// </summary>
        bool win;

        /// <summary>
        /// if lose true
        /// </summary>
        bool lose;

        /// <summary>
        /// the timer for the fireworks
        /// </summary>
        double timer = 0;

        /// <summary>
        /// updates the win loss screen when it is active 
        /// </summary>
        /// <param name="gameTime">the game time</param>
        /// <param name="keyboardState">the current keboardState</param>
        /// <param name="_fireworks">the fireworks</param>
        /// <returns></returns>
        public bool Update(GameTime gameTime, KeyboardState keyboardState, FireworkParticleSystem _fireworks)
        {
            timer += gameTime.ElapsedGameTime.TotalSeconds;
            
            if(timer > 0.5f)
            {
                _fireworks.PlaceFireWork(new Vector2(RandomHelper.Next(10, 700), RandomHelper.Next(10, 400)));
                timer = 0;
            }
            if (keyboardState.IsKeyDown(Keys.Escape)) return true;
            else return false;


        }

        /// <summary>
        /// checks to see if someone has won or lost 
        /// </summary>
        /// <param name="team1">the player's team</param>
        /// <param name="team2">the "ai" team </param>
        /// <returns></returns>
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
