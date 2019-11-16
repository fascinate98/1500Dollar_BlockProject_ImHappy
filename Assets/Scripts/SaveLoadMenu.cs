using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadMenu : MonoBehaviour
{
    public Text Title;
    public Text SaveLoadBtnText;
    public GameObject content;
    public GameObject itemPrefab;
    public InputField inputField;
    static List<string> GetShellArray(string arrayData)
    {
        List<string> shellList = new List<string>();
        string writeBuffer = null;
        for (int i = 0; i < arrayData.Length; i++)
        {
            if (arrayData[i] == '(' || arrayData[i] == '[')
            {
                for (int j = i + 1; j < arrayData.Length; j++)
                {
                    if (arrayData[j] == ')' || arrayData[j] == ']') { i = j + 1; break; }
                    writeBuffer += arrayData[j];
                }
                shellList.Add(writeBuffer);
                writeBuffer = null;
            }
        }

        return shellList;
    }


    public bool isFileValid()
    {

        return true;
    }

    public void Action()
    {
        if (!IsLoad) 
            Save();
        if (IsLoad)
            Load();
    }
    public void Save()
    {
        string values = null;
        foreach (var data in ShapeManager.Instance.dc)
        {
            var start = "(";
            var x = $"x={data.transform.position.x.ToString("0.00")},";
            var y = $"y={data.transform.position.y.ToString("0.00")},";
            var z = $"z={data.transform.position.z.ToString("0.00")},";
            var name = $"name={data.name}";
            var end = "),";
            values += (start + x + y + z + name + end);
        }

        if (values != null)
        {
            if ((values.Length != 0))
            {
                if (values[values.Length - 1] == ',')
                {
                    values = values.Remove(values.Length - 1, 1);
                    string fileName = inputField.text;
                    System.IO.File.WriteAllText(Application.persistentDataPath + "/" + fileName + ".txt", values);
                    LoadFileList();
                }
            }
        }
    }

    public void Delete()
    {
        var path = Application.persistentDataPath + "/" + (inputField.text);
        if (System.IO.File.Exists(path) == true)
        {
            System.IO.File.Delete(path);
            LoadFileList();
        }
    }
    public void Load()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/" + (inputField.text)) == false)
        {
            return; 
        }

        ShapeManager.Instance.ClearAllShapeObject();
        var loadedFile = System.IO.File.ReadAllText(Application.persistentDataPath + "/" + (inputField.text));
        float x = 0, y = 0, z = 0;
        string name = null;
        var list = GetShellArray(loadedFile);
        foreach (var data in list)
        {
            Debug.Log(data);
            var loadedSplit = data.Split(',');
            foreach (var loadedData in loadedSplit)
            {
                var spliting = loadedData.Split('=');
                if (spliting[0] == "x")
                {
                    x = float.Parse(spliting[1]);
                }
                if (spliting[0] == "y")
                {
                    y = float.Parse(spliting[1]);
                }
                if (spliting[0] == "z")
                {
                    z = float.Parse(spliting[1]);
                }
                if (spliting[0] == "name")
                {
                    name = (spliting[1]);
                }
            }
            var block = Resources.Load("Prefabs/" + name) as GameObject;
            var instant = Instantiate(block);
            instant.transform.position = new Vector3(x, y, z);
            instant.name = block.name;
        }
    }
    private void Awake()
    {
        LoadFileList();
    }
    public void LoadFileList()
    {
        var di = new System.IO.DirectoryInfo(Application.persistentDataPath);


        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
        foreach (var data in di.GetFiles())
        {
            var str = data.Name;
            var createInstance = GameObject.Instantiate(itemPrefab);
            createInstance.transform.SetParent(content.transform, false);
            createInstance.name = str;
            createInstance.GetComponent<SaveItem>().slMenu = this;
            createInstance.GetComponent<SaveItem>().SetFileName(str);
        }
    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }

    public bool IsLoad = false;
    public void Open(bool isLoad)
    {
        if (!isLoad)
        {
            Title.text = "Save Your Block";
            SaveLoadBtnText.text = "Save";
        }
        if (isLoad)
        {
            Title.text = "Load Your Block";
            SaveLoadBtnText.text = "Load";
        }
        this.IsLoad = isLoad;
        this.gameObject.SetActive(true);
        LoadFileList();
    }
}
