/*******************************************************************************
 *  IEntityFactory.cs: Factories that create Entities must implement
 *  this interface.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

namespace Questar.Primitives
{
    public interface IEntityFactory<T> where T: Entity
    {
        T Create (string id);
        event EventHandler<EventArgs> Created;
    }
}

