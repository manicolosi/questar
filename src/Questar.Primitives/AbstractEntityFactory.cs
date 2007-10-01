/*******************************************************************************
 *  AbstractEntityFactory.cs: An abstract implementation of
 *  IEntityFactory.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

using Questar.Helpers;

namespace Questar.Primitives
{
    public abstract class AbstractEntityFactory<T> : IEntityFactory<T>
        where T: Entity
    {
        public event EventHandler<EntityCreatedEventArgs> Created;

        public abstract T Create (string id);

        protected void OnCreation (Entity entity)
        {
            EventHelper.Raise (this, Created,
                delegate (EntityCreatedEventArgs args) {
                    args.Entity = entity;
                });
        }
    }
}

