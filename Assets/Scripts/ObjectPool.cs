﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private int poolSize;
    [SerializeField]
    private float scale;
    private List<GameObject> pool;
    private int killedEnemies;
    private void Awake()
    {
        InitPool();
    }
    private void InitPool()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            pool.Add(Instantiate(prefab));
            pool[i].transform.localScale = new Vector3(scale, scale, 0f);
            pool[i].SetActive(false);
        }
        killedEnemies = 0;
    }

    public GameObject InstantiateFromPool(Vector2 pos, Quaternion rot)
    {
        if (pool.Count < 1f)
        {
            pool.Add(Instantiate(prefab));
            pool[pool.Count - 1].SetActive(false);
            poolSize++;
        }
        GameObject newObject = pool[pool.Count - 1];

        newObject.SetActive(true);
        newObject.transform.position = pos;
        newObject.transform.rotation = rot;

        pool.RemoveAt(pool.Count - 1);
        return newObject;
    }

    public void ReturnToPool(GameObject go)
    {
        go.SetActive(false);
        pool.Add(go);
    }

    public bool IsFull()
    {
        return pool.Count == poolSize;
    }

    public int getNumberOut()
    {
        return poolSize - pool.Count;
    }
}
