using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对象池
/// </summary>
public class PoolManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static PoolManager instance;
    public Transform parent;
    public List<Pool> pools;

    public Dictionary<string, Queue<GameObject>> poolDictionary;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        InstantiatePool();
    }

    public void InstantiatePool()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectQueue = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, parent);
                
                obj.SetActive(false);
                if (obj.GetComponent<audience>())
                {
                    obj.GetComponent<audience>().Tai = StageManager.Instance.Stage;

                }
                objectQueue.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectQueue);
        }
    }

    //从对象池中生成
    public GameObject SpawnFromPool(string tag, Vector3 position)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag "+tag+" doesn't exist!");
            return null;
        }
        
        GameObject objectToSpawn = null;
        for (int i = 0; i < poolDictionary[tag].Count; i++)
        {
            objectToSpawn = poolDictionary[tag].Dequeue();
            poolDictionary[tag].Enqueue(objectToSpawn);
            if (objectToSpawn.activeSelf == false)//遍历，有未激活的，就激活
            {
                objectToSpawn.SetActive(true);
                objectToSpawn.transform.position = position;
                //objectToSpawn.GetComponent<Hit>().underManager = underManager1;
                return objectToSpawn;
            }
        }
        GameObject obj = Instantiate(pools[int.Parse(tag)-1].prefab, parent);
        obj.SetActive(false);
        obj.GetComponent<audience>().Tai = StageManager.Instance.Stage;
        poolDictionary[tag].Enqueue(obj);
        obj.SetActive(true);
        obj.transform.position = position;
        
        //objectToSpawn.GetComponent<Hit>().underManager = underManager1;
        return obj;
    }
    public GameObject SpawnFromPool(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag "+tag+" doesn't exist!");
            return null;
        }
        GameObject objectToSpawn = null;
        for (int i = 0; i < poolDictionary[tag].Count; i++)
        {
            objectToSpawn = poolDictionary[tag].Dequeue();
            poolDictionary[tag].Enqueue(objectToSpawn);
            if (objectToSpawn.activeSelf == false)//遍历，有未激活的，就激活
            {
                objectToSpawn.SetActive(true);
                return objectToSpawn;
            }
        }
        GameObject obj = Instantiate(pools[int.Parse(tag)-1].prefab, parent);
        obj.SetActive(false);
        poolDictionary[tag].Enqueue(obj);
        obj.SetActive(true);
        return obj;
    }
    public GameObject SpawnFromPool(string tag, Transform Parent)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag "+tag+" doesn't exist!");
            return null;
        }
        GameObject objectToSpawn = null;
        for (int i = 0; i < poolDictionary[tag].Count; i++)
        {
            objectToSpawn = poolDictionary[tag].Dequeue();
            poolDictionary[tag].Enqueue(objectToSpawn);
            if (objectToSpawn.activeSelf == false)//遍历，有未激活的，就激活
            {
                objectToSpawn.SetActive(true);
                objectToSpawn.transform.parent = Parent;
                objectToSpawn.transform.localPosition = Vector3.zero;
                objectToSpawn.transform.localRotation = Quaternion.identity;
                return objectToSpawn;
            }
        }
        
        GameObject obj = Instantiate(pools[20].prefab, parent);
        obj.SetActive(false);
        //obj.GetComponent<audience>().Tai = StageManager.Instance.Stage;
        poolDictionary[tag].Enqueue(obj);
        obj.SetActive(true);
        objectToSpawn.transform.parent = Parent;
        objectToSpawn.transform.localPosition = Vector3.zero;
        objectToSpawn.transform.localRotation = Quaternion.identity;
        return obj;
    }
    //全部回收对象池
    public void DespawnToPool(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag" + tag + " doesn't exist!");
            return;
        }
        
        GameObject objectToSpawn = null;
        for (int i = 0; i < poolDictionary[tag].Count; i++)
        {
            objectToSpawn = poolDictionary[tag].Dequeue();
            objectToSpawn.transform.position = Vector3.zero;
            objectToSpawn.transform.parent = parent;
            poolDictionary[tag].Enqueue(objectToSpawn);
            if (objectToSpawn.activeSelf)
            {
                objectToSpawn.SetActive(false);
            }
        }
    }

    //延时回收某个对象
    public void DelayDespawn(GameObject g, float delayTime)
    {
        StartCoroutine(DelaySetActiveFalse(g, delayTime));
    }

    
    IEnumerator DelaySetActiveFalse(GameObject g, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        g.SetActive(false);
    }
}



