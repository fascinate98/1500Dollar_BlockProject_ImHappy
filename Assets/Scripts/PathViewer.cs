#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class PathViewer
{

    [MenuItem("HamsterLibs/Tools/App/Show All")]
    public static void ShowAll()
    {
        Debug.Log($"<color=red>dataPath</color> {Application.dataPath}");
        Debug.Log($"<color=red>persistentDataPath</color> {Application.persistentDataPath}");
        Debug.Log($"<color=red>streamingAssetsPath</color> {Application.streamingAssetsPath}");
        Debug.Log($"<color=red>temporaryCachePath</color> {Application.temporaryCachePath}");
        Debug.Log($"<color=red>consoleLogPath</color> {Application.consoleLogPath}");
    }
    [MenuItem("HamsterLibs/Tools/App/Show DataPath")]
    public static void ViewDataPath()
    {
        Debug.Log(Application.dataPath);
    }
    [MenuItem("HamsterLibs/Tools/App/Copy DataPath")]
    public static void CopyDP()
    {
        var p = Application.dataPath;
        var clipboard = p;
        var textEditor = new TextEditor();
        textEditor.text = clipboard;
        textEditor.SelectAll();
        textEditor.Copy();
    }

    [MenuItem("HamsterLibs/Tools/App/Show PersistentPath")]

    public static void ViewPersistentPath()
    {
        Debug.Log(Application.persistentDataPath);

    }
    [MenuItem("HamsterLibs/Tools/App/Copy PersistentPath")]
    public static void CoptPP()
    {
        var p = Application.persistentDataPath;
        var clipboard = p;
        var textEditor = new TextEditor();
        textEditor.text = clipboard;
        textEditor.SelectAll();
        textEditor.Copy();
    }


    [MenuItem("HamsterLibs/Tools/App/Show StreamingAssetsPath")]

    public static void ViewStreamingAssetsPath()
    {
        Debug.Log(Application.streamingAssetsPath);

    }
    [MenuItem("HamsterLibs/Tools/App/Copy StreamingAssetsPath")]
    public static void CopySAP()
    {
        var p = Application.streamingAssetsPath;
        var clipboard = p;
        var textEditor = new TextEditor();
        textEditor.text = clipboard;
        textEditor.SelectAll();
        textEditor.Copy();
    }

    [MenuItem("HamsterLibs/Tools/App/Show TemporaryCachePath")]

    public static void ViewTemporaryCachePath()
    {
        Debug.Log(Application.temporaryCachePath);

    }
    [MenuItem("HamsterLibs/Tools/App/Copy TemporaryCachePath")]
    public static void CopyTemporaryCachePath()
    {
        var p = Application.temporaryCachePath;
        var clipboard = p;
        var textEditor = new TextEditor();
        textEditor.text = clipboard;
        textEditor.SelectAll();
        textEditor.Copy();
    }

    [MenuItem("HamsterLibs/Tools/App/Show ConsolLogPath")]

    public static void ViewClog()
    {
        Debug.Log(Application.consoleLogPath);

    }
    [MenuItem("HamsterLibs/Tools/App/Copy ConsolLogPath")]
    public static void CopyClog()
    {
        var p = Application.consoleLogPath;
        var clipboard = p;
        var textEditor = new TextEditor();
        textEditor.text = clipboard;
        textEditor.SelectAll();
        textEditor.Copy();
    }
}
#endif