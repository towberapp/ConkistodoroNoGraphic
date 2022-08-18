using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeTaker_19 : MonoBehaviour 
{
    [SerializeField] private float _AttractPower;
    private Rigidbody2D _RigidBody;
    private List<RopePart_19> _AttractObjects = new List<RopePart_19>();

    private Rigidbody2D rigid
    {
        get
        {
            if (!_RigidBody)
                _RigidBody = GetComponent<Rigidbody2D>();
            return _RigidBody;
        }
    }

    private void Update()
    {
        transform.eulerAngles = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (GetNearest() is var rope && rope != null)
            Attract(rope.rigid);
    }

    private RopePart_19 GetNearest()
    {
        var minDistance = float.MaxValue;
        int index = 0;

        if (_AttractObjects.Count == 0)
            return null;
        for (int i = 0; i < _AttractObjects.Count; i++)
        {
            if (_AttractObjects[i].IsAttractive
                && Vector2.Distance(_AttractObjects[i].transform.position, transform.position) is var distance && distance < minDistance)
            {
                minDistance = distance;
                index = 0;
            }
        }
        return _AttractObjects[index];
    }
    private void Attract(Rigidbody2D attractObj)
    {
        var direction = rigid.position - attractObj.position;
        var distance = direction.magnitude;

        var forceMagnittude = (rigid.mass * attractObj.mass) * distance * _AttractPower;
        var force = direction.normalized * forceMagnittude;

        attractObj.AddForce(force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<RopePart_19>() is var rope && rope)
            _AttractObjects.Add(rope);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<RopePart_19>() is var rope && rope)
            _AttractObjects.Remove(rope);
    }
}
