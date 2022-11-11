using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ScaleChanger))]
[RequireComponent(typeof(CharacterIniter))]
public class CharacterMovement : MonoBehaviour
{
    public Vector2 Speed = Vector2.zero;
    public MapPoint PrevPoint { get; private set; } = null;

    [SerializeField] private PointSystem _PointSystem = null;
    [SerializeField] private bool _IsTeleporting = false;
    [SerializeField] private CharacterIniter _ValueIniter = null;
    [SerializeField] private Vector2 _StartSpeed = new Vector3(6, 6);
    [SerializeField] private AnimationController _AnimController = null;
    [SerializeField] private bool _IsPaused = false;
    private GameProgressManager _ProgressManager = null;
    private Vector2 _MaxSpeed;
    private MapPoint _currentPoint = null;
    private Path m_CurrentPath = null;
    private List<UnityAction> _OnEnable = new List<UnityAction>();
    private PlayerSide _CurrentSide;
    private ScaleChanger _ScaleChanger = null;
    private Coroutine _Moving = null;

    private GameProgressManager ProgressManager
    {
        get
        {
            if (!_ProgressManager)
                _ProgressManager = GameProgressManager.Manager;
            return _ProgressManager;
        }
    }
    public Vector2 StartSpeed
    {
        get => _StartSpeed;
    }
    public MapPoint CurrentPoint 
    {
        get => _currentPoint;
        set
        {
            _currentPoint = value;
            if (value)
                ProgressManager.PlayerData.LastPoint = _currentPoint.name;
        }
    }

    public Vector2 Velocity { get; private set; } = Vector2.zero;

    public PlayerSide CurrentSide
    {
        get => _CurrentSide;
        private set
        {
            _CurrentSide = value;
            ProgressManager.PlayerData.LastSide = _CurrentSide.SideName;
        }
    }

    private PointSystem PointSys
    {
        get
        {
            if (_PointSystem == null)
                _PointSystem = FindObjectOfType<PointSystem>();
            return _PointSystem;
        }
    }


    delegate void Action();

    private void Start()
    {
        _MaxSpeed = _StartSpeed;
        Speed = _StartSpeed;
        _AnimController = GetComponentInChildren<AnimationController>();
        _ValueIniter = GetComponent<CharacterIniter>();
        _ScaleChanger = GetComponent<ScaleChanger>();
        SetToPoint(_ValueIniter.Start.StartPoint);
        CurrentSide = _ValueIniter.Start.StartSide;
    }


    public void Pause()
    {
        _IsPaused = true;
        Velocity = Vector2.zero;
    }
    
    public void Continue()
    {
        _IsPaused = false;
    }

    public void Stop()
    {
        if (_Moving != null)
            StopCoroutine(_Moving);
        m_CurrentPath = null;
        Velocity = Vector2.zero;
        Debug.Log(CurrentPoint.name);
    }
    public IEnumerator Moving()
    {
        float maxDistance;
        while (CurrentPoint)
        {
            maxDistance = Vector2.Distance(transform.position, CurrentPoint.transform.position);
            _ScaleChanger.TargetScale = CurrentPoint.TargetScale;
            while (Vector2.Distance(transform.position, CurrentPoint.transform.position) > 0.1f)
            {
                while (_IsPaused)
                    yield return null;
                Vector3 delta = Vector3.zero;
                delta.x = Vector3.MoveTowards(transform.position, CurrentPoint.transform.position, Time.deltaTime * Speed.x).x;
                delta.y = Vector3.MoveTowards(transform.position, CurrentPoint.transform.position, Time.deltaTime * Speed.y).y;
                delta -= transform.position;
                UpdateVelocity(delta, Speed);
                transform.position += delta;
                Speed = _MaxSpeed * _ScaleChanger.CalcNewScale(maxDistance, delta.magnitude);
                yield return null;
            }
            transform.position = CurrentPoint.transform.position;
            PrevPoint = CurrentPoint;
            _ScaleChanger.ScaleToTarget();
            if (Velocity != Vector2.zero)
                CurrentSide = CurrentPoint.GetPlayerSide(Velocity / 2);
            CurrentPoint = m_CurrentPath.GetNextPoint();
            if (PrevPoint is ActionMapPoint && ((ActionMapPoint)PrevPoint).GetEvent(CurrentPoint) is UnityEvent pointEvent)
            {
                pointEvent?.Invoke();
                yield return null;
            }
        }
        PrevPoint = CurrentPoint;
        CurrentPoint = m_CurrentPath.CurrentPoint;
        Velocity = Vector2.zero;
    }

    private IEnumerator Moving(Action finishAction)
    {
        float maxDistance;
        while (CurrentPoint)
        {
            maxDistance = Vector2.Distance(transform.position, CurrentPoint.transform.position);
            _ScaleChanger.TargetScale = CurrentPoint.TargetScale;
            while (Vector2.Distance(transform.position, CurrentPoint.transform.position) > 0.1f)
            {
                while (_IsPaused)
                    yield return null;
                Vector3 delta = Vector3.zero;
                delta.x = Vector3.MoveTowards(transform.position, CurrentPoint.transform.position, Time.deltaTime * Speed.x).x;
                delta.y = Vector3.MoveTowards(transform.position, CurrentPoint.transform.position, Time.deltaTime * Speed.y).y;
                delta -= transform.position;
                UpdateVelocity(delta, Speed);
                transform.position += delta;
                Speed = _MaxSpeed * _ScaleChanger.CalcNewScale(maxDistance, delta.magnitude);
                yield return new WaitForFixedUpdate();
            }
            transform.position = CurrentPoint.transform.position;
            PrevPoint = CurrentPoint;
            _ScaleChanger.ScaleToTarget();
            if (Velocity != Vector2.zero)
                CurrentSide = CurrentPoint.GetPlayerSide(Velocity / 2);
            CurrentPoint = m_CurrentPath.GetNextPoint();
            if (PrevPoint is ActionMapPoint && ((ActionMapPoint)PrevPoint).GetEvent(CurrentPoint) is UnityEvent pointEvent)
            {
                pointEvent?.Invoke();
            }
        }
        PrevPoint = CurrentPoint;
        CurrentPoint = m_CurrentPath.CurrentPoint;
        
        Velocity = Vector2.zero;
        yield return null;
        finishAction?.Invoke();
    }
    //Обновляет направление исключительно для Animator'а, чтобы включалась нужная сторона персонажа
    private void UpdateVelocity(Vector2 delta, Vector2 speed)
    {
        Vector2 newVelocity = Vector2.zero;
        newVelocity.x = delta.x / (Time.deltaTime * Speed.x);
        newVelocity.y = delta.y / (Time.deltaTime * Speed.y);
        if (Mathf.Abs(newVelocity.x) >= 0.25f)
            newVelocity.x = newVelocity.x < 0 ? -2 : 2;
        if (Mathf.Abs(newVelocity.y) >= 0.1f)
            newVelocity.y = newVelocity.y < 0 ? -2 : 2; 
        Velocity = newVelocity;
    }

    private void SetPath(Path newPath)
    {
        m_CurrentPath = newPath;
        PrevPoint = CurrentPoint;
        CurrentPoint = m_CurrentPath?.CurrentPoint;
    }

    public void Move(MapPoint point)
    {
        if (!gameObject.activeSelf)
            return;
        Path newPath = PointSys.GetPath(CurrentPoint, PrevPoint, point);
        Move(newPath);
    }

    public void Move(Path path)
    {
        if (!gameObject.activeSelf)
            return;
        SetPath(path);
        if (_Moving != null)
            StopCoroutine(_Moving);
        if (!_IsTeleporting)
            _Moving = StartCoroutine(Moving());
        else
            SetToPoint(m_CurrentPath.LastPoint);
    }
    public void MoveWrong(Path path)
    {
        Debug.Log("MOVE WRING: " + _AnimController);

        if (_Moving != null)
            StopCoroutine(_Moving);
        if (!_IsTeleporting && path != null)
        {            
            SetPath(path);
            _Moving = StartCoroutine(Moving(() => _AnimController.WrongAction()));
        }
        if (_IsTeleporting)
            SetToPoint(m_CurrentPath.LastPoint);
        if (path == null)
            _AnimController.WrongAction();
    }

    public void SetToPointOnPause(MapPoint point)
    {
        IEnumerator Routine()
        {
            while (!_IsPaused)
                yield return null;
            yield return null;
            SetToPoint(point);
        }
        StartCoroutine(Routine());
    }
    public void SetToPoint(MapPoint point)
    {
        PrevPoint = point;
        CurrentPoint = point;
        _ScaleChanger.TargetScale = point.TargetScale;
        _ScaleChanger.ScaleToTarget();
        transform.position = point.transform.position;
    }

    public void MoveAndInteract(Path path, InteractableObject target)
    {
        if (!gameObject.activeSelf)
            return;
        StopAllCoroutines();
        if (path != null)
        {
            SetPath(path);
            if (!_IsTeleporting)
                _Moving = StartCoroutine(Moving(() =>
                {
                    if (CurrentPoint == target.Point)
                        target.Interact();
                }));
            else
            {
                SetToPoint(m_CurrentPath.LastPoint);
                target.Interact();
            }
        }
        else
            target.Interact();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("PerspectiveSlower"))
            _MaxSpeed = collision.GetComponent<PerspectiveSlower>().TargetSpeed;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Contains("PerspectiveSlower"))
            _MaxSpeed = _StartSpeed;
    }
}
