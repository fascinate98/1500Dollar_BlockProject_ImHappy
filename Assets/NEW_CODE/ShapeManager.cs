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

    public DataContainer<ShapeObject, List<Vector3>> dc = new DataContainer<ShapeObject, List<Vector3>>();
    public void AddShapeObject(ShapeObject obj)
    {
        if (dc.IsExist(obj) == false)
        {
            Debug.Log("registered  => " + this.name);
            dc.Add(obj, obj.Positions); 
        }
        else
        {
            Debug.Log("dupliated  object.");
        }
    }


    public void RemoveShapeObject(ShapeObject obj)
    {
        if(dc.IsExist(obj))
        {
            dc.Remove(obj);
        }
    } 
    public List<Vector3> GetExistVectorList(ShapeObject compareListTarget)
    { 
        List<Vector3> compareList = compareListTarget.Positions;
        Dictionary<Vector3, int> compareList2 = new Dictionary<Vector3, int>();
        List<Vector3> retList = new List<Vector3>(); 
        foreach (var data in dc.map)
        { 
            //같은 객체는 패스
            if (data.Key == compareListTarget) 
                continue;
            else
            {
                //다른 객체의 Value와하기위해 compareList2에 취합
                foreach (var positions in data.Value)
                    if((compareList2.ContainsKey(positions) == false))
                    compareList2.Add(positions, 0);
            } 
        }  
        for (int i = 0; i < compareList.Count; i++)
        {
            if (compareList2.ContainsKey(compareList[i]))
            { 
                if (retList.Contains(compareList[i]) == false)
                {
                    retList.Add(compareList[i]);
                }
            } 
        } 
        return retList;
    }

}
