using InexperiencedDeveloper.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Player")]
    [SerializeField] private GameObject m_PlayerPrefab;
    public GameObject PlayerPrefab => m_PlayerPrefab;
}
