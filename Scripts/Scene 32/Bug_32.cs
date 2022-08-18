using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug_32 : AnimatorObject
{
    [SerializeField] private State _PortalState = null;
    [SerializeField] private float _MinWait = 0, _MaxWait = 20;
    [SerializeField] private GameObject _Cup;
    private bool IsPortal = false,
                _IsPortalAccessable = true;


    public void DisablePortalAccess() => _IsPortalAccessable = false;
    private void Start()
    {
        StartCoroutine(PortalChecker());
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Cup" && !_Cup.activeInHierarchy)
            _Cup.SetActive(true);
    }

    public void DisableCup() => _Cup.SetActive(false);
    public void EnableCup() => _Cup.SetActive(true);

    public void CheckCup()
    {
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Cup" && _Cup.activeInHierarchy)
            _Cup.SetActive(false);
        else if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Cup" && !_Cup.activeInHierarchy)
            _Cup.SetActive(true);
    }

    public void ExitPortal()
    {
        _PortalState.Value = "Off";
        animator.SetBool(nameof(IsPortal), false);
        IsPortal = false;
    }

    public void EnterPortal() => _PortalState.Value = "On";

    private IEnumerator PortalChecker()
    {
        while (_IsPortalAccessable)
        {
            float waitTime = Random.Range(_MinWait, _MaxWait);
            yield return new WaitForSeconds(waitTime);
            if (!_IsPortalAccessable)
                break;
            animator.SetBool(nameof(IsPortal), true);
            while (IsPortal)
                yield return null;
        }
    }
}
