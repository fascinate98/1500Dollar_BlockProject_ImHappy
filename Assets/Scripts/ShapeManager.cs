
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ShapeManager : MonoBehaviour
{
    public bool isDebug = false;
    public static ShapeManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<ShapeManager>();
            return instance;
        }
    }
    private static ShapeManager instance; 
    public List<ShapeObject> dc = new List<ShapeObject>();


    public void ClearAllShapeObject()
    {
        foreach(var data in dc)
        {
            Destroy(data.gameObject);
        }
        dc.Clear();
    }
    public void AddShapeObject(ShapeObject obj)
    {
        dc.Add(obj);
    } 
    public void RemoveShapeObject(ShapeObject obj)
    {
        dc.Remove(obj);
    }
 
}
