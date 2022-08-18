using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    [SerializeField] private float _MinDeltaTime = 0;
    [SerializeField] private CharacterIniter _ValueIniter = null;
    [SerializeField] private ExtraObjectsAdder _ObjectsAdder = null;

    private const float _MaxWrongTime = 5;
    private Animator _Animator = null;
    private CharacterMovement _CharacterMove = null;
    private Vector2 _LastVelocity = Vector2.zero;
    private float _LastWrongTime;
    private float _Time = 0;
    private PlayerSide _StartSide = null;
    private TouchController _TouchController = null;
    private Coroutine _animJob;
    private Transform[] _Childs;


    void Start()
    {
        _Childs = GetComponentsInChildren<Transform>(true);
        _TouchController = FindObjectOfType<TouchController>();
        _CharacterMove = GetComponentInParent<CharacterMovement>();
        _Animator = GetComponent<Animator>();
        if (_ValueIniter.Start.AnimController)
            _Animator.runtimeAnimatorController = _ValueIniter.Start.AnimController;
        if (_ValueIniter.Start.StartSide)
            SetStartSide(_ValueIniter.Start.StartSide);
        _Time = Time.time;
    }
    private void OnEnable()
    {
        if ((_Animator?.enabled  ?? false) && _StartSide)
            StartCoroutine(PlayStartAnim());
    }
    private IEnumerator PlayStartAnim()
    {
        _Animator.Play(_StartSide.AnimationName);
        yield return null;
        _Animator.Play(_StartSide.AnimationName);
    }
    public void SetAnimatorController(RuntimeAnimatorController newController)
    {
        if (!newController)
            return ;
        _Animator.runtimeAnimatorController = newController;
        GameProgressManager.Manager.PlayerData.Animator = newController.name;
        SetStartSide(_StartSide);
    }
    public void SetStartSide(PlayerSide newSide)
    {
        _StartSide = newSide;
        _Animator.Play(newSide.AnimationName);
    }

    public void UpdateSide()
    {
        SetPlayerSide(_CharacterMove.CurrentSide);
    }

    private void OnDisable()
    {
        _LastVelocity = Vector2.zero;
        StopMoving();
    }

    public void Fear()
    {
        _Animator.SetFloat("MovementX", 0);
        _Animator.SetFloat("MovementY", 0);
        _Animator.SetBool("IsMoving", false);
        _Animator.Play(_CharacterMove.CurrentSide.SideName + "Fear");
    }

    public void WrongAction()
    {
        int animIndex = Time.time - _LastWrongTime <= _MaxWrongTime ? 2 : 1;

        _LastWrongTime = Time.time;
        _Animator.SetFloat("MovementX", 0);
        _Animator.SetFloat("MovementY", 0);
        _Animator.SetBool("IsMoving", false);
        _Animator.Play(_CharacterMove.CurrentSide.SideName + $"Wrong {animIndex}");
    }
    // Update is called once per frame
    void Update()
    {
        //Вторая проверка нужна, чтобы персонаж не дергался, когда расстояние между точками маленькое
        if (_LastVelocity != _CharacterMove.Velocity && Time.time - _Time >= _MinDeltaTime)
        {
            _Time = Time.time;
            _LastVelocity = _CharacterMove.Velocity;
            _Animator.SetFloat("MovementX", _LastVelocity.x);
            _Animator.SetFloat("MovementY", _LastVelocity.y);
        }
    }

    public void StartMoving() =>
        _Animator.SetBool("IsMoving", true);

    public void StopMoving()
    {
        _Animator?.SetBool("IsMoving", false);
        UpdateSide();
    }
    public void SetPlayerSide(PlayerSide side)
    {
        string animName = side.AnimationName;
        _Animator.Play(animName);
    }

    private void ResetChildPos()
    {
        foreach (var child in GetComponentsInChildren<Transform>(true))
            if (Array.Find(_Childs, el => el.gameObject == child.gameObject) is var startT && startT)
            {
                child.localPosition = startT.localPosition;
                child.localEulerAngles = startT.localEulerAngles;
                child.localScale = startT.localScale;
            }
    }

    private IEnumerator ItemAnimating(ItemAnimation data)
    {
        _StartSide = data.PlayerSide;
        _CharacterMove.Pause();
        StopMoving();
        _Animator.enabled = false;
        //yield return null;
        _ObjectsAdder.RemoveUnnecessary(data.NecesseryObjects);
        //_Animator.Rebind();
        for (int i = 0; i < data.ObjectsPath.Count; i++)
            _ObjectsAdder.AddExtraObjects(data.ObjectsPath[i].name, data.NecesseryObjects[i]);
        yield return null;
        _Animator.enabled = true;
        _Animator.Rebind();
        _Animator.Play(data.AnimationName);
        yield return null;
        while (_Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == data.AnimationName || _CharacterMove.Velocity != Vector2.zero)
            yield return null;
        for (int i = 0; i < data.ObjectsPath.Count; i++)
            _ObjectsAdder.RemoveExtraObjects(data.NecesseryObjects[i]);
        yield return null;
        _Animator.Rebind();
        yield return null;
        ResetChildPos();
        SetStartSide(data.PlayerSide);
        _CharacterMove.Continue();
    }
    public void SetItemAnim(ItemAnimation data)
    {
        if (!data)
            return;
        
        if (_animJob != null)
            StopCoroutine(_animJob);
        _animJob = StartCoroutine(ItemAnimating(data));
    }
}