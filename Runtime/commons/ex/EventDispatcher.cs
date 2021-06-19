using System;

namespace mulova.commons
{
    public class EventDispatcher : Loggable
    {
        private DoubleBuffer<Action> callbackList;

        public EventDispatcher(bool oneshot)
        {
            callbackList = new DoubleBuffer<Action>(oneshot);
        }

        public void Broadcast()
        {
            callbackList.Exec(a =>
            {
                try
                {
                    a();
                } catch (Exception ex)
                {
                    log.Error(ex);
                }
            });
        }

        public void SetCallback(Action callback)
        {
            this.callbackList.Clear();
            this.callbackList.Add(callback);
        }

        public void AddCallback(Action callback)
        {
            if (callback != null)
            {
                this.callbackList.Add(callback);
            } else
            {
                log.Warn("Null callback is ignored");
            }
        }

        public void RemoveCallback(Action callback)
        {
            this.callbackList.Remove(callback);
        }

        public void Clear()
        {
            this.callbackList.Clear();
        }
    }




    public class EventDispatcher<T> : Loggable
    {
        private DoubleBuffer<Action<T>> callbackList;

        public EventDispatcher(bool oneshot = false)
        {
            callbackList = new DoubleBuffer<Action<T>>(oneshot);
        }

        public void Broadcast(T arg)
        {
            callbackList.Exec(a =>
            {
                try
                {
                    a(arg);
                } catch (Exception ex)
                {
                    log.Error(ex);
                }
            });
        }

        public void SetCallback(Action<T> callback)
        {
            this.callbackList.Clear();
            this.callbackList.Add(callback);
        }

        public void AddCallback(Action<T> callback)
        {
            if (callback != null)
            {
                this.callbackList.Add(callback);
            } else
            {
                log.Warn("Null callback is ignored");
            }
        }

        public void RemoveCallback(Action<T> callback)
        {
            this.callbackList.Remove(callback);
        }

        public void Clear()
        {
            this.callbackList.Clear();
        }
    }

    public class EventDispatcher<T,U> : Loggable
    {
        private DoubleBuffer<Action<T,U>> callbackList;

        public EventDispatcher(bool oneshot = false)
        {
            callbackList = new DoubleBuffer<Action<T,U>>(oneshot);
        }

        public void Broadcast(T t, U u)
        {
            callbackList.Exec(a =>
            {
                try
                {
                    a(t, u);
                } catch (Exception ex)
                {
                    log.Error(ex);
                }
            });
        }

        public void SetCallback(Action<T,U> callback)
        {
            this.callbackList.Clear();
            this.callbackList.Add(callback);
        }

        public void AddCallback(Action<T,U> callback)
        {
            if (callback != null)
            {
                this.callbackList.Add(callback);
            } else
            {
                log.Warn("Null callback is ignored");
            }
        }

        public void RemoveCallback(Action<T,U> callback)
        {
            this.callbackList.Remove(callback);
        }

        public void Clear()
        {
            this.callbackList.Clear();
        }
    }

    public class EventDispatcher<T,U,V> : Loggable
    {
        private DoubleBuffer<Action<T,U,V>> callbackList;

        public EventDispatcher(bool oneshot = false)
        {
            callbackList = new DoubleBuffer<Action<T,U,V>>(oneshot);
        }

        public void Broadcast(T t, U u, V v)
        {
            callbackList.Exec(a =>
            {
                try
                {
                    a(t, u, v);
                } catch (Exception ex)
                {
                    log.Error(ex);
                }
            });
        }

        public void SetCallback(Action<T,U,V> callback)
        {
            this.callbackList.Clear();
            this.callbackList.Add(callback);
        }

        public void AddCallback(Action<T,U,V> callback)
        {
            if (callback != null)
            {
                this.callbackList.Add(callback);
            } else
            {
                log.Warn("Null callback is ignored");
            }
        }

        public void RemoveCallback(Action<T,U,V> callback)
        {
            this.callbackList.Remove(callback);
        }

        public void Clear()
        {
            this.callbackList.Clear();
        }
    }
}