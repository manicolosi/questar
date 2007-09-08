using System;

using Questar.Actors;
using Questar.Primitives;

namespace Questar.Items
{
	public abstract class AbstractItem : Item
	{
		private string name;
		private string description;
        private Location location;
        private Actor owner = null;
		
		public string Name
		{
			get { return name; }
			protected set { name = value; }
		}
		
		public string Description
		{
			get { return description; }
			protected set { description = value; }
        }

        public Location Location
        {
            get { return location; }
            protected set { location = value; }
        }

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
