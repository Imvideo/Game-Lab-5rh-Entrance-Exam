using UnityEngine;

public class Iwtl0303_EnemyHealth : MonoBehaviour
{
    public int hp = 5;
    public int point = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attacked(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Iwtl0303_MainManager.Instance.AddPoint(point);
            Destroy(gameObject);
        }
    }
}
