using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public static class EventAggregator
{
    public static readonly Event<Mode> ModeSwitched = new();
    public static readonly Event<int> MetalUpdate = new();
    public static readonly Event<TurretBody> MouseOverTurret = new();
    public static readonly Event<Wall> MouseOverWall = new();
    public static readonly Event<TurretBody> ChooseUpgradeTurret = new();
    public static readonly Event<Wall> ChooseUpgradeWall = new();
    public static readonly Event<Barrier> ChooseUpgradeBarrier = new();


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
