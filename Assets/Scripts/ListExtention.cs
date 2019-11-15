using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class ListExtention 
{ 
    public static bool ContainsEx(this List<Vector3> data, Vector3 value)
    {
        foreach(var dt  in data)
        {
            if (dt == value) return true; 
        }
        return false; 
    }
}