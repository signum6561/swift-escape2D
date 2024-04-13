using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolObjectInfo
{
    public ObjectType type;
    public int amount = 0;
    public GameObject prefab;
    public List<GameObject> pool = new();
}
public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }
    [SerializeField] private List<PoolObjectInfo> listOfObjectPool;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        foreach (PoolObjectInfo info in listOfObjectPool)
        {
            FillPool(info);
        }
    }
    private void FillPool(PoolObjectInfo info)
    {
        for (int i = 0; i < info.amount; i++)
        {
            GameObject obj;
            obj = Instantiate(info.prefab);
            obj.SetActive(false);
            info.pool.Add(obj);
        }
    }
    public GameObject GetObject(ObjectType type)
    {
        PoolObjectInfo selected = GetObjectByType(type);
        List<GameObject> pool = selected.pool;
        GameObject obj;
        if (pool.Count > 0)
        {
            obj = pool[^1];
            pool.Remove(obj);
        }
        else
        {
            obj = Instantiate(selected.prefab);
            obj.SetActive(false);
        }
        return obj;
    }
    public void CoolObject(GameObject obj, ObjectType type)
    {
        obj.SetActive(false);
        PoolObjectInfo selected = GetObjectByType(type);
        List<GameObject> pool = selected.pool;
        if (!pool.Contains(obj))
            pool.Add(obj);
    }
    private PoolObjectInfo GetObjectByType(ObjectType type)
    {
        return listOfObjectPool.Find(e => e.type == type);
    }
}

