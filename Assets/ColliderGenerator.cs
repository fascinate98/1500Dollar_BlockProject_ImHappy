#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
 

[System.Serializable]
public class Pattern
{
    public Vector3 addCenter;
    public Vector3 scale;
}
public class ColliderGenerator : MonoBehaviour
{
    public Vector3 baseCenter; 
    public int pattenrCount = 1;
    public List<Pattern> pattern = new List<Pattern>();
    public List<Pattern> validatePattern = new List<Pattern>();
    public float colliderScaleMultipier = 1.0f;
    public bool isPattenrGUIEnable = false;
    public bool isValidPattenrGUIEnable = false;

    public void RemovePattern()
    {
        if (pattern.Count != 0)
            pattern.RemoveAt(pattern.Count - 1);
    }
    public void AddPattern(bool createMirror = false)
    {
        if (pattern.Count == 0)
        {
            pattern.Add(new Pattern());
        }
        else
        {
            var pt = pattern[pattern.Count - 1];
            pattern.Add(new Pattern()
            {
                addCenter = pt.addCenter,
                scale = pt.scale
            });

            if (createMirror)
            {
                var i = pattern.Count - 1;
                pattern[i].addCenter.x = -pattern[i].addCenter.x;
            }
        }
    }
    public void OnDrawGizmos()
    {
        int cnt = 0;
        if (isPattenrGUIEnable)
            for (int i = 0; i < pattern.Count; i++)
            {
                Pattern data = pattern[i];
                if (i == (pattern.Count - 1))
                {
                    Gizmos.color = new Color(0, 1, 0, 1f);
                }
                else
                {
                    Gizmos.color = new Color(1, 0, 0, 1f);
                }
                Gizmos.DrawCube((this.transform.position + baseCenter) + data.addCenter, data.scale * colliderScaleMultipier);
                cnt++;
            }
        if (isValidPattenrGUIEnable)
            for (int i = 0; i < validatePattern.Count; i++)
            {
                Pattern data = validatePattern[i];
                Gizmos.color = new Color(0, 0, 1, 1f);
                Gizmos.DrawCube((this.transform.position + baseCenter) + data.addCenter, data.scale * colliderScaleMultipier);
            }
    }
    public void Buiid()
    {
        validatePattern.Clear();
        for (int i = 0; i < pattenrCount; i++)
        {
            var pc = i;
            var pos = -(baseCenter) * (i);
            foreach (var data in pattern)
            {
                var newVec = data.addCenter + pos;
                var pattern = new Pattern();
                pattern.addCenter = newVec;
                pattern.scale = data.scale * colliderScaleMultipier;
                validatePattern.Add(pattern);
            }
        }
    }
    public void Dettach()
    {
        foreach (var data in this.GetComponentsInChildren<Transform>())
        { 
            if(data.transform == this.transform)
            {
                continue;
            }
            DestroyImmediate(data.gameObject);
        }
    }
    public void CreateCollider(bool validPattern = false)
    {
        Dettach();
        if (validPattern)
        { 
                foreach (var data in validatePattern)
                {
                    var v = new GameObject();
                    v.transform.SetParent(this.transform, false);
                    var bc = v.AddComponent<BoxCollider>();
                    bc.center = data.addCenter + (baseCenter);
                    bc.size = data.scale * colliderScaleMultipier;
                } 
        }
        else
        {
            foreach (var data in pattern)
            {
                var v = new GameObject();
                v.transform.SetParent(this.transform, false);
                var bc = v.AddComponent<BoxCollider>();
                bc.center = data.addCenter + baseCenter;
                bc.size = data.scale * colliderScaleMultipier;
            }
        }
    }
} 
[CustomEditor(typeof(ColliderGenerator))]
public class ColliderGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var script = (ColliderGenerator)target;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("AddPattern"))
        {
            script.AddPattern();
        }
        if (GUILayout.Button("Mirror"))
        {
            script.AddPattern(true);
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("RemovePattern"))
        {
            script.RemovePattern();
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Baking Pattern"))
        {
            script.Buiid();
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("CreateCollider"))
        {
            script.CreateCollider();
        }
        if (GUILayout.Button("CreateCollider Valid"))
        {
            script.CreateCollider(true);
        }
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Dettach"))
        {
            script.Dettach();
        }
    }
} 


#endif