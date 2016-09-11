using UnityEngine;
using System.Collections;

public class StoreDataController : MonoBehaviour 
{
    public int bodies;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
