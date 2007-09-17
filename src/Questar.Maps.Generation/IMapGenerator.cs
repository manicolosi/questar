/*******************************************************************************
 *  IMapGenerator.cs: An interface responsible for creating Maps.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

using Questar.Maps;

namespace Questar.Maps.Generation
{
    public interface IMapGenerator
    {
        Map Generate ();
    }
}

