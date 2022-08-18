using UnityEngine;

public class Cup_21 : MonoBehaviour
{
    [HideInInspector] public State CupPosState;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Ground>() != null)
        {
            CupPosState.Value = "Ground";
            Destroy(gameObject);
        }
    }
}