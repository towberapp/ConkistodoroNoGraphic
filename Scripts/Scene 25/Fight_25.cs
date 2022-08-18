using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_25 : MonoBehaviour
{
    [SerializeField] private State _BumState;
    [SerializeField] private State _PersState;

    [SerializeField] private Bum_25 _Bum;
    [SerializeField] private Pers_25 _Pers;

    [SerializeField] private float _restTimeMin = 0;
    [SerializeField] private float _restTimeMax = 1;

    [SerializeField] private float _attackTimeMin = 0;
    [SerializeField] private float _attackTimeMax = 0;

    private bool _isPaused = false;

    public void Pause() => _isPaused = true;
    public void UnPause() => _isPaused = false;

    private IEnumerator Monitoring()
    {
        float waitTime;
        yield return null;
        while (true)
        {
            if (_BumState.Value == "Fight" && _PersState.Value == "Fight")
            {
                _Bum.AttackPers();
                _Pers.Attack();
            }
            else
                yield break;
            waitTime = Random.Range(_attackTimeMin, _attackTimeMax);
            yield return new WaitForSeconds(waitTime);
            if (_BumState.Value == "Fight" && _PersState.Value == "Fight")
            {
                _Bum.PauseAttack();
                _Pers.StopAttack();
            }
            else
                yield break;
            waitTime = Random.Range(_restTimeMin, _restTimeMax);
            yield return new WaitForSeconds(waitTime);
            while (_isPaused)
                yield return null;
        }
    }

    private void Start()
    {
        StartCoroutine(Monitoring());
    }
}
