using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mech_32 : AnimatorObject
{
    [SerializeField]
    private GameObject _Drink = null;

    [SerializeField]
    private GameObject _Container = null;
    private int Gold = 0;
    private bool Gas = false;

    public void SetGas(bool value) => animator.SetBool(nameof(Gas), Gas = value); 
    public void SetGold() => animator.SetInteger(nameof(Gold), Gold = 1);
    public void WarnGold() => animator.SetInteger(nameof(Gold), Gold = 2);
    public void SuckGold() => animator.SetInteger(nameof(Gold), Gold = 3);
    public void AddContainer() =>_Container.SetActive(true);

    public void SetDrink(bool state) => _Drink.SetActive(state);
}
