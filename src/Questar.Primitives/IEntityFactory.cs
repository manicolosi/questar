/*******************************************************************************
 *  IEntityFactory.cs: Factories that create Entities must implement
 *  this interface.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Questar.Maps;

namespace Questar.Primitives
{
    public interface IEntityFactory<T> where T: Entity
    {
        T Create (string);
        event EventHandler<EventArgs> Created;
    }
}

