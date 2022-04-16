using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIName {UIPlayer, UIStore, UIBag, UIGameOver,UIGameOptions}

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public List<UI_Canvas> list_UICanvases;

    public void AddUICanvas(UI_Canvas uI_Canvas)
    {
        list_UICanvases.Add(uI_Canvas);
    }

    public UI_Canvas GetUI_Canvas(UIName uIName)
    {
        for (int i = 0; i < list_UICanvases.Count; i++)
        {
            if (list_UICanvases[i].uiName == uIName)
                return list_UICanvases[i];
        }
        return null;
    }

    bool bagOn;
    bool shopOn;
    bool gameOverOn;
    bool playerOn = true;
    bool gameOptionsOn;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            bagOn = !bagOn;
            if (bagOn)
                GetUI_Canvas(UIName.UIBag).OnOpen();
            else GetUI_Canvas(UIName.UIBag).OnClose();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            shopOn = !shopOn;
            if (shopOn)
                GetUI_Canvas(UIName.UIStore).OnOpen();
            else GetUI_Canvas(UIName.UIStore).OnClose();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameOptionsOn = !gameOptionsOn;
            if (gameOptionsOn)
                GetUI_Canvas(UIName.UIGameOptions).OnOpen();
            else GetUI_Canvas(UIName.UIGameOptions).OnClose();
        }

        if (playerOn)
        {
            GetUI_Canvas(UIName.UIPlayer).OnOpen();
        }
        else
        {
            GetUI_Canvas(UIName.UIPlayer).OnClose();
        }
    }
}
