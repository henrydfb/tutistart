using UnityEngine;
using System.Collections;

public class StoreDataController : MonoBehaviour 
{
    public int MAX_BODIES;
    public int MAX_ROCKS;
    public int TIME;
    public int level;
    public int bodies;
    public bool firstLevel = true;
    public int coins;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
