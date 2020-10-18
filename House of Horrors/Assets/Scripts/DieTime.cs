using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieTime : MonoBehaviour
{
    public float dieTime;

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
