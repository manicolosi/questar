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
    public class EntityCreatedEventArgs : EventArgs
    {
        public Entity Entity;
    }

    public interface IEntityFactory<T> where T: Entity
    {
        event EventHandler<EntityCreatedEventArgs> Created;
        T Create (string id);
    }
}

