using UnityEngine;

public class State : MonoBehaviour
{
    [SerializeField]
    protected string _Value;

    public string Value
    {
        get => _Value;
        set {
            //Debug.Log("SET: " + value);
            _Value = value;
        }
    }
}