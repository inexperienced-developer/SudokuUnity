using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalSceneUI : MonoBehaviour
{
    [SerializeField] private ButtonContainer[] m_SceneButtons;

    private void Awake()
    {
       foreach(var button in m_SceneButtons)
        {
            button.Init();
        }
    }


}
