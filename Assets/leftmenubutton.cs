using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class leftmenubutton : MonoBehaviour,   IPointerEnterHandler, IPointerExitHandler
{

    public Image img;


    public void OnPointerEnter(PointerEventData eventData)
    {
        img.color = new Color(img.color.r, img.color.g, img.color.b, 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0.6f);
    }



    public void Start()
    {
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0.6f);
    }
}
