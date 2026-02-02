using UnityEngine;

public class Iwtl0303_PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    public float speed = 10.0f;
    public float cooldown = 1.0f;
    private float nextReadyTime = 0f;

    public float padding = 0.3f; // 플레이어 반지름
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!Iwtl0303_MainManager.Instance.isGameOver && !Iwtl0303_MainManager.Instance.isWin)
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

    }

    void LateUpdate()
    {
        ClampToScreen();
    }

    public void Teleport() // 순간이동
    {
        Vector3 mouse = Input.mousePosition;
        Vector3 world = Camera.main.ScreenToWorldPoint(mouse);

        transform.position = new Vector3(world.x, world.y, transform.position.z);
    }

    void ClampToScreen() // 화면 밖 이동을 제한
    {
        if (Camera.main == null) return;

        Vector3 pos = transform.position;

        Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        pos.x = Mathf.Clamp(pos.x, min.x + padding, max.x - padding);
        pos.y = Mathf.Clamp(pos.y, min.y + padding, max.y - padding);

        transform.position = pos;
    }
}
