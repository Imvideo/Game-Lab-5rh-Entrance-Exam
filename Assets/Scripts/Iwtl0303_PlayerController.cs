using UnityEngine;

public class Iwtl0303_PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    public float speed = 10.0f;
    public float cooldown = 1.0f;
    private float nextReadyTime = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        transform.Translate(Vector3.up * verticalInput * Time.deltaTime * speed);

        if (Input.GetMouseButtonDown(1))
        {
            if (Time.time >= nextReadyTime)
            {
                Teleport();
                nextReadyTime = Time.time + cooldown;
            }
        }
    }

    void Teleport() // 순간이동
    {
        Vector3 mouse = Input.mousePosition;
        Vector3 world = Camera.main.ScreenToWorldPoint(mouse);

        transform.position = new Vector3(world.x, world.y, transform.position.z);
    }

}
