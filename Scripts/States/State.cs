using UnityEngine;

public class State : MonoBehaviour
{
    [SerializeField]
    protected string _Value;

    public string Value
    {
        get => _Value;
        set => _Value = value;
    }
}