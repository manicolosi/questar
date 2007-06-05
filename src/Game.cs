/*******************************************************************************
 *  Game.cs: FIXME
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

namespace Questar.Base
{
    public class Game
    {
        public static Game Instance
        {
            get { return Singleton<Game>.Instance; }
        }

        private Game ()
        {
        }
    }
}

