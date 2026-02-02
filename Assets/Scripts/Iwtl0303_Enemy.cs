using UnityEngine;

public class Iwtl0303_Enemy : MonoBehaviour
{
    public float speed;
    private Rigidbody2D enemyRb;
    private GameObject player;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        if (!Iwtl0303_MainManager.Instance.isGameOver && !Iwtl0303_MainManager.Instance.isWin)
        {
            Vector2 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.MovePosition(enemyRb.position + lookDirection * speed * Time.fixedDeltaTime);
        }
        
    }

}
