using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

[Serializable]
public class ButtonContainer
{
    [SerializeField] private Button m_Button;
    [SerializeField] private UnityEvent m_Action;

    public void Init()
    {
        m_Button.onClick.AddListener(() => m_Action.Invoke());
    }
}
