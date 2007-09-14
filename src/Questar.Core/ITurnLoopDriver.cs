/*******************************************************************************
 *  ITurnLoopDriver.cs: Classes that want to drive the TurnLoop must
 *  implement this interface.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

namespace Questar.Core
{
    public interface ITurnLoopDriver
    {
        void Start ();
        void Stop ();

        bool IsRunning { get; }
    }
}

