using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Timeline;

public class StatesChecker : MonoBehaviour
{
    [SerializeField]
    private List<State> _StatesForCheck = new List<State>();

    [SerializeField]
    private List<string> _TargetValues = new List<string>();

    [SerializeField]
    private List<UnityEvent> _TargetEvents = new List<UnityEvent>();

    [SerializeField]
    private List<TimelineAsset> _Clips = new List<TimelineAsset>();

    [SerializeField]
    private List<Item> _Items = new List<Item>();

    public (UnityEvent, TimelineAsset) GetValues(Item item)
    {
        int index = _TargetValues.FindIndex((string val) => {
            string[] values = val.Split('\n');
            for (int i = 0; i < _StatesForCheck.Count && i < values.Length; i++)
            {
                if (values[i][0] == '*')
                    continue;
                if (values[i][0] == '!')
                {
                    if ("!" + _StatesForCheck[i].Value.ToLower() == values[i].ToLower())
                        return false;
                }
                else
                {
                    if (_StatesForCheck[i].Value.ToLower() != values[i].ToLower())
                        return false;
                }
            }
            if (values[values.Length - 1].ToLower() != (item?.name  ?? "none").ToLower())
                return false;
            return true;
            });
        if (index < 0)
            return (null, null);
        return (_TargetEvents[index], _Clips[index]);
    }

    public string UpdateStates()
    {
        string newState = "";
        foreach (State target in _StatesForCheck)
            newState += $"{target.Value}\n";
        return newState;
    }
}
