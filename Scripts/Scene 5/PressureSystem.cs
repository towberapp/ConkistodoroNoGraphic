using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureSystem : AnimatorObject
{
    [SerializeField]
    private State _PressureState = null;

    [Header("Nozzle Settings")]
    [SerializeField]
    private Item _CorrectNozzle = null;

    [SerializeField]
    private Item _CurNozzle = null;

    [SerializeField]
    private ObjectProgressManager _NozzleSaver = null;

    [SerializeField]
    private SpriteRenderer _NozzleImage = null;

    [Header("Door Settings")]
    [SerializeField]
    private GameObject _Door = null;

    [SerializeField]
    private State _DoorState = null;

    [Header("Button")]
    [SerializeField]
    private State _ButtonState = null;

    [SerializeField]
    private ObjectProgressManager _ButtonSaver = null;

    [Header("Player Settings")]
    [SerializeField]
    private PlayerInventory _PlayerInventory = null;

    [Header("Pressure")]
    [SerializeField]
    private Animator _TubeAnimator = null;


    public void SetButtonRight()
    {
        _TubeAnimator.SetBool("IsIn", true);
        animator.SetBool("IsIn", true);
        _ButtonState.Value = "Horizontal";
        _ButtonSaver.SetState(_ButtonState.Value);
        animator.Play("Button Turn Right");
    }

    public void SetButtonLeft()
    {
        _TubeAnimator.SetBool("IsIn", false);
        animator.SetBool("IsIn", false);
        _ButtonState.Value = "Vertical";
        _ButtonSaver.SetState(_ButtonState.Value);
        animator.Play("Button Turn Back");
    }

    public void DisableDoors() => _Door.SetActive(false);
    public void EnableDoors() => _Door.SetActive(true);

    public void OpenDoors()
    {
        _DoorState.Value = "Opened";
        animator.Play("Open Door");
    }

    public void CloseDoors()
    {
        _DoorState.Value = "Closed";
        animator.Play("Close Door");
    }

    public void SetStove(bool isStoveActive)
    {
        _TubeAnimator.SetBool("IsStoveFire", isStoveActive);
        animator.SetBool("IsStoveFire", isStoveActive);
    }
    public void SetNozzle(Item item)
    {
        _PressureState.Value = item == _CorrectNozzle ? "Working" : "Broken";
        _TubeAnimator.SetBool("IsBroken", item != _CorrectNozzle);
        animator.SetBool("IsBroken", item != _CorrectNozzle);
        animator.SetBool("IsNozzleChanged", true);
        _NozzleImage.sprite = item.Sprite;
        _CurNozzle = item;
        _NozzleSaver.SetState(item == _CorrectNozzle ? "Working" : "Broken");
    }
    public void ChangeNozzle(Item item)
    {
        _PlayerInventory.AddItem(_CurNozzle);
        _PlayerInventory.RemoveItem(item);
        SetNozzle(item);
    }

    public void StopSwitchingNozzle()
    {
        animator.SetBool("IsNozzleChanged", false);
    }
}
