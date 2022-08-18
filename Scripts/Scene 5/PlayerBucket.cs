using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Timeline;

public class PlayerBucket : MonoBehaviour
{
    public bool _IsHandled = false;

    private string _BucketZone = null;

    [SerializeField]
    private State _BucketState = null,
                    _OreState = null;

    [SerializeField]
    private TimelineManager _TimelineManager = null;

    [SerializeField]
    private BucketsManager _BucketManager = null;


    [SerializeField]
    private List<SpriteRenderer> _Ores = new List<SpriteRenderer>();


    [SerializeField]
    private UnityEvent _AddEvent = null, 
        _RemoveEvent = null;

    [SerializeField]
    private Stove _Stove = null;

    [SerializeField]
    private int _OreNum = 0;

    [SerializeField]
    private Sprite _OreSprite1 = null,
                    _OreSprite2 = null;

    [SerializeField]
    private List<SideFollower> _Followers = new List<SideFollower>();

    [SerializeField]
    private ObjectProgressManager _ZoneRemember = null,
                                    _OreRemember = null;
    public void SetOre(int newOre)
    {
        _OreNum = newOre;
        Sprite newSprite;
        _OreState.Value = newOre.ToString();
        if (_OreNum > 0)
            newSprite = _OreNum == 1 ? _OreSprite1 : _OreSprite2;
        else
            newSprite = null;
        foreach (SpriteRenderer ore in _Ores)
            ore.sprite = newSprite;
        _OreRemember.SetState(newOre.ToString());
        _BucketManager.SetOreToBuckets(newSprite);
    }

    public void Take()
    {
        if (!_IsHandled)
        {
            SetOresActive(true);
            _BucketState.Value = "Player";
            _ZoneRemember.SetState("Player");
            _BucketManager.SetBucket("Player");
            _IsHandled = true;
            _AddEvent.Invoke();
        }
    }
    public void Remove()
    {
        if (_IsHandled)
        {
            SetOresActive(false);
            _BucketState.Value = _BucketZone;
            _ZoneRemember.SetState(_BucketZone);
            _BucketManager.SetBucket(_BucketZone);
            _RemoveEvent.Invoke();
        }
        _IsHandled = false;
    }

    private void SetOresActive(bool isActive)
    {
        foreach (SpriteRenderer ore in _Ores)
            ore.gameObject.SetActive(isActive);
    }
    public void Check(TimelineAsset removeTimeline)
    {
        if (_IsHandled)
        {
            _ZoneRemember.SetState(_BucketZone);
            _RemoveEvent.Invoke();
            _Followers.ForEach((follower) => follower.SetToTarget());
            _TimelineManager.AddForward(removeTimeline);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BucketZone>() is BucketZone zone)
            _BucketZone = zone.ZoneName;
    }

    public void ThrowOre()
    {
        _Stove.AddOre(_OreNum);
        SetOre(0);
    }
}
