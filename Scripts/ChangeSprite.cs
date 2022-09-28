using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{    
    public void Change (Sprite sprite)
    {
        SpriteRenderer sr =  GetComponent<SpriteRenderer>();
        sr.sprite = sprite;
    }
}
