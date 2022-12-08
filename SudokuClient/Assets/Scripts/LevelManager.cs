using InexperiencedDeveloper.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] int screenToSwitchTo;
    

    public void LoadScene(int sceneSwitch)
    {
        SceneManager.LoadScene(sceneSwitch);
    }

    public void LoadSceneAsync(int sceneSwitch, Action<AsyncOperation> callback)
    {
        SceneManager.LoadSceneAsync(sceneSwitch).completed += callback;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(screenToSwitchTo);
    }
}
