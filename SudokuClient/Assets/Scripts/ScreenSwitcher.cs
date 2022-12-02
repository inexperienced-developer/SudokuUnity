using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenSwitcher : MonoBehaviour
{
    [SerializeField] int screenToSwitchTo;
    
    public void ChangeScene()
    {
        SceneManager.LoadScene(screenToSwitchTo);
    }
}
