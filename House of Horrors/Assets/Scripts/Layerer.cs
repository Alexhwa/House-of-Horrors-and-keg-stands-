using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Layerer : MonoBehaviour
{
    private Transform[] needsLayering;

    // Update is called once per frame
    void Update()
    {
        Monster[] monsters = GameObject.FindObjectsOfType<Monster>();
        needsLayering = new Transform[monsters.Length + 1];
        for (int i = 0; i < monsters.Length; i++) {
            needsLayering[i] = monsters[i].transform;
        }
        
        needsLayering[monsters.Length] = GameObject.FindObjectOfType<PlayerController>().transform;
        Comparer compare = new Comparer();
        Array.Sort(needsLayering, compare);

        print(StringFrmArray(needsLayering));
    }
    private string StringFrmArray(Transform[] array)
    {
        string result = "";
        foreach (Transform e in array) {
            result += e.name;
        }
        return result;
    }
    public class Comparer : IComparer
    {
        public int Compare(object x, object y)
        {
            return ((Transform)x).position.y < ((Transform)y).position.y ? 1 : -1;
        }
    }
}
