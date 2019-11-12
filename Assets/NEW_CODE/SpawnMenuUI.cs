using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class SpawnMenuUI : MonoBehaviour
{
    public  List<GameObject> loadedObjects = new List<GameObject>();
    public Transform Content;
    public GameObject SpawnUIItemPrefab;
    public void LoadPrefabs()
    {
        var _loadedObjects  = Resources.LoadAll("Prefabs");
        foreach (var data in _loadedObjects)
        {
            var go = data as GameObject;
            loadedObjects.Add(go);
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Generate")]
    public void GeneratePrefabsPreviewTexture()
    {
        var _loadedObjects = Resources.LoadAll("Prefabs");

        foreach (var data in _loadedObjects)
        {
            var tex2D = AssetPreview.GetAssetPreview(data);
            var png = tex2D.EncodeToPNG();
            if(png != null)
            {
                var fileName = Application.dataPath + "/Resources/PreviewPNG/" + data.name + ".png";
                System.IO.File.WriteAllBytes(fileName, png);
                Debug.Log("generated => " + fileName);
                AssetDatabase.Refresh();
            }
        }

    }
#endif
    private void Awake()
    { 
        LoadPrefabs();
        for(int i  = 0; i < Content.childCount; i++)
        {
            var c = Content.transform.GetChild(i);
            Destroy(c.gameObject);
        }
        for(int i = 0; i< loadedObjects.Count; i++)
        {
            var item = GameObject.Instantiate(SpawnUIItemPrefab);
            item.transform.SetParent(Content, false);
            int value = i;
            var img = item.GetComponent<Image>();
            var loadedPreviewSprite = Resources.Load<Sprite>("PreviewPNG/" + loadedObjects[i].name);
            if (loadedPreviewSprite != null)
                img.sprite = loadedPreviewSprite;
            else
            {
                Debug.Log("PreviewPNG/" + loadedObjects[i].name  +" is null");
            }
            item.GetComponent<Button>().onClick.AddListener(() => {
                var data = GameObject.Instantiate(loadedObjects[value]);
                
            });
        }
    }
}
