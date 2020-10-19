using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SatanSpot : MonoBehaviour
{
    public bool filled;
    public ParticleSystem ps;
    public bool needsTorso;
    public UnityEvent onFilled = new UnityEvent();
    public GameObject partInst;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BodyPart" && !filled)
        {
            var partDrag = collision.GetComponent<Dragable>();
            if(partDrag.beingDragged && !filled)
            {
                var sprite = collision.GetComponent<SpriteRenderer>();
                if (needsTorso && !sprite.sprite.name.Contains("torso"))
                {
                    return;
                }
                FillSpot(partDrag);
                partInst = collision.gameObject;
                onFilled.Invoke();
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }



    private void FillSpot(Dragable part)
    {
        part.transform.position = transform.position;
        part.disabled = true;
        ps.Stop();
        filled = true;
    }
}