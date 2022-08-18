using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puzzle12 : AnimatorObject
{
    [SerializeField]
    private bool Telo = false, Golova = false, Korona = false;

    [SerializeField]
    private bool IsArrow_1 = true, IsArrow_2 = true, IsArrow_3 = true, IsSword = true;


    [SerializeField] private UnityEvent _arrow1, _arrow3, _arrow13, _noArrow;
    private void Start()
    {
        StartCoroutine(Checking());

        animator.SetBool(nameof(IsArrow_1), IsArrow_1);
        animator.SetBool(nameof(IsArrow_2), IsArrow_2);
        animator.SetBool(nameof(IsArrow_3), IsArrow_3);
        animator.SetBool(nameof(IsSword), IsSword);

        string direction = !Telo ? "Left" : "Right";
        animator.Play($"{nameof(Telo)} {direction}");

        direction = !Golova ? "Left" : "Right";
        animator.Play($"{nameof(Golova)} {direction}");

        direction = !Korona ? "Left" : "Right";
        animator.Play($"{nameof(Korona)} {direction}");
    }

    public void TryMoveBody()
    {
        string direction = Telo ? "Left" : "Right";
        animator.Play($"{nameof(Telo)} {direction} Start");
        if (!IsArrow_1 && !IsSword && !IsArrow_3)
            Telo = !Telo;
    }

    public void TryMoveCrown()
    {
        string direction = Korona ? "Left" : "Right";
        animator.Play($"{nameof(Korona)} {direction}");
        Korona = !Korona;
    }

    public void TryMoveHead()
    {
        string direction = Golova ? "Left" : "Right";
        animator.Play($"{nameof(Golova)} {direction} Start");
        if (!IsArrow_2)
            Golova = !Golova;
    }

    public void SwitchArrow1()
    {
        IsArrow_1 = !IsArrow_1;
        animator.SetBool(nameof(IsArrow_1), IsArrow_1);
    }
    public void SwitchArrow2()
    {
        IsArrow_2 = !IsArrow_2;
        animator.SetBool(nameof(IsArrow_2), IsArrow_2);
    }
    public void SwitchArrow3()
    {
        IsArrow_3 = !IsArrow_3;
        animator.SetBool(nameof(IsArrow_3), IsArrow_3);
    }
    public void SwitchSword()
    {
        IsSword = !IsSword;
        animator.SetBool(nameof(IsSword), IsSword);
    }

    private IEnumerator Checking()
    {
        while (!Telo || !Golova || !Korona)
            yield return null;
        if (IsArrow_1 && IsArrow_3)
            _arrow13.Invoke();
        else if (IsArrow_1 && !IsArrow_3)
            _arrow1.Invoke();
        else if (!IsArrow_1 && IsArrow_3)
            _arrow3.Invoke();
        else if (!IsArrow_1 && !IsArrow_3)
            _noArrow.Invoke();

    }
}
