using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePart_19 : MonoBehaviour
{
    [SerializeField] private  bool _isAttractive = false;
    private Rigidbody2D _rb;

    public bool IsAttractive
    {
        get => _isAttractive;
        set
        {
            if (_isAttractive == value)
                return;
            _isAttractive = value;
            rigid.simulated = value;
        }
    }

    public Rigidbody2D rigid
    {
        get
        {
            if (!_rb)
                _rb = GetComponent<Rigidbody2D>();
            return _rb;
        }
    }

    private IEnumerator FromStart()
    {
        yield return new WaitForSeconds(2);
        rigid.simulated = IsAttractive;
    }

    private void Start()
    {
        StartCoroutine(FromStart());
    }
}
