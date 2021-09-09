using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEditor.Experimental.SceneManagement;
using System;
using System.Threading.Tasks;

#if UNITY_2021_2_OR_NEWER
namespace UnityEditor.SceneManagement
#else
namespace UnityEditor.Experimental.SceneManagement
#endif
{
    public class PrefabStageCallbackManager : IDisposable
    {

        public class PrefabStageCallback : IComparable
        {
            public Action<PrefabStage> callback;
            public int order;
            public long timestamp;

            public PrefabStageCallback()
            {
                timestamp = DateTime.Now.Ticks;
            }

            public int CompareTo(object obj)
            {
                var that = obj as PrefabStageCallback;
                var delta = this.order - that.order;
                if (delta != 0)
                {
                    return delta;
                }
                else
                {
                    return (int)(timestamp - that.timestamp);
                }
            }

            internal void Invoke(PrefabStage obj)
            {
                callback(obj);
            }
        }

        public static PrefabStageCallbackManager _instance;
        public static PrefabStageCallbackManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PrefabStageCallbackManager();
                }
                return _instance;
            }
        }

        private List<PrefabStageCallback> openCallback = new List<PrefabStageCallback>();
        private List<PrefabStageCallback> closeCallback = new List<PrefabStageCallback>();
        public PrefabStageCallbackManager()
        {
            PrefabStage.prefabStageOpened += OnPrefabStageOpen;
            PrefabStage.prefabStageClosing += OnPrefabStageClose;
        }

        public void Dispose()
        {
            PrefabStage.prefabStageOpened -= OnPrefabStageOpen;
            PrefabStage.prefabStageClosing -= OnPrefabStageClose;
        }

        private void OnPrefabStageClose(PrefabStage obj)
        {
            foreach (var c in closeCallback)
            {
                c.Invoke(obj);
            }
        }

        private async void OnPrefabStageOpen(PrefabStage obj)
        {
            await Task.Delay(10);
            foreach (var c in openCallback)
            {
                c.Invoke(obj);
            }
        }

        public void RegisterOpen(Action<PrefabStage> callback, int order)
        {
            openCallback.Add(new PrefabStageCallback { callback = callback, order = order});
            openCallback.Sort();
        }

        public void DeregisterOpen(Action<PrefabStage> callback)
        {
            openCallback.RemoveAll(c => c.callback == callback);
        }

        public void RegisterClose(Action<PrefabStage> callback, int order)
        {
            closeCallback.Add(new PrefabStageCallback { callback = callback, order = order });
            closeCallback.Sort();
        }

        public void DeregisterClose(Action<PrefabStage> callback)
        {
            closeCallback.RemoveAll(c => c.callback == callback);
        }

    }
}
