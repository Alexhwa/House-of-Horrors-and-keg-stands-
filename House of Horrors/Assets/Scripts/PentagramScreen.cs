using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagramScreen : MonoBehaviour
{

    private Inventory inventory;
    private Transform spawnPos;
    public GameObject bodyPart;
    // Start is called before the first frame update
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        spawnPos = transform.GetChild(0);
        for(int i = 0; i < inventory.parts.Count; i++)
        {
            var part = Instantiate(bodyPart, spawnPos.position + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0), spawnPos.rotation);
            part.GetComponent<SpriteRenderer>().sprite = inventory.parts[i];
            Vector3 rot = part.transform.eulerAngles;
            rot.z = Random.Range(0, 360);
            part.transform.eulerAngles = rot;
        }
    }
    
}
