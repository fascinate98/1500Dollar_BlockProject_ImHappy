using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuWindowUI : MonoBehaviour
{
    public bool isMenuOpen;
    public GameObject menu;
    public float speed = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void menuButton()
    {
        isMenuOpen = !isMenuOpen; //머가어려운게잇니...?
    }

    public void exit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        var menurectTransform = this.transform as RectTransform;
        if (isMenuOpen)
        {
            Vector2 newvec = new Vector2(0, menurectTransform.anchoredPosition.y);
            menurectTransform.anchoredPosition = Vector2.Lerp(menurectTransform.anchoredPosition, newvec, speed);
        }
        else
        {
            Vector2 newvec = new Vector2(250, menurectTransform.anchoredPosition.y);
            menurectTransform.anchoredPosition = Vector2.Lerp(menurectTransform.anchoredPosition, newvec, speed);
        }
    }
}
