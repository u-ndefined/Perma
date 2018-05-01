﻿using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ObjectsPooler Description
/// </summary>
public class ObjectsPooler : ISingleton<ObjectsPooler>
{
    protected ObjectsPooler() { } // guarantee this will be always a singleton only - can't use the constructor!

    #region Attributes
    [System.Serializable]
    public class Pool
    {
        public GameData.Prefabs tag;
        public GameObject prefab;
        public int size;
        public bool shouldExpand = false;
    }

    public List<Pool> pools;

    private Dictionary<GameData.Prefabs, List<GameObject>> poolDictionary;

    #endregion

    #region Initialization

    private void Awake()
    {
        Debug.Log("init pool");
        InitPool();
    }

	private void Start()
	{
        //DesactiveAll();
	}

	/// <summary>
	/// initialise la pool
	/// </summary>
	private void InitPool()
    {
        poolDictionary = new Dictionary<GameData.Prefabs, List<GameObject>>();

        for (int j = 0; j < pools.Count; j++)
        {
            List<GameObject> objectPool = new List<GameObject>();
            for (int i = 0; i < pools[j].size; i++)
            {
                GameObject obj = Instantiate(pools[j].prefab, transform);
                obj.SetActive(false);
                objectPool.Add(obj);
            }
            poolDictionary.Add(pools[j].tag, objectPool);
        }

    }

    private void DesactiveAll()
    {
        foreach (KeyValuePair<GameData.Prefabs, List<GameObject>> attachStat in poolDictionary)
        {
            List<GameObject> objFromTag = attachStat.Value;
            for (int j = 0; j < objFromTag.Count; j++)
            {
                //l'objet en question
                GameObject obj = objFromTag[j];
                if (!obj)
                    continue;

                obj.SetActive(false);
            }
        }
    }

    #endregion

    #region Core
    /// <summary>
    /// ici désactive tout les éléments de la pool qui sont actuellement activé...
    /// Appeler une fonction spécial ??
    /// </summary>
    public void DesactiveEveryOneForTransition()
    {
        foreach (KeyValuePair<GameData.Prefabs, List<GameObject>> attachStat in poolDictionary)
        {
            List<GameObject> objFromTag = attachStat.Value;
            for (int j = 0; j < objFromTag.Count; j++)
            {
                //l'objet en question
                GameObject obj = objFromTag[j];
                if (!obj)
                    continue;

                //est-ce que l'objet est dans la pool ? (le transform), Si non, le mettre
                GoBackToPool(obj);
            }
        }
    }

    /// <summary>
    /// access object from pool
    /// </summary>
    public GameObject SpawnFromPool(GameData.Prefabs tag, Vector3 position, Quaternion rotation, Transform parent)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("pool with tag: " + tag + "doesn't exist");
            return (null);
        }

        List<GameObject> objFromTag = poolDictionary[tag];

        for (int i = 0; i < objFromTag.Count; i++)
        {
            if (objFromTag[i] && !objFromTag[i].activeSelf)
            {
                //ici on récupère un objet de la pool !

                objFromTag[i].SetActive(true);
                objFromTag[i].transform.position = position;
                objFromTag[i].transform.rotation = rotation;
                objFromTag[i].transform.SetParent(parent);

                IPooledObject pooledObject = objFromTag[i].GetComponent<IPooledObject>();

                if (pooledObject != null)
                {
                    pooledObject.OnObjectSpawn();
                }

                return (objFromTag[i]);
            }
        }

        Debug.Log("ici on a raté ! tout les objets de la pools sont complet !!");
        for (int i = 0; i < pools.Count; i++)
        {
            if (pools[i].tag == tag)
            {
                if (pools[i].shouldExpand)
                {
                    GameObject obj = Instantiate(pools[i].prefab, transform);
                    //obj.SetActive(false);
                    objFromTag.Add(obj);


                    obj.SetActive(true);
                    obj.transform.position = position;
                    obj.transform.rotation = rotation;
                    obj.transform.SetParent(parent);

                    IPooledObject pooledObject = obj.GetComponent<IPooledObject>();

                    if (pooledObject != null)
                    {
                        pooledObject.OnObjectSpawn();
                    }

                    return (obj);


                }
                else
                {
                    Debug.LogError("pas d'expantion, error pour le tag: " + tag);

                    break;
                }
            }
        }


        return (null);
    }

    public void GoBackToPool(GameObject obj)
    {
        if (obj.transform.parent.GetInstanceID() != transform.GetInstanceID())
        {
            Debug.Log("ici set le transform parent de: " + obj.name);
            obj.transform.SetParent(transform);
        }
        if (obj.activeSelf)
        {
            IPooledObject pooledObject = obj.GetComponent<IPooledObject>();

            if (pooledObject != null)
            {
                pooledObject.OnGoBackToPool();
            }

                obj.SetActive(false);

        }
    }
    #endregion
}