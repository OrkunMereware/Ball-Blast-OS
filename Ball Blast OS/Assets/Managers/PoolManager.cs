using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int maxInstance = 100;
    [System.NonSerialized] private int currentIndex = -1;
    void Awake()
    {
        for (int i = 0; i < maxInstance; i++)
        {
            GameObject prefabClone = MonoBehaviour.Instantiate(prefab);
            prefabClone.transform.parent = transform;
            prefabClone.SetActive(false);
        }    
    }

    public GameObject GetOne()
    {
        if (++currentIndex >= transform.childCount)
        {
            currentIndex = 0;
        }
        Debug.Log(currentIndex + " / " + transform.childCount);
        GameObject obj = transform.GetChild(currentIndex).gameObject;
        return obj;
    }

    public void ReturnOne(GameObject obj)
    {
        //obj.transform.parent = transform;
        obj.SetActive(false);
    }
}
