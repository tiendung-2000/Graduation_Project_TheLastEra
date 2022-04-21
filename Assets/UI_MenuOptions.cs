using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MenuOptions : UI_Canvas
{
    public Button backToMenu;
    public Button backToTown;
    public Button openOptions;
    public Button quitGame;
    public Button exitMenuOptions;
    
    public override void Start()
    {
        base.Start();
        backToMenu.onClick.AddListener(BackToMenu);
        backToTown.onClick.AddListener(BackToTown);
        openOptions.onClick.AddListener(GameOptions);
        quitGame.onClick.AddListener(QuitGame);
        exitMenuOptions.onClick.AddListener(ExitMenuOptions);
    }

    void BackToMenu()
    {
        ScenesManager.instance.ChangeScene(0);
    }
    void BackToTown()
    {
        ScenesManager.instance.ChangeScene(1);      
    }
    void GameOptions()
    {

    }
    void QuitGame()
    {
        Application.Quit();
    }
    void ExitMenuOptions()
    {
        gameObject.SetActive(false);
    }
}
