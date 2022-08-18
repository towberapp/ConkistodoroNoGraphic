using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puzzle_20 : MonoBehaviour
{
    [SerializeField] private PuzzleButton_20[] _Buttons = new PuzzleButton_20[] { };
    [SerializeField] private bool _IsAimUp = true;
    [SerializeField] private UnityEvent _DoneEvent;
    // Start is called before the first frame update
    void Start()
    {
        _Buttons = GetComponentsInChildren<PuzzleButton_20>(false);
        StartCoroutine(Check());
    }

    // Update is called once per frame
    private IEnumerator Check()
    {
        bool isReady = false;
        while (!isReady)
        {
            yield return new WaitForSeconds(0.1f);
            isReady = true;
            foreach (var button in _Buttons)
                if (button.IsUp != _IsAimUp)
                {
                    isReady = false;
                    break;
                }
        }
        _DoneEvent.Invoke();
    }
}
