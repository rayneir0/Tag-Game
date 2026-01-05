using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagManager : MonoBehaviour
{
    public static TagManager Instance;
    [HideInInspector] public GameObject currentIt;


    // Manages who is it and who is not
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

    }

    public void SetIt(GameObject newIt)
    {
        currentIt = newIt;
        Debug.Log(newIt.name + " is now IT!");
        
    }

    public bool IsIt(GameObject obj)
    {
        return currentIt == obj;
    }
}
