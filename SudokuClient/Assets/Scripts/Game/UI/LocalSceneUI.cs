using InexperiencedDeveloper.Utils.Log;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalSceneUI : MonoBehaviour
{
    [SerializeField] private GraphicRaycaster m_Raycaster;
    [SerializeField] private ButtonContainer[] m_SceneButtons;

    private void Start()
    {
        UIManager.Instance.SetRaycaster(m_Raycaster);
        foreach(var button in m_SceneButtons)
        {
            button.Init();
        }
    }

    public void JoinMatchmaking()
    {
        PlayerManager.GetLocalPlayer().RequestToJoinRandomLobby();
    }
}
