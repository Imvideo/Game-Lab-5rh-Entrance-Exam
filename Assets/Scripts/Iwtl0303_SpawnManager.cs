using System.Collections;
using UnityEngine;

public class Iwtl0303_SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float minSpwanDelay = 0.5f;
    public float maxSpwanDelay = 2.0f;

    public float spawnOffset = 1.0f;

    Camera mainCam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCam = Camera.main;
        StartCoroutine(SpawnLoop());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            if (Iwtl0303_MainManager.Instance != null && Iwtl0303_MainManager.Instance.isGameOver)
            {
                yield break;
            }

            float delay = Random.Range(minSpwanDelay, maxSpwanDelay);
            yield return new WaitForSeconds(delay);

            if (Iwtl0303_MainManager.Instance != null && Iwtl0303_MainManager.Instance.isGameOver)
            {
                yield break;
            }
            
            Vector2 spawnPos = GetSpawnPositionOutsideCamera();
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
    }

    Vector2 GetSpawnPositionOutsideCamera()
    {
        Vector2 camPos = mainCam.transform.position;

        float camHeight = mainCam.orthographicSize;
        float camWidth = camHeight * mainCam.aspect;

        // 0: 위, 1: 아래, 2: 왼쪽, 3: 오른쪽
        int side = Random.Range(0, 4);

        switch (side)
        {
            case 0: // 위
                return new Vector2(
                    camPos.x + Random.Range(-camWidth, camWidth),
                    camPos.y + camHeight + spawnOffset
                );

            case 1: // 아래
                return new Vector2(
                    camPos.x + Random.Range(-camWidth, camWidth),
                    camPos.y - camHeight - spawnOffset
                );

            case 2: // 왼쪽
                return new Vector2(
                    camPos.x - camWidth - spawnOffset,
                    camPos.y + Random.Range(-camHeight, camHeight)
                );

            default: // 오른쪽
                return new Vector2(
                    camPos.x + camWidth + spawnOffset,
                    camPos.y + Random.Range(-camHeight, camHeight)
                );
        }
    }

}
