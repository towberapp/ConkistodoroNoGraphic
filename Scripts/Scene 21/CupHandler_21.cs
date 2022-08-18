using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum CupHandlerState
{
    Start,
    Rotating,
    Elongation,
    RotatingBack,
    ElongationBack
}

public class CupHandler_21 : AnimatorObject
{
    [SerializeField] private Cup_21 _Cup = null;
    [SerializeField] private SpriteRenderer _Stick = null;
    [SerializeField] private SpriteRenderer _Circle = null;
    [SerializeField] Transform _CupStartPoint = null;
    [SerializeField] State _CupPosState;
    [SerializeField] private float _clickDuration = 3;
    [SerializeField] private int _MaxSticks = 5;
    [SerializeField] private float _ElongationSpeed = 1;
    [SerializeField] private float _RotationSpeed = 30;
    [SerializeField] private float _MaxRotDelta = 45;
    [SerializeField] private string state;

    private bool _isPaused;
    private float _lastClick;
    private List<SpriteRenderer> _Circles = new List<SpriteRenderer>();
    private List<SpriteRenderer> _Sticks = new List<SpriteRenderer>();
    private bool _IsCupHandled = false;
    private CupHandlerState State = CupHandlerState.Start;
    private float _MaxDistance;
    private Coroutine _Action;

    private void Update()
    {
        state = State.ToString();
    }

    private void Start()
    {
        _lastClick = Time.time;
        var texture = _Stick.sprite.texture;
        _MaxDistance = texture.height / _Stick.sprite.pixelsPerUnit - 0.2f;
    }

    public void SpawnGoldenCup()
    {
        var newCup = Instantiate<Cup_21>(_Cup, null);
        newCup.CupPosState = _CupPosState;
        newCup.transform.position = _CupStartPoint.position;
        newCup.transform.eulerAngles = _CupStartPoint.eulerAngles;
    }

    public void Pause() => _isPaused = true;
    public void Continue() => _isPaused = false;

    public void TakeCup()
    {
        _CupPosState.Value = "Taken";
        _IsCupHandled = true;
        StopElongation();
    }

    public void ThrowCup()
    {
        _IsCupHandled = false;
        SpawnGoldenCup();
    }

    public void OnClick()
    {
        if (Time.time - _lastClick < _clickDuration)
            return;
        if (State == CupHandlerState.Start)
            StartElongate();
        else if (State == CupHandlerState.Elongation)
            StopElongation();
        else if (State == CupHandlerState.Rotating)
            StopRotate();
        _lastClick = Time.time;
    }

    private void StopElongation()
    {
        if (_Action != null)
            StopCoroutine(_Action);

        if (_Sticks.Count == 1 && State == CupHandlerState.Elongation)
            StartElongate();
        else if ((_Sticks.Count == _MaxSticks) && State == CupHandlerState.Elongation)
            StartElongateBack();
        else if (State == CupHandlerState.ElongationBack)
        {
            var lastStick = _Sticks[_Sticks.Count - 1];
            var lastCircle = _Circles[_Circles.Count - 1];

            _Sticks.Remove(lastStick);
            _Circles.Remove(lastCircle);
            Destroy(lastStick.gameObject);
            Destroy(lastCircle.gameObject);
            if (_Sticks.Count == 0)
                State = CupHandlerState.Start;
            else
                RotateBack();
        }
        else
            StartRotate();
    }

    private void RotateBack()
    {
        if (_Action != null)
            StopCoroutine(_Action);
        State = CupHandlerState.RotatingBack;
        _Action = StartCoroutine(RotatingBack(_Sticks[_Sticks.Count - 1].transform.eulerAngles));
    }

    private void StartRotate()
    {
        if (_Action != null)
            StopCoroutine(_Action);
        State = CupHandlerState.Rotating;
        _Action = StartCoroutine(Rotating());
    }

    private void StartElongate()
    {
        if (_Action != null)
            StopCoroutine(_Action);

        var newCircle = Instantiate<SpriteRenderer>(_Circle, null);
        var newStick = Instantiate<SpriteRenderer>(_Stick, null);

        newCircle.transform.position = transform.position;
        newStick.transform.position = transform.position;
        newStick.transform.eulerAngles = transform.eulerAngles;
        newStick.transform.localScale = new Vector3(1, 0, 1);
        _Circles.Add(newCircle);
        _Sticks.Add(newStick);
        State = CupHandlerState.Elongation;
        _Action = StartCoroutine(Elongation());
    }

    private void StartElongateBack()
    {
        if (_Action != null)
            StopCoroutine(_Action);
        State = CupHandlerState.ElongationBack;
        _Action = StartCoroutine(ElongationBack());
    }

    private void StopRotate()
    {
        transform.parent = null;
        if (_Action != null)
            StopCoroutine(_Action);
        if (State == CupHandlerState.Rotating)
            StartElongate();
        else if (State == CupHandlerState.RotatingBack)
            StartElongateBack();
    }

    private IEnumerator RotatingBack(Vector3 targetRot)
    {
        while (Vector3.Distance(transform.eulerAngles, targetRot) > 0.1f)
        {
            while (_isPaused)
                yield return null;
            Vector3 newRot;
            if (Vector3.Distance(transform.eulerAngles, targetRot) < 180)
                newRot = Vector3.MoveTowards(transform.eulerAngles, targetRot, Time.deltaTime * _RotationSpeed * 18);
            else
                newRot = Vector3.MoveTowards(transform.eulerAngles, transform.eulerAngles + Vector3.forward * (transform.eulerAngles.z > targetRot.z ? 5 : -5), Time.deltaTime * _RotationSpeed * 18);
            transform.eulerAngles = newRot;
            yield return null;
        }
        transform.eulerAngles = targetRot;
        StopRotate();
    }    

    private IEnumerator Rotating()
    {
        Transform lastStick = _Sticks[_Sticks.Count - 1].transform;
        Vector3 startRot = transform.eulerAngles;
        float direction = -1;
        Vector3 currentRot = startRot;

        transform.parent = lastStick;

        /*
        while (transform.eulerAngles != startRot && transform.eulerAngles.z != 360 + startRot.z)
        {
            while (_isPaused)
                yield return null;
            Vector3 newRot;
            if (transform.eulerAngles.z > 180)
                newRot = Vector3.MoveTowards(transform.eulerAngles, startRot, Time.deltaTime * _RotationSpeed);
            else
                newRot = Vector3.MoveTowards(transform.eulerAngles, startRot - Vector3.forward * 360, Time.deltaTime * _RotationSpeed);
            transform.eulerAngles = newRot;
            yield return null;
        }
        */
        while (true)
        {
            while (_isPaused)
                yield return null;
            currentRot.z += _RotationSpeed * Time.deltaTime * direction;
            if (currentRot.z >= startRot.z + _MaxRotDelta)
            {
                currentRot.z = startRot.z + _MaxRotDelta;
                direction *= -1;
            }
            else if (currentRot.z <= startRot.z - _MaxRotDelta)
            {
                currentRot.z = startRot.z - _MaxRotDelta;
                direction *= -1;
            }
            lastStick.eulerAngles = currentRot;
            yield return null;
        }
    }

    private IEnumerator ElongationBack()
    {
        var CurrentStick = _Sticks[_Sticks.Count - 1];
        float distance = Vector3.Distance(transform.position, CurrentStick.transform.position);

        while (distance > 0)
        {
            while (_isPaused)
                yield return null;
            transform.position = Vector3.MoveTowards(transform.position, CurrentStick.transform.position, Time.deltaTime * _ElongationSpeed * 3);
            distance = Vector3.Distance(transform.position, CurrentStick.transform.position);
            CurrentStick.transform.localScale = new Vector3(1, distance / _MaxDistance, 1);
            yield return null;
        }
        StopElongation();
    }

    private IEnumerator Elongation()
    {
        var CurrentStick = _Sticks[_Sticks.Count - 1];
        float distance = Vector3.Distance(transform.position, CurrentStick.transform.position);

        while (distance < _MaxDistance)
        {
            while (_isPaused)
                yield return null;
            transform.position -= transform.up * Time.deltaTime * _ElongationSpeed;
            distance = Vector3.Distance(transform.position, CurrentStick.transform.position);
            CurrentStick.transform.localScale = new Vector3(1, distance / _MaxDistance, 1);
            yield return null;
        }
        StopElongation();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_CupPosState.Value == "Bug" && 
            (State == CupHandlerState.Elongation || State == CupHandlerState.Rotating) && 
            collision.GetComponent<CupZone_21>() is var zone && zone != null)
        {
            animator.Play("Take Cup");
        }
        else if (_IsCupHandled && collision.GetComponent<ThrowCupZone_21>() is var throwZone && throwZone != null)
        {
            animator.Play("Throw Cup");
        }
        else if (collision.GetComponent<Ground>() is var ground && ground != null)
        {
            transform.parent = null;
            StartElongateBack();
        }
    }
}
