using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragableTitleBar : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform target; 
    public void OnBeginDrag(PointerEventData eventData)
    {
       
    } 

    public void OnDrag(PointerEventData eventData)
    {  
        target.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
         
    }
}
