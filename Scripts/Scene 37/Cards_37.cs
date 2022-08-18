using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards_37 : AnimatorObject
{
    [SerializeField] private Vector3 _StartPos;
    [SerializeField] private ItemAnimation _idleAnim;
    [SerializeField] private ItemAnimation _finishAnim;
    [Header("Tear Settings")]
    [SerializeField] private ItemAnimation _tearAnim;
    [SerializeField] private Item _oldCards;
    [SerializeField] private Item _tearedCards;
    [SerializeField] private Item _extraItem;
    private PlayerInventory _playerInventory;
    private AnimationController _playerAnim;
    private int State = 0;
    public void NextState() => animator.SetInteger(nameof(State), State = State == 5 ? 0 : State + 1);

    public void Close()
    {
        StopAllCoroutines();
        _playerAnim.SetItemAnim(_finishAnim);
    }

    private IEnumerator FromStart()
    {
        yield return new WaitForSeconds(2);
        _playerAnim.SetItemAnim(_idleAnim);
    }

    public void TearCard()
    {
        _playerAnim.SetItemAnim(_tearAnim);
        _playerInventory.AddItem(_tearedCards);
        _playerInventory.AddItem(_extraItem);
        _playerInventory.RemoveItem(_oldCards);
    }

    private void Start()
    {
        State = animator.GetInteger(nameof(State));
        _playerInventory = FindObjectOfType<PlayerInventory>();
        _playerAnim = FindObjectOfType<AnimationController>();
        transform.parent = Camera.allCameras[0].transform;
        transform.localPosition = _StartPos;
        transform.localEulerAngles = Vector3.zero;
        StartCoroutine(FromStart());
    }

}
