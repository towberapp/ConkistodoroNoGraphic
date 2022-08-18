using System.Collections;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchController : MonoBehaviour
{
    [SerializeField]
    private CharacterMovement _CharacterMovement = null;

    [SerializeField]
    private PointSystem _PointSystem = null;

    private Camera _Cam = null;

    public UIItem DraggingItem = null;


    private void Start()
    {
        _Cam = Camera.main;
        if (!_PointSystem)
            _PointSystem = FindObjectOfType<PointSystem>();
    }

    private void OnDisable()
    {
        DraggingItem?.Appear();
        DraggingItem = null;
    }
    private void CheckCollisions(RaycastHit2D[] collisions)
    {
        InteractableObject target;
        Path path;

        foreach (RaycastHit2D hit in collisions)
        {
            if (hit && hit.collider
                    && hit.collider.tag.Contains("Interactable"))
            {
                target = hit.collider.GetComponent<InteractableObject>();
                if (target.Check(DraggingItem?.Item))
                {
                    if (target.IsPointed)
                    {
                        path = _PointSystem.GetPath(_CharacterMovement.CurrentPoint, _CharacterMovement.PrevPoint, target.Point);
                        _CharacterMovement.MoveAndInteract(path, target);
                    }
                    else
                        _CharacterMovement.MoveAndInteract(null, target);
                    DraggingItem?.Appear();
                    DraggingItem = null;
                    return;
                }
            }
        }
        target = collisions[0].collider.GetComponent<InteractableObject>();
        if (target.IsPointed)
        {
            path = _PointSystem.GetPath(_CharacterMovement.CurrentPoint, _CharacterMovement.PrevPoint,
                                                            collisions[0].collider.GetComponent<InteractableObject>().Point);
            _CharacterMovement.MoveWrong(path);
        }
        else
            _CharacterMovement.MoveWrong(null);
        DraggingItem?.Appear();
        DraggingItem = null;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0)) {

     
            Vector3 mousePos = _Cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = _Cam.transform.position.z;

            if ((EventSystem.current.currentSelectedGameObject?.tag.Contains("UI") ?? false) && !DraggingItem)
            {
          
                DraggingItem?.Appear();
                DraggingItem = null;
            }
            else if (Array.FindAll<RaycastHit2D>(Physics2D.RaycastAll(mousePos, Vector3.forward, 100), (RaycastHit2D a) => a.collider.GetComponent<InteractableObject>()) is RaycastHit2D[] results && results.Length > 0)
            {
             
                CheckCollisions(results);
            }
            else if (DraggingItem != null)
            {
              
                DraggingItem.Appear();
                DraggingItem = null;
            }
            else if (!EventSystem.current.currentSelectedGameObject?.tag.Contains("UI") ?? true)
            {
               
                Path path = _PointSystem.GetPath(_CharacterMovement.CurrentPoint, _CharacterMovement.PrevPoint, mousePos);
                if (path != null)
                {
                    _CharacterMovement.Move(path);
                }
            }
        }
    }
}
