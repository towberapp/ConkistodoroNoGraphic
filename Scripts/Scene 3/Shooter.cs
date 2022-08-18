using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(Collider2D))]
public class Shooter : MonoBehaviour
{
    [SerializeField]
    private List<PlayableDirector> _Shooters = new List<PlayableDirector>();

    [SerializeField]
    List<PlayableDirector> _ShootOrder = new List<PlayableDirector>();

    [SerializeField]
    private float _MinWait = 0, _MaxWait = 1;

    [SerializeField]
    private float _LastShootWait = 0;

    [SerializeField]
    private bool _IsShooting = false;

    public bool IsStopped
    {
        get
        {
            foreach (PlayableDirector shooter in _Shooters)
                if (shooter.state == PlayState.Playing)
                    return false;
            return true;
        }
    }
    void Start()
    {
        _ShootOrder = new List<PlayableDirector>(_Shooters);
    }

    private void GenerateOrder()
    {
        List<PlayableDirector> notAddedShooters = new List<PlayableDirector>(_Shooters);
        for (int i = _ShootOrder.Count; i < _Shooters.Count; i++)
        {
            PlayableDirector randDirector = notAddedShooters[Random.Range(0, notAddedShooters.Count)];
            _ShootOrder.Add(randDirector);
            notAddedShooters.Remove(randDirector);
        }
    }
    private PlayableDirector GetShooter()
    {
        if (_ShootOrder.Count == 0)
            GenerateOrder();
        PlayableDirector shooter = _ShootOrder[0];
        _ShootOrder.RemoveAt(0);
        return shooter;
    }
    private void Shoot(PlayableDirector shooter)
    {
        shooter.time = 0;
        shooter.Play();
    }
    private IEnumerator Shooting()
    {
        while (_IsShooting)
        {
            Shoot(GetShooter());
            yield return new WaitForSeconds(Random.Range(_MinWait, _MaxWait));
        }
    }

    private IEnumerator LastShoot()
    {
        yield return new WaitForSeconds(_LastShootWait);
        Shoot(GetShooter());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player") && !_IsShooting)
        {
            StopAllCoroutines();
            _IsShooting = true;
            StartCoroutine(Shooting());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            StartCoroutine(LastShoot());
            _IsShooting = false;
        }
    }
}
