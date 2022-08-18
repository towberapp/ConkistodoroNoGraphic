using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _Ore = null;

    [SerializeField]
    private string _ZoneName = "";

    public string Zone { get { return _ZoneName; } }
    public void SetOre(Sprite newSprite) => _Ore.sprite = newSprite;
}
