using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketZone : MonoBehaviour
{
    [SerializeField]
    private string _Zone = "";

    public string ZoneName
    {
        get => _Zone;
    }
}
