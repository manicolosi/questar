/*******************************************************************************
 *  Action.cs: Anything an Actor can do on its turn is representing by
 *  an Action. This is basically the Command Pattern.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

namespace Questar.Actors.Actions
{
    public interface Action
    {
        void Execute ();
    }
}

