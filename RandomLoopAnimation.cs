using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLoopAnimation : MonoBehaviour
{
    [SerializeField] private string animclipString;

    [Header("Первоначальная задержка")]
    [SerializeField] private float delayBeforeStart = 0f;

    [Header("Минимальный диапазон")]
    [SerializeField] private float fromRange;

    [Header("Максимальный диапазон")]
    [SerializeField] private float toRange;

    [Header("Количество повторений (0 - бесконечно)")]
    [SerializeField] private int loopCount = 0;

    private Animator animator;
    private int count = 0;
    private IEnumerator courutine;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        courutine = LoopCourutines();
    }

    private void Start()
    {
        if (animator != null)
             StartCoroutine(courutine);
    }


    IEnumerator LoopCourutines()
    {
        yield return new WaitForSeconds(delayBeforeStart);

        while ((count < loopCount && loopCount != 0) || loopCount == 0)
        {

            yield return new WaitForSeconds(Random.Range(fromRange, toRange));
            animator.Play(animclipString);            
            yield return null;
           

            float length = animator.GetCurrentAnimatorStateInfo(0).length;
            string name = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

            Debug.Log("length: " + length);
            Debug.Log("name: " + name);

            if (length == 1) StopCoroutine(courutine);                        
            yield return new WaitForSeconds(length);

            count++;
        }           
    }
}
