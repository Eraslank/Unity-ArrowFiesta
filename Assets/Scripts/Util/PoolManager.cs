using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PoolManager<T> where T : Component, IPoolable
{
    public HashSet<T> pool;

    private GameObject prefab;

    private PoolManager() { }

    public PoolManager(GameObject prefab)
    {
        pool = new HashSet<T>();
        this.prefab = prefab;
    }


    public T Get()
    {
        var obj = pool.FirstOrDefault(p => p.IsAvailable);
        if (obj != null)
            return obj;
        obj = GameObject.Instantiate(prefab).AddComponent<T>();
        pool.Add(obj);
        return obj;
    }

}
