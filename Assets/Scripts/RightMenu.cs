using RuntimeGizmos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static bool blocking = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        blocking = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        blocking = false;
    }
}
