/*******************************************************************************
 *  ImpossibleActionException.cs: An Exception for use when an Action is
 *  impossible.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

namespace Questar.Actors.Actions
{
    public class ImpossibleActionException : ApplicationException
    {
        public ImpossibleActionException (string message) : base (message)
        {
        }
    }
}

