﻿using System;
using System.Collections.Generic;
using System.Collections.Generic.Ex;

namespace mulova.commons
{
    public class DoubleBuffer<T> : Loggable where T:class
    {
        private readonly List<T> buf = new List<T>();
        private readonly List<T> temp = new List<T>();
        private readonly bool clear;
        private bool executing;

        public DoubleBuffer(bool clear = true)
        {
            this.clear = clear;
        }

        public void Add(T t)
        {
            if (executing)
            {
                log.Debug("Buffer is modified in between Exec()");
            }
            lock(buf)
            {
                buf.Add(t);
            }
        }

        public void Remove(T t)
        {
            if (executing)
            {
                log.Debug("Buffer is modified in between Exec()");
            }
            lock(buf)
            {
                buf.Remove(t);
            }
        }

        public void Clear()
        {
            lock(buf)
            {
                buf.Clear();
            }
        }

        public void Exec(Action<T> a)
        {
            if (buf.IsEmpty())
            {
                return;
            }
            if (executing)
            {
                log.Error("Recursive call of Exec");
                return;
            }
            lock(buf)
            {
                temp.AddRange(buf);
                if (clear)
                {
                    buf.Clear();
                }
            }
            executing = true;
            foreach (T t in temp)
            {
                a(t);
            }
            temp.Clear();
            executing = false;
        }
    }
}
