using UnityEngine;
using UnityEngine.SceneManagement;


public class QuitManager : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        // Example: Restart the scene when the 'R' key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartCurrentScene();
        }
    }

    void RestartCurrentScene()
    {
        // Get the current scene name
        string sceneName = SceneManager.GetActiveScene().name;
        
        // Load the scene with the same name
        SceneManager.LoadScene(sceneName);
    }
}
