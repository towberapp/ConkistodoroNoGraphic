using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLoopTrigger : MonoBehaviour
{
    [SerializeField] private string triggerName;

    [Header("�������������� ��������")]
    [SerializeField] private float delayBeforeStart = 0f;

    [Header("����������� ��������")]
    [SerializeField] private float fromRange;

    [Header("������������ ��������")]
    [SerializeField] private float toRange;

    [Header("���������� ���������� (0 - ����������)")]
    [SerializeField] private int loopCount = 0;

    private Animator animator;
    private int count = 0;
    private IEnumerator courutine;

    private void Awake()
    {
        Debug.Log("???");

        animator = GetComponent<Animator>();
        courutine = LoopCourutines();
    }


    private void OnEnable()
    {
        Debug.Log("??? ???");

        if (animator != null)
            StartCoroutine(courutine);
    }

/*    private void Enable()
    {
        Debug.Log("??? ???");

        if (animator != null)
            StartCoroutine(courutine);
    }
*/

    IEnumerator LoopCourutines()
    {
        yield return new WaitForSeconds(delayBeforeStart);

        while ((count < loopCount && loopCount != 0) || loopCount == 0)
        {
            float rnd = Random.Range(fromRange, toRange);
            yield return new WaitForSeconds(rnd);
            animator.SetTrigger(triggerName);           
            count++;
        }
    }
}
