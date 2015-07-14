#region Author: P Durrant

// This code was constructed as a learning exercise from publicly available documentation, online example code, and my own thoughts.

#endregion

using System;

namespace HDD.Utility
{
    static public class EventExtensions
    {
        public static void RaiseEvent(this EventHandler handler, object sender, EventArgs e)
        {
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        public static void RaiseEvent<T>(this EventHandler<EventArgs<T>> handler, object sender, T e)
        {
            if (handler != null)
            {
                handler(sender, new EventArgs<T>(e));
            }
        }
    }
}