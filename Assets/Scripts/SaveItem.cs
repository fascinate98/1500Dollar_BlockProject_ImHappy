using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class SaveItem : MonoBehaviour
{
    public Text saveItemText;
    public SaveLoadMenu slMenu;
    public void SetFileName(string fileName)
    {
        this.saveItemText.text = fileName;
    } 
     
    public void OnClick()
    {
        slMenu.inputField.text = saveItemText.text;
    }
     
}
