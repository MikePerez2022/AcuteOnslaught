using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private Dictionary<PoolType, Pool> pools = new();


    public void NewPool(PoolType type)
    {
        pools.TryAdd(type, new Pool());
    }

    public GameObject GetPooledObject(PoolType type, GameObject bullet, Vector3 position, Quaternion rotation)
    {
        /*
         * Checks if there is already pool of type
         * if there is it finds a usable bullet
         * if there is no usable bullet it spawns a new one
         * if there isnt a pool of type, it makes a pool of type then spawns a new bullet
         */
        if (pools.ContainsKey(type))
        {
            foreach (GameObject obj in pools[type].pool)
            {
                if (!obj.GetComponent<Bullet>().inUse)
                    return obj;
            }
            GameObject spawned = Instantiate(original: bullet, position, rotation);
            pools[type].pool.Add(spawned);
            return spawned;
        }

        NewPool(type);
        GameObject spawned2 = Instantiate(original: bullet, position, rotation);
        pools[type].pool.Add(spawned2);
        return spawned2;
    }
}

public class Pool
{
    public List<GameObject> pool = new();
}

public enum PoolType
{
    Normal,
    Sin,
    Laser
}