/*******************************************************************************
 *  Entity.cs: This interface allows Actors and Items to be treated
 *  similarly.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Questar.Maps;

namespace Questar.Primitives
{
    public interface Entity
    {
        string Name { get; }
        string Description { get; }
        string Tile { get; } 
        Location Location { get; }
    }
}

