/*******************************************************************************
 *  HitPoints.cs: Encapsulates an Actor's HP.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

using Questar.Helpers;

namespace Questar.Actors
{
    public class HitPointsEventArgs : EventArgs
    {
        public HitPoints OldHitPoints;
    }

    public class HitPoints : ICloneable
    {
        public int current, max;

        public event EventHandler<HitPointsEventArgs> Changed;

        public HitPoints (int current, int max)
        {
            Max = max;
            Current = current;
        }

        public HitPoints (int max) : this (max, max)
        {
        }

        public int Current
        {
            get { return current; }
            set {
                int new_current = Math.Min (value, max);
                if (new_current == current)
                    return;

                HitPoints old = Clone ();
                current = new_current;
                OnChanged (old);
            }
        }

        public int Max
        {
            get { return max; }
            set {
                if (value == max)
                    return;

                HitPoints old = Clone ();
                max = value;
                OnChanged (old);
            }
        }

        private void OnChanged (HitPoints old)
        {
            EventHelper.Raise (this, Changed,
                delegate (HitPointsEventArgs args) {
                    args.OldHitPoints = old;
                });
        }

        public bool IsEmpty
        {
            get { return current == 0; }
        }

        public bool IsFull
        {
            get { return current == max; }
        }

        public double AsDouble
        {
            get {
                return (double) ((double) current / max);
            }
        }

        public override string ToString ()
        {
            return String.Format ("{0}/{1}", Current, Max);
        }

        public HitPoints Clone ()
        {
            return (HitPoints) base.MemberwiseClone ();
        }

        object ICloneable.Clone ()
        {
            return Clone ();
        }

        public override int GetHashCode ()
        {
            return ToString ().GetHashCode ();
        }

        public override bool Equals (object o)
        {
            HitPoints hp = o as HitPoints;

            if (hp == null)
                return false;
            if (Current != hp.Current)
                return false;
            if (Max != hp.Max)
                return false;

            return true;
        }

        public static bool operator == (HitPoints a, HitPoints b)
        {
            return Object.Equals (a, b);
        }

        public static bool operator != (HitPoints a, HitPoints b)
        {
            return !Object.Equals (a, b);
        }
    }
}

