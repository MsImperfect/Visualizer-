using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Call this from a button with the scene name
    public void LoadSceneByName(string sceneName)
    {
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene " + sceneName + " not found in Build Settings!");
        }
    }

    // Optional: quit button support
    public void QuitApplication()
    {
        Debug.Log("Quit Application");
        Application.Quit();
    }
}
