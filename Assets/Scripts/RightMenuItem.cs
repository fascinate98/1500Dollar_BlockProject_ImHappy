﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RightMenuItem : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public string menuString = null;
    public DescBox db;
    public void OnPointerDown(PointerEventData eventData)
    {
 
    }

    public void OnPointerEnter(PointerEventData eventData)
    { 
        db.transform.GetComponentInChildren<Text>().text = menuString;
        var dbRect = db.transform as RectTransform;
        var thisRect = transform as RectTransform;
        dbRect.position = new Vector2(dbRect.position.x, thisRect.position.y);
        db.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        db.gameObject.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    { 
    }
}