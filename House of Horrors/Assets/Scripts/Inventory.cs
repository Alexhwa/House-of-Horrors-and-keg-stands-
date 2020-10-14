using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    static Inventory instance; 

    public List<Sprite> parts;
    private void Awake()
    {
        if(instance == null)
        {
            instance = GameObject.FindObjectOfType<Inventory>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void AddPart(Sprite part)
    {
        parts.Add(part);
    }
}
