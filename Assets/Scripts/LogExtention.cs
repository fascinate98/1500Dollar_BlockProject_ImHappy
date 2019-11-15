using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class LogExtention 
{ 
    public static void ListLog<T>(this List<T> data)
    {
 
        foreach(var dt in data)
        {
            Debug.Log(dt);
        }
    }
}