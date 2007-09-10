/*******************************************************************************
 *  AbstractItem.cs: Provides an implementation of Item, used by
 *  inheriters in Questar.Items.Concrete namespace.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

using Questar.Actors;
using Questar.Helpers;
using Questar.Primitives;

namespace Questar.Items
{
    public abstract class AbstractItem : AbstractEntity, Entity, Item
    {
    }
}

