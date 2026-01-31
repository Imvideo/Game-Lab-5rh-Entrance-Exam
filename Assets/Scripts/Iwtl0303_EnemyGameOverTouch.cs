using UnityEngine;

public class Iwtl0303_EnemyGameOverTouch : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        Debug.Log("[Hit] enabled: " + transform.root.name);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Iwtl0303_MainManager.Instance?.GameOver();
        }
    }
}
