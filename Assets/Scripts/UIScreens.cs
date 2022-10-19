using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreens : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu, gameScreen, difficultyScreen, optionsScreen;

    private CurrentScreen currentScreen;
    public CurrentScreen CurrentScreen
    {
        get { return currentScreen; }
        set
        {
            currentScreen = value;
            SwitchScreen();
        }
    }

    private void Awake()
    {
        currentScreen = CurrentScreen.MainMenu;
    }

    public void SwitchScreen()
    {
        switch (currentScreen)
        {
            case CurrentScreen.MainMenu:
                mainMenu.SetActive(true);
                gameScreen.SetActive(false);
                break;
            case CurrentScreen.GameScreen:
                mainMenu.SetActive(false);
                gameScreen.SetActive(true);
                break;
            default:
                currentScreen = CurrentScreen.MainMenu;
                SwitchScreen();
                return;
        }
    }
}
