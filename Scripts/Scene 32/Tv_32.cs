using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tv_32 : MonoBehaviour
{
    [SerializeField]
    private State _Tv = null;

    [SerializeField]
    private float _MinWait = 0, _MaxWait = 20;
    private Coroutine _Monitoring;
    // Start is called before the first frame update
    void Start()
    {
        StartMonitor();
    }

    private IEnumerator WaitForLags()
    {
        while (true)
        {
            float waitTime = Random.Range(_MinWait, _MaxWait);
            yield return new WaitForSeconds(waitTime);
            SetLags();
            while (_Tv.Value != "Fixed")
                yield return null;
        }
    }

    public void StopMonitor()
    {
        if (_Monitoring != null)
            StopCoroutine(_Monitoring);
    }
    public void StartMonitor()
    {
        if (_Monitoring == null)
            _Monitoring = StartCoroutine(WaitForLags());
    }

    public void SetLags() => _Tv.Value = "Lagging";
    public void SetTouched() => _Tv.Value = "Touched";
    public void SetUntouched() => _Tv.Value = "Untouched";
    public void FixLags() => _Tv.Value = "Fixed";
}
