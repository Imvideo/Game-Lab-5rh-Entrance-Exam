using UnityEngine;

public class Iwtl0303_PlayerAttack : MonoBehaviour
{
    public float radius = 2.0f;
    public float angle = 90.0f;
    public int damage = 3;
    public float cooldown = 0.5f;
    private float nextReadyTime = 0f;
    public LayerMask enemyMask;

    public LineRenderer line;
    public int lineSegments = 20;
    public float showTime = 0.1f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time >= nextReadyTime)
            {
                Attack();
                nextReadyTime = Time.time + cooldown;
            }
        }
    }

    public void Attack()
    {
        Vector2 p = transform.position;
        Vector2 dir = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - p).normalized;
        float half = angle / 2;

        foreach (var h in Physics2D.OverlapCircleAll(p, radius, enemyMask))
            if (Vector2.Angle(dir, (Vector2)h.transform.position - p) <= half)
                h.GetComponentInParent<Iwtl0303_EnemyHealth>()?.Attacked(damage);

        DrawFan(dir);
    }

    public void DrawFan(Vector2 dir)
    {
        float half = angle / 2;
        line.positionCount = lineSegments + 3;

        line.SetPosition(0, Vector2.zero);

        Vector2 localDir = transform.InverseTransformDirection(dir);


        for (int i = 0; i <= lineSegments; i++)
        {
            float t = (float)i / lineSegments;
            float currentAngle = -half + angle * t;

            Vector2 rotatedDir = Rotate(localDir, currentAngle);
            Vector2 point = rotatedDir * radius;

            line.SetPosition(i + 1, point);
        }
        line.SetPosition(line.positionCount - 1, Vector2.zero);

        CancelInvoke(nameof(HideFan));
        line.enabled = true;
        Invoke(nameof(HideFan), showTime);
    }

    public Vector2 Rotate(Vector2 v, float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);

        return new Vector2(
            v.x * cos - v.y * sin,
            v.x * sin + v.y * cos
        );
    }

    public void HideFan()
    {
        line.enabled = false;
    }

}
