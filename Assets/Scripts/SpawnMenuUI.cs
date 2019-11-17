using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RuntimeGizmos;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class SpawnMenuUI : MonoBehaviour
{ 
    public List<GameObject> loadedObjects = new List<GameObject>();
    public Transform Content;
    public GameObject SpawnUIItemPrefab;
    public GameObject window;
    public float speed = 0.15f;
    public bool isWindowOpen;
  
    public void LoadPrefabs()
    {
        var _loadedObjects = Resources.LoadAll("Prefabs");
        foreach (var data in _loadedObjects)
        {
            var go = data as GameObject;
            loadedObjects.Add(go);
        }
    }

    public void windowButton()
    {
        //현재 true면 false로, false면 true로전환 
        isWindowOpen = !isWindowOpen;
    }



    
    private void Update()
    {
        var rectTransform = this.transform as RectTransform;
        if (isWindowOpen)
        {
            Vector2 newvec = new Vector2(336f, rectTransform.anchoredPosition.y);
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, newvec, speed);
        }
        else
        {
            Vector2 newvec = new Vector2(-336f, rectTransform.anchoredPosition.y);
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, newvec, speed);
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
            if (png != null)
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
        for (int i = 0; i < Content.childCount; i++)
        {
            var c = Content.transform.GetChild(i);
            Destroy(c.gameObject);
        }
        for (int i = 0; i < loadedObjects.Count; i++)
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
                Debug.Log("PreviewPNG/" + loadedObjects[i].name + " is null");
            }
            item.GetComponent<Button>().onClick.AddListener(() =>
            {
                  var data = GameObject.Instantiate(loadedObjects[value]);
                  data.name = loadedObjects[value].name; 
                  data.transform.position = (ShapeManager.Instance.dc.Count  == 0 || ShapeManager.Instance.dc.Count == 1) ? Vector3.zero : ShapeManager.Instance.dc[ShapeManager.Instance.dc.Count - 2].transform.position;
                   FindObjectOfType<TransformGizmo>().ClearTargets(data.transform);
                   FindObjectOfType<TransformGizmo>().AddTarget(data.transform);

            });
        }
    }
}
