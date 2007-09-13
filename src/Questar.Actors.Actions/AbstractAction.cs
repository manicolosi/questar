/*******************************************************************************
 *  AbstractAction.cs: This is an abstract implementation of the Action
 *  interface. It provides functionality that makes concrete
 *  implementations of Action easier.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Questar.Actors;

namespace Questar.Actors.Actions
{
    public abstract class AbstractAction : Action
    {
        private Actor actor;

        public AbstractAction (Actor actor)
        {
            this.actor = actor;
        }

        public Actor Actor
        {
            get { return this.actor; }
            protected set { this.actor = value; }
        }

        public abstract void Execute ();
    }
}

