using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelperInteractiveObject : MonoBehaviour
{
    [Header("Interactive Object")]
    [SerializeField] private GameObject[] m_Object;

    [Header("Settings")]    
    [Range(0f, 10f)]
    [Tooltip("In Second")]
    [SerializeField] private float timerActivity = 6f;

  

    [Header("System (DONT TUCH)")]
    [Space(50)]
    [SerializeField] private GameObject prefabHelper;
    [SerializeField] private Button helpBtn;


    private void Awake()
    {
        helpBtn.onClick.AddListener(OnClickBtn);
    }

    public void OnClickBtn()
    {        
        foreach (GameObject item in m_Object)
        {
            Debug.Log(item.name);
            if(item.activeSelf)
            {
                Debug.Log(item.name);
                GenerateGlue(item);
                break;                
            }
        }
    }


    private void GenerateGlue(GameObject obj)
    {
        Collider2D[] colliders = obj.GetComponents<Collider2D>();

        foreach (Collider2D item in colliders)
        {
            if (item.enabled)
            {                
                GameObject glue = Instantiate(prefabHelper, item.bounds.center, Quaternion.identity, obj.transform);
                StartCoroutine(DestroyGlue(glue));
                break;
            }            
        }
    }


    IEnumerator DestroyGlue(GameObject glue)
    {
        yield return new WaitForSeconds(timerActivity);
        Destroy(glue);
    }

    private void OnDestroy()
    {
        helpBtn.onClick.RemoveListener(OnClickBtn);
    }
}
