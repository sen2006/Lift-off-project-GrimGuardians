using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Core;
using GXPEngine.Managers;

namespace GXPEngine
{

    public class LevelHandler
    {

        public enum LevelMode
        {
            Menu,
            Level,
            EndMenu
        }

        public static LevelMode levelMode = LevelMode.Level;

        private Game game;

        LevelHandler(Game game)
        {
            this.game = game;
        }

        public void Update()
        {
            if(levelMode == LevelMode.Menu)
            {

            }

            if(levelMode == LevelMode.Level)
            {

            }

            if( levelMode == LevelMode.EndMenu)
            {

            }
        }
    }
}
