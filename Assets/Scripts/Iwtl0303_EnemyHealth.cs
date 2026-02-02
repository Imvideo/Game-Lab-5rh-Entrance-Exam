using System.Collections;
using UnityEngine;

public class Iwtl0303_EnemyHealth : MonoBehaviour
{
    public int hp = 5;
    public int point = 5;

    public Renderer rend;
    public Color hitColor = Color.blue;
    public float hitFlashTime = 0.08f;

    static readonly int BaseColorId = Shader.PropertyToID("_BaseColor");
    static readonly int ColorId = Shader.PropertyToID("_Color");
    Color originalColor;
    Coroutine flashCo;

    public GameObject DeathEffectPrefab;
    public float deathEffectLife = 1.5f;

    void Awake()
    {
        if (rend == null) rend = GetComponent<Renderer>();

        Debug.Log($"[EnemyHealth] rend={(rend ? rend.name : "NULL")}, shader={(rend && rend.sharedMaterial ? rend.sharedMaterial.shader.name : "NULL")}");

        if (rend == null || rend.sharedMaterial == null) return;

        if (rend.sharedMaterial.HasProperty(BaseColorId))
            originalColor = rend.sharedMaterial.GetColor(BaseColorId);
        else if (rend.sharedMaterial.HasProperty(ColorId))
            originalColor = rend.sharedMaterial.GetColor(ColorId);
        else
            Debug.LogError("[EnemyHealth] material has no _BaseColor/_Color");
    }

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
        Flash();
        if (hp <= 0)
        {
            SpawnDeathEffect();
            Iwtl0303_MainManager.Instance.AddPoint(point);
            Destroy(gameObject);
        }
    }

    void Flash() // 맞으면 색 변경
    {
        if (flashCo != null) StopCoroutine(flashCo);
        flashCo = StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        SetRenderColor(hitColor);
        yield return new WaitForSeconds(hitFlashTime);
        SetRenderColor(originalColor);
        flashCo = null;
    }

    void SetRenderColor(Color c)
    {
        var mat = rend.material;

        if (mat.HasProperty(BaseColorId))
            mat.SetColor(BaseColorId, c);
        else if (mat.HasProperty(ColorId))
            mat.SetColor(ColorId, c);
    }

    void SpawnDeathEffect()
    {
        var fx = Instantiate(DeathEffectPrefab, transform.position, Quaternion.identity);
        Destroy(fx, deathEffectLife);
    }

}
