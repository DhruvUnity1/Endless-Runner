using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject firstEmptyTile;
    [SerializeField] private Transform parent;
    [SerializeField] private int totalObjects;
    [SerializeField] private Vector3 firstObjectPos;
    [SerializeField] private List<GameObject> pooledGameobjects;
    [SerializeField] private float zDiffer;
    [SerializeField] private float lastValue;

    private void OnEnable()
    {
        GameManager.OnStartGame += onStartGame;
        GameManager.OnEndGame += onEndGame;
    }
    private void OnDisable()
    {
        GameManager.OnStartGame -= onStartGame;
        GameManager.OnEndGame -= onEndGame;
    }
    void Start()
    {
        // Initialize boundary
        pooledGameobjects = new List<GameObject>();
    }

    GameObject element;
    public void FirstSpawn(int num)
    {
       /* element = Instantiate(firstEmptyTile, firstObjectPos, Quaternion.identity);
        element.transform.parent = parent.transform;
        
        firstObjectPos.z += 10;*/
        for (int i = 0; i < num; i++)
        {

            element = Instantiate(prefab, firstObjectPos, Quaternion.identity);
            element.transform.parent = parent.transform;
            // element.SetActive(false);
            pooledGameobjects.Add(element);
            firstObjectPos.z += zDiffer;
        }
    }
    public GameObject Getpool()
    {
        foreach (GameObject obj in pooledGameobjects)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }

        }
        return null;
    }
    public GameObject GetLastElement()
    {
        //this will send the element from last order
        for (int i = pooledGameobjects.Count - 1; i >= 0; i--)
        {
            if (pooledGameobjects[i].activeInHierarchy)
            {
                return pooledGameobjects[i];
            }
        }

        return null;
    }
    public void DoPool()
    {
        GameObject element = Getpool();

        if (element != null)
        {

            element.SetActive(true);
            element.transform.position = firstObjectPos;
            firstObjectPos.z += zDiffer;

        }
    }

    public void RemoveLastElement()
    {
        GameObject element = GetLastElement();

        if (element != null)
        {
            element.SetActive(false);
        }
    }


    void onStartGame()
    {
        zDiffer = 30;
        firstObjectPos = new Vector3(0.5f, 0 , 9);
        pooledGameobjects.Clear();
        FirstSpawn(totalObjects);
    }
    void onEndGame()
    {
        
        if (parent.childCount > 0)
        {
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }
        }
    }
    
}
