using System.Collections.Generic;
using Client.Scripts;
using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Shared.Helpers;

namespace ZeroZeroGames.Ecs.Modules.Shared.Data
{
    public class MonobDictionary<T2> where T2 : IGetMonoB
    {

        private readonly Dictionary<string, RuntimeData.ObjPrefabCopyPair<IGetMonoB>> _objs = new();


        public void DebugDict()
        {
            EventHelpers.RequestLog("Старт дебага словаря");
            foreach (var el in _objs) EventHelpers.RequestLog($"{el.Key} : {el.Value.prefab == null}");
        }

        public void RegisterCanvasObj<T>(T prefab) where T : T2
        {
            _objs.TryAdd(typeof(T).Name,
                new RuntimeData.ObjPrefabCopyPair<IGetMonoB>
                {
                    prefab =
                        prefab.GetObj().GetComponent<T>()
                });
        }

        public T GetOrCreateObj<T>() where T : T2
        {
            return HasObj<T>() ? GetObj<T>() : CreateObj<T>();
        }

        private T CreateObj<T>() where T : T2
        {
            var obj = PrefabHelpers.InstantiateFromPrefab(_objs[typeof(T).Name].prefab.GetObj());
            _objs[typeof(T).Name].copy = obj.GetComponent<T>();
            return obj.GetComponent<T>();
        }

        public T GetObj<T>() where T : T2
        {
            return (T)_objs[typeof(T).Name].copy;
        }

        public bool HasObj<T>() where T : T2
        {
            return _objs.ContainsKey(typeof(T).Name) &&
                   _objs[typeof(T).Name].copy != null;
        }

        public void RemoveFromObjsWithDestroy<T>() where T : T2
        {
            if (HasObj<T>() == false) return;
            PrefabHelpers.DestroyInstantiatedFromPrefab(
                _objs[typeof(T).Name].copy.GetObj());
            _objs[typeof(T).Name].copy = null;
        }

        private bool HasObj<T>(T val)
        {
            var key = val.GetType().Name;
            return _objs.ContainsKey(key) &&
                   _objs[key].copy != null;
        }

        public void RemoveFromObjsWithDestroy<T>(T val)
        {
            if (HasObj(val) == false) return;
            var key = val.GetType().Name;
            PrefabHelpers.DestroyInstantiatedFromPrefab(
                _objs[key].copy.GetObj());
            _objs[key].copy = null;
        }

        public List<GameObject> GetAliveObjs()
        {
            List<GameObject> l = new();
            foreach (var el in _objs)
            {
                if (el.Value.copy == null) continue;
                if (el.Value.copy.GetObj() == null) continue;
                l.Add(el.Value.copy.GetObj());
            }

            return l;
        }
    }
}