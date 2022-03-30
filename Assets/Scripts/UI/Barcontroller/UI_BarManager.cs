using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BarManager : MonoBehaviour
{
    public static UI_BarManager instance;
    public List<UI_BarController> list_BarControllers;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else instance = this;
    }

    public void AddBarController(UI_BarController controller)
    {
        list_BarControllers.Add(controller);
    }

    public UI_BarController GetBarController(BarName barName)
    {
        for(int i = 0; i< list_BarControllers.Count; i++)
        {
            if (list_BarControllers[i].barName == barName)
                return list_BarControllers[i];
        }
        return null;
    }
}
