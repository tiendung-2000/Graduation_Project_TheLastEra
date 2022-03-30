using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;

    private void Awake()
    {
        instance = this;
    }
    public void ChangeScene(int indexScence) {
        SceneManager.LoadScene(indexScence);
    }
}
