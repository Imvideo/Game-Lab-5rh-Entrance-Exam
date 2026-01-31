using UnityEngine;

public class Iwtl0303_PersistentData : MonoBehaviour
{
    public static Iwtl0303_PersistentData Instance;

    public string PlayerName;
    public int BestScore;
    public string BestName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


}
