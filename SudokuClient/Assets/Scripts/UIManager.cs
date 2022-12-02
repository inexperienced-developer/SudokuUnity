using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentScreen
{
    MainMenu,
    Options,
    GameScreen
}

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIScreens screens;

    public bool ClearTiles = true;

    public async void StartGame()
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

}
