using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ShapeManager : MonoBehaviour
{
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
            dc.Add(obj, obj.Positions); 
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
        List<Vector3> compareList2 = new List<Vector3>();
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
                    compareList2.Add(positions);
            } 
        } 
        for (int i = 0; i < compareList.Count; i++)
        { 
            for(int j = 0; j < compareList2.Count; j++)
            {
                if(compareList2[j] == compareList[i] && retList.Contains(compareList[i]) == false)
                {  
                    retList.Add(compareList[i]); 
                }
            } 
        } 
        return retList;
    }

}
