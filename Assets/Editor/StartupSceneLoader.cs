using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class StartupSceneLoader
{
    static StartupSceneLoader()
    {
        EditorApplication.playModeStateChanged += LoadStartupScene;
    }

    private static void LoadStartupScene(PlayModeStateChange state)
    {
        if(state == PlayModeStateChange.ExitingPlayMode)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        }

        if(state == PlayModeStateChange.EnteredPlayMode)
        {
            // Check if the current scene is not the startup scene
            if (EditorSceneManager.GetActiveScene().buildIndex != 0)
            {
                // Load the startup scene
                EditorSceneManager.LoadScene(0);
            }
        }

    }

}

