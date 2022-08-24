using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HeadRotating : MonoBehaviour
{
    private Animator _Animator = null;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip clipStart;
    [SerializeField] private AudioClip clipMove;
    [SerializeField] private AudioClip clipEnd;

    [SerializeField]
    private float _Rotation = 0;

    [SerializeField]
    private float _Speed = 1;

    [SerializeField]
    private float _TimeBetweenDust = 3;

    [SerializeField]
    private bool _IsDust = false;

    [SerializeField]
    private Shooter _Shooter = null;

    private bool _IsPlayerInZone = false;

    private Coroutine _Rotating = null, _DustChecker = null;

    private bool isRotation = false;
    private bool isStartMove = false;
    private bool isCheckStop = false;
    private IEnumerator coroutine;

    private float Rotation
    {
        get
        {
            return _Rotation;
        }
        set
        {
            if (value >= 0)
            {
                _Rotation = value >= 1 ? 0.99f : value;
            } 
            else
            {
                _Rotation = 0;
            }
            _Animator.SetFloat("Rotation", _Rotation);
            //print("Rotation");
        }
    }
    private Vector3 _PrevPlayerPos;
    

    [SerializeField]
    private bool _IsMoveToStart = false;
    private bool IsDust
    {
        get
        {
            return _IsDust;
        }
        set
        {
            _IsDust = value;
            _Animator.SetBool("IsDust", _IsDust);
        }
    }
    private BoxCollider2D _Collider;
    private Vector3 _ColliderOffset;
    private Vector3 _ColliderSize;
    void Start()
    {
        _Animator = GetComponent<Animator>();
        _Collider = GetComponent<BoxCollider2D>();
        _ColliderOffset = _Collider.offset;
        _ColliderSize = _Collider.size;
        coroutine = CheckStop();
    }

    private float CalcRotation(Vector3 playerPos)
    {
        return (playerPos.x - (transform.position + _ColliderOffset - _ColliderSize / 2).x) / _ColliderSize.x;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            if (_IsMoveToStart)
            {
                MoveTo(0);
                _PrevPlayerPos = Vector2.zero;
            }
            _IsPlayerInZone = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Player") && _Shooter.IsStopped)
        {
            Transform player = collision.transform;
            if (_PrevPlayerPos != player.position)
            {
                float newRot = CalcRotation(player.position);
                MoveTo(newRot);
                _PrevPlayerPos = player.position;                
            }            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        { 
            _IsPlayerInZone = true;
            StartDusting();
        }
    }

    private void StartDusting()
    {
        IEnumerator DustCheck()
        {
            float lastDust = 0;
            float lastRot = Rotation;
            while (_IsPlayerInZone)
            {
                if (Time.time - lastDust >= _TimeBetweenDust && lastRot != Rotation)
                {
                    IsDust = true;
                    yield return new WaitForSeconds(0.5f);
                    IsDust = false;
                    lastDust = Time.time;
                }
                lastRot = Rotation;
                yield return null;
            }
            _DustChecker = null;
        }

        if (_DustChecker == null)
            _DustChecker = StartCoroutine(DustCheck());
    }
    public void MoveTo(float newRotation)
    {


        IEnumerator Moving(float targetRot)
        {

            /* if (!isRotation)
             {
                 print("START ROTATION");
                 audioSource.loop = false;
                 audioSource.clip = clipStart;
                 audioSource.Play();
             }*/

            if (!isStartMove)
            //if (isRotation && !isStartMove && !audioSource.isPlaying)
            {
                print("move");
                isStartMove = true;
                audioSource.loop = true;
                audioSource.clip = clipMove;              
                audioSource.Play();
            }

            isRotation = true;
            StopCoroutine(coroutine);
            


            //isRotation = true;

            if (targetRot > 0.99f)
                targetRot = 0.99f;
            else if (targetRot < 0)
                targetRot = 0;
            float multipl = targetRot >= Rotation ? 1 : -1;
            while (Rotation != targetRot )
            {
                Rotation += Mathf.Abs(targetRot - Rotation) > Time.deltaTime * _Speed ? Time.deltaTime * _Speed * multipl : targetRot - Rotation;
                yield return null;
            }

            coroutine = CheckStop();
            StartCoroutine(coroutine);
        }


        if (_Rotating != null)
        {
            StopCoroutine(_Rotating);         
        }

        _Rotating = StartCoroutine(Moving(newRotation));
    }


    IEnumerator CheckStop()
    {        
        yield return new WaitForSeconds(0.2f);
        print("STOP ROTATION");
        isStartMove = false;
        isRotation = false;

        audioSource.loop = false;
        audioSource.clip = clipEnd;
        audioSource.Play();

    }

}
