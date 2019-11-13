using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    
    private void Update()//안대니? 웅.. 
    {

        // UI는 transform.position을  쓰지않아 잘대던대..?  그래..?
        //좌표개념자체가  트,ㄹ려서그래

        //rectTransform안에 anchoredPosition이란걸로 바꿔보렴!
        //그리고 UI는 x,y만있지?웅.,. .그래서 백터2써야해
        //다시바꺼보아 
        var rectTransform = this.transform as RectTransform;
        if (isWindowOpen)
        {
            Vector2 newvec = new Vector2(-629.5f, rectTransform.anchoredPosition.y);
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, newvec, speed);
        }
        else
        {
            Vector2 newvec = new Vector2(-1306, rectTransform.anchoredPosition.y);
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

            });
        }
    }
}
