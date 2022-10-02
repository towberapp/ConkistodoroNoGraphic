using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class InventoryState : MonoBehaviour
{
    [SerializeField]
    [Range(0,1)]
    private float _DistanceToAppear = 0.125f;

    [SerializeField] private Animator rightPanel;

    private Animator _Animator = null;
    private Coroutine _Monitoring = null;
    public bool IsActive { get; private set; } = false;
    void Start()
    {
        _Animator = GetComponent<Animator>();
        StartMonitoring();
    }

    private IEnumerator Monitoring()
    {
        while (true)
        {
            bool isShowingItems = transform.parent.GetComponentsInChildren<NewItemShower>().Length > 1;
            if (IsActive && 1 - Input.mousePosition.y / Screen.height > _DistanceToAppear && !isShowingItems)
            {
                _Animator.Play("Disappear");                
                rightPanel?.Play("Disappear");


                IsActive = false;
            }
            else if (!IsActive && 1 - Input.mousePosition.y / Screen.height < _DistanceToAppear || isShowingItems)
            {
                _Animator.Play("Appear");
                rightPanel?.Play("Appear");
                IsActive = true;
            }
            yield return null;
        }
    }
    public void StopMonitoring()
    {
        if (_Monitoring != null)
            StopCoroutine(_Monitoring);
        _Monitoring = null;
    }

    public void Show()
    {
        StopMonitoring();
        if (!IsActive)
            _Animator.Play("FromAppear");
        else
            _Animator.Play("FromHolding");
        IsActive = false;
    }
    public void StartMonitoring()
    {
        if (_Monitoring != null)
            StopCoroutine(_Monitoring);
        _Monitoring = StartCoroutine(Monitoring());
    }    
}
