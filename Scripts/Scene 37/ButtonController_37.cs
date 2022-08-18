using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController_37 : AnimatorObject
{
    private bool Button_1 = false;
    private bool Button_2 = false;
    private bool Button_3 = false;
    private bool Button_4 = false;
    private bool Button_5 = false;

    public void SetButton1(bool state) => animator.SetBool(nameof(Button_1), Button_1 = state);
    public void SetButton2(bool state) => animator.SetBool(nameof(Button_2), Button_2 = state);
    public void SetButton3(bool state) => animator.SetBool(nameof(Button_3), Button_3 = state);
    public void SetButton4(bool state) => animator.SetBool(nameof(Button_4), Button_4 = state);
    public void SetButton5(bool state) => animator.SetBool(nameof(Button_5), Button_5 = state);
}
