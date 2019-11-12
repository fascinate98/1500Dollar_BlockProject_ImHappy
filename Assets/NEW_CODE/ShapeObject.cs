using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IShapeObject
{
    bool IsBuildAble();
    List<Vector3> Positions { get;  }
    void OnChangePosition(Vector3 moveAxis);
}
[System.Serializable]
public class ShapeObject : MonoBehaviour, IShapeObject
{

    public List<Vector3> positions = new List<Vector3>();
    public List<Vector3> Positions { get => positions; }

    private List<Transform> positionDatas
    {
        get
        {
            List<Transform> datas = new List<Transform>();
            var pc = this.transform.Find("PositionContainer");
            for (int i = 0; i < this.transform.Find("PositionContainer").transform.childCount; i++)
            {
                var pos = pc.GetChild(i);
                datas.Add(pos);
            }

            return datas;
        }
    }

 
    private void Awake()
    {
        Init();
    }

    private void PositionUpdate()
    {
        this.positions.Clear();
        foreach (var data in positionDatas)
        {
            positions.Add(data.position); 
        } 
    }
    private void Init()
    {
        PositionUpdate();
        ShapeManager.Instance.AddShapeObject(this);
    }
    public void ExecuteBuild()
    {

    }


    public void OnColliderEnter(Collider e)
    {
        Debug.Log(e.GetComponent<Collider>().name);
    }
  
    public bool IsBuildAble()
    {
        var existVectors = ShapeManager.Instance.GetExistVectorList(this); 
        foreach(var data  in existVectors)
        {
            foreach(var m  in positionDatas)
            {
                if (data == m.position)
                {
                    m.GetComponent<MeshRenderer>().material.color = Color.red;
                }
            }
        }
        return existVectors.Count == 0;
    }

    public void OnChangePosition(Vector3 moveAxis)
    { 
        PositionUpdate();
        var buildable = IsBuildAble();
        Debug.Log(this.gameObject.name + " =>" + buildable);
    }
}
