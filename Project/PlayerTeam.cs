//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection.Metadata;
//using System.Security.Policy;
//using System.Text;
//using System.Threading.Tasks;

//namespace Project
//{
//    public class PlayerTeam
//    {
//        public List<Person> Army = new List<Person>();
//        ContentManager Content; 

//        public PlayerTeam(ContentManager c)
//        {
//            Content = c;
//        }

//        public void Initialize()
//        {

//        }

//        public void LoadContent()
//        {
//        }

//        public void Update(GameTime gT, KeyboardState prev, KeyboardState cur)
//        {
//            if (cur.IsKeyDown(Keys.A) && !prev.IsKeyDown(Keys.A))
//            {
//                Team1.Add(new SwordsMan(Content, "Team1"));
//            }

//            if (cur.IsKeyDown(Keys.S) && !prev.IsKeyDown(Keys.S))
//            {
//                Team2.Add(new SwordsMan(Content, "Team2"));
//            }


//            foreach (Person p in Team1) p.Update(gameTime);
//            foreach (Person p in Team2) p.Update(gameTime);
//        }

//        public void Draw(SpriteBatch sB)
//        {

//        }
//    }
//}
