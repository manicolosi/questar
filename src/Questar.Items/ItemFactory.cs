/*******************************************************************************
 *  FactoryFixture.cs: A factory that creates Item objects.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Questar.Base;

namespace Questar.Items
{
    public class ItemFactory
    {
        public static ItemFactory Instance
        {
            get { return Singleton<ItemFactory>.Instance; }
        }

        private ItemFactory ()
        {
        }
    }
}

