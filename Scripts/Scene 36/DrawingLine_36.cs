using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingLine_36 : MonoBehaviour
{
    [SerializeField] private float _dotSpamDelay = 0.1f;
    [SerializeField] private float _maxDistance = 1f;
    [SerializeField] private float _moveSpeed = 1;
    [SerializeField] private Color _color;
    private Color _prevColor;
    private LineRenderer _lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        StartCoroutine(DotSpam());
        StartCoroutine(MoveDots());
    }

    private IEnumerator DotSpam()
    {
        while (true)
        {
            _lineRenderer.positionCount++;
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, transform.position);
            yield return new WaitForSeconds(_dotSpamDelay);
        }
    }

    private void DeletePoint(int index)
    {
        Vector3[] points = new Vector3[_lineRenderer.positionCount];
        List<Vector3> newList = new List<Vector3>();

        _lineRenderer.GetPositions(points);
        for (int i = 0; i < points.Length; i++)
            if (i != index)
                newList.Add(points[i]);
        _lineRenderer.positionCount--;
        _lineRenderer.SetPositions(newList.ToArray());
    }

    private IEnumerator MoveDots()
    {
        Vector3 dotPos;

        while (true)
        {
            for (int i = 0; i < _lineRenderer.positionCount; i++)
            {
                dotPos = _lineRenderer.GetPosition(i);
                dotPos.x -= Time.deltaTime * _moveSpeed;
                _lineRenderer.SetPosition(i, dotPos);
                if (Mathf.Abs(dotPos.x - transform.position.x) > _maxDistance
                    || dotPos.x > transform.position.x)
                    DeletePoint(i);
            }
            yield return null;
        }
    }
    private void Update()
    {
        if (_prevColor != _color)
        {
            _lineRenderer.endColor = _color;
            _lineRenderer.startColor = _color;
            _prevColor = _color;
        }
    }
}
