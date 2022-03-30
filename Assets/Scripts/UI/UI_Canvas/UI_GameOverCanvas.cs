using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameOverCanvas : UI_Canvas
{
    public Button backToMenu;
    public Button backToTown;
    public override void Start()
    {
        base.Start();
        backToMenu.onClick.AddListener(BackToMenu);
        backToTown.onClick.AddListener(BackToTown);
    }

    void BackToMenu() {
        ScenesManager.instance.ChangeScene(0);
    }
    void BackToTown() {
        ScenesManager.instance.ChangeScene(1);
    }
}
