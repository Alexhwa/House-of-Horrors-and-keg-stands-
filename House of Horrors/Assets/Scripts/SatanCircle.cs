using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanCircle : MonoBehaviour
{
    public SatanSpot[] spots;
    private Inventory inventory;

    public GameObject amalgam;
    // Start is called before the first frame update
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        foreach(SatanSpot e in spots)
        {
            e.onFilled.AddListener(TryMakeAmalgam);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void TryMakeAmalgam()
    {
        foreach (SatanSpot e in spots)
        {
            if (!e.filled)
            {
                return;
            }
        }
        var amalgInst = Instantiate(amalgam, transform.position, transform.rotation).GetComponent<Amalgam>();
        List<Sprite> parts = new List<Sprite>();
        foreach (SatanSpot e in spots)
        {
            var curPart = e.partInst.GetComponent<SpriteRenderer>();
            inventory.parts.Remove(curPart.sprite);
            parts.Add(curPart.sprite);
            Destroy(e.partInst);
        }
        amalgInst.SetBodyParts(parts.ToArray());
    }
}
