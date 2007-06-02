//
// HitPoints.cs: Represents an actor's HP.
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

using System;

using Questar.Base;

namespace Questar.Actors
{
    public class HitPointsEventArgs : EventArgs
    {
        public HitPoints OldHitPoints;

        public HitPointsEventArgs ()
        {
        }
    }

    public class HitPoints : ICloneable
    {
        public int current, max;

        public event EventHandler<HitPointsEventArgs> Changed;

        public HitPoints (int current, int max)
        {
            this.current = current;
            this.max = max;
        }

        public int Current
        {
            get { return current; }
            set {
                HitPoints old = Clone () as HitPoints;
                current = value;

                Events.FireEvent<HitPointsEventArgs> (this, Changed,
                    delegate (HitPointsEventArgs args) {
                        args.OldHitPoints = old;
                    });
            }
        }

        public int Max
        {
            get { return max; }
            set {
                HitPoints old = Clone () as HitPoints;
                max = value;

                Events.FireEvent<HitPointsEventArgs> (this, Changed,
                    delegate (HitPointsEventArgs args) {
                        args.OldHitPoints = old;
                    });
            }
        }

        public override string ToString ()
        {
            return String.Format ("{0}/{1}", Current, Max);
        }

        public object Clone ()
        {
            return base.MemberwiseClone ();
        }
    }
}

