using System;

using Questar.Actors;
using Questar.Primitives;

namespace Questar.Items
{
	public abstract class AbstractItem : AbstractEntity, Entity, Item
	{
        private Actor owner = null;

        public Actor Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        public bool IsOwned
        {
            get { return owner != null; }
        }
	}
}
