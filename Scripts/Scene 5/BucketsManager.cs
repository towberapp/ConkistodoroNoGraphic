using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketsManager : MonoBehaviour
{
    public string Zone = "";

    [SerializeField]
    private List<Bucket> _Buckets = new List<Bucket>();

    public void SetBucket(string newZone)
    {
        if (Zone == newZone)
            return;
        foreach (Bucket bucket in _Buckets)
        {
            if (bucket.Zone != newZone)
                bucket.gameObject.SetActive(false);
            else
                bucket.gameObject.SetActive(true);
        }
        Zone = newZone;
    }

    public void SetOreToBuckets(Sprite oreSprite)
    {
        foreach (Bucket bucket in _Buckets)
            bucket.SetOre(oreSprite);
    }
}
