using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bug_25 : AnimatorObject
{
    [SerializeField] private float _AttackDelay;
    [SerializeField] private UnityEvent _DieEvent;
    private float _LastAttack = 0;
    public void Die()
    {
        animator.Play("Die");
        _DieEvent.Invoke();
    }

    private void Start()
    {
        _LastAttack = 0;
    }

    public void Attack()
    {
        if (Time.time - _LastAttack < _AttackDelay)
            return;
        _LastAttack = Time.time;
        animator.Play("Attack");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BumWeapon_25>() is var enemy && enemy)
            Die();
    }
}
