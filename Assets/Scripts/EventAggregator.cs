using System;
using System.Collections.Generic;
using System.Linq;

public static class EventAggregator
{
    public static readonly Event<Mode> ModeSwitched = new();
    
    public class Event<T>
    {
        private readonly List<Action<T>> callbacks = new();

        public void Subscribe(Action<T> action)
        {
            callbacks.Add(action);
        }
    
        public void Publish(T obj)
        {
            foreach (var action in callbacks.ToList())
            {
                action(obj);
            }
        }

        public void Unsubscribe(Action<T> action)
        {
            callbacks.Remove(action);
        }
    }
}
