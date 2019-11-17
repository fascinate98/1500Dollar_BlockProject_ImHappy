using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class ShapeObject : Sirenix.OdinInspector.SerializedMonoBehaviour
{
    public enum ShapeStatus
    {
        NormalMode,
        EditMode
    }
    public Dictionary<Transform, int> dc = new Dictionary<Transform, int>();
    public ShapeStatus shapeStatus;
    public bool isDebugMode = false;
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
    private void Awake() => Init();
    private void Init()
    {
        ShapeManager.Instance.AddShapeObject(this); 
    }

   
    public void RemoveBlock()
    {
        ShapeManager.Instance.RemoveShapeObject(this);
        for (int i = 0; i < ShapeManager.Instance.dc.Count; i++)
        {
            var data = ShapeManager.Instance.dc[i];
            var ctk = data.dc.ContainsKey(this.transform);
            if (ctk)
            { 
                data.dc.Remove(this.transform); 
                int cnt = 0;
                foreach (var vv in data.dc)
                {
                    cnt++;
                }

                if (cnt == 0)
                    data.GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }
        Destroy(this.gameObject);
    }

#if  UNITY_EDITOR
    [ContextMenu("positionDatas comp  delete")]
    public void DestroyAllComp()
    {
        foreach (var v in positionDatas)
        {
            var comps = v.GetComponents<Component>();
            foreach (var comp in comps)
            {
                if (comp == v.transform) continue;
                DestroyImmediate(comp);
            }
        }
    }
#endif

    public void OnCollisionEnter(Collision collision)
    {
        if (!dc.ContainsKey(collision.transform))
            dc.Add(collision.transform, 1);
        else
            dc[collision.transform] += 1;

        if (dc.ContainsKey(collision.transform))
        {
            if (dc[collision.transform] > 0)
                this.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        Debug.Log("혷");
        if (dc.ContainsKey(collision.transform))
        {
            dc[collision.transform] -= 1;
            if (dc[collision.transform] == 0)
            {

                dc.Remove(collision.transform);
            }
        }

        int cnt = 0;
        foreach (var data in dc)
        {
            cnt++;
        }

        if(cnt == 0)
            this.GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
