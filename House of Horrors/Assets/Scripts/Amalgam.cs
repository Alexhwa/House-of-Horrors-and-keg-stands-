using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amalgam : MonoBehaviour
{
    public SpriteRenderer[] bodyParts;

    public void SetBodyParts(Sprite[] sprites)
    {
        for(int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].sprite = sprites[i];
        }
    }
    
}
