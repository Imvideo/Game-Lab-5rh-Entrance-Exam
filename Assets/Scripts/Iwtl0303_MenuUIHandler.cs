using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class Iwtl0303_MenuUIHandler : MonoBehaviour
{
    public TMP_InputField nameInput;

    public void StartNew()
    {
        string playerName = nameInput.text.Trim();
        if (string.IsNullOrEmpty(playerName))
        {
            playerName = "Guest";
        }

        Iwtl0303_PersistentData.Instance.PlayerName = playerName;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
