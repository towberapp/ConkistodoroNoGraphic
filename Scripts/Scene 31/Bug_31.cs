using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Bug_31 : MonoBehaviour
{
    private bool IsWatching = false;

    private float Rotating = 0;

    private BoxCollider2D _Collider = null;

    [SerializeField]
    private Animator _Anim = null;
    // Update is called once per frame
    private void Start()
    {
        _Collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
            _Anim.SetBool(nameof(IsWatching), true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
            _Anim.SetBool(nameof(IsWatching), false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            Rotating = (collision.transform.position.x - (_Collider.transform.position.x - _Collider.offset.x - _Collider.size.x / 2)) / _Collider.size.x;
            _Anim.SetFloat(nameof(Rotating), Rotating);
        }
    }
}
