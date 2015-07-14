#region Author: P Durrant

// This code was constructed as a learning exercise from publicly available documentation, online example code, and my own thoughts.

#endregion

using System;

namespace HDD.Utility
{
    public class EventArgs<T> : EventArgs
    {
        private T _value;

        public EventArgs(T value)
        {
            _value = value;
        }

        public T Value
        {
            get { return _value; }
        }
    }
}