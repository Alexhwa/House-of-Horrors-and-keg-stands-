using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float dieTime;
    public LayerMask targets;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Monster")
        {
            collision.GetComponent<Monster>().Die();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Die(dieTime));
    }
    IEnumerator Die(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
