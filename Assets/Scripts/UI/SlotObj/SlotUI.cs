using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    public Image icon;
    public Text countText;

    public GameObject resetButton;
    public GameObject countTextObject;

    public virtual void ResetButton() {
        
        resetButton.SetActive(false);
        countText.text = "";
        countTextObject.SetActive(false);
        icon.sprite = null;

    }

    public virtual void OnClickSlot() { 
    
    
    }

}
