using InexperiencedDeveloper.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CurrentScreen
{
    MainMenu,
    Options,
    GameScreen
}

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private UIScreens screens;
    public GraphicRaycaster Raycaster { get; private set; }

    public bool ClearTiles = true;

    public void SignIn()
    {
        NetworkManager.Instance.Connect();
    }

    public void StartGame()
    {
        if (ClearTiles)
        {
            GameManager.Instance.ClearTiles();
            PuzzleManager.Instance.StartGame();
        }
        screens.CurrentScreen = CurrentScreen.GameScreen;
    }

    public void GoBack()
    {
        screens.CurrentScreen = CurrentScreen.MainMenu;
    }

    public void SetRaycaster(GraphicRaycaster raycaster)
    {
        Raycaster = raycaster;
    }
}
