using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// unity内部资源
/// </summary>
public class BuiltInResourcesWindow : EditorWindow
{
    private static BuiltInResourcesWindow _instance = null;
    static string[] text;
    [MenuItem("Tools/Unity默认资源")]
    public static void ShowWindow()
    {
        if (_instance == null)
        {
            _instance = (BuiltInResourcesWindow)EditorWindow.GetWindow(typeof(BuiltInResourcesWindow));
            _instance.titleContent = new GUIContent("内置资源");
            _instance.Show();
        }
        TextAsset _text = EditorGUIUtility.Load("DefaultIcon.txt") as TextAsset;
        text = _text.text.Split("\n"[0]);
        _instance.Focus();
    }
    public Vector2 scrollPosition;
    void OnGUI()
    {

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        int LineNumber = 3;
        //内置图标
        for (int i = 0; i < text.Length; i += LineNumber)
        {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < LineNumber; j++)
            {
                int index = i + j;
                if (index < text.Length)
                {
                    GUILayout.BeginVertical();
                    GUILayout.Label(text[index]);
                    GUILayout.Button(EditorGUIUtility.IconContent(text[index]), GUILayout.Width(50), GUILayout.Height(30));
                    GUILayout.EndVertical();
                }
            }
            GUILayout.EndHorizontal();

        }
        //鼠标放在按钮上的样式
        foreach (MouseCursor item in Enum.GetValues(typeof(MouseCursor)))
        {
            GUILayout.Button(Enum.GetName(typeof(MouseCursor), item));
            EditorGUIUtility.AddCursorRect(GUILayoutUtility.GetLastRect(), item);
            GUILayout.Space(10);
        }


        GUILayout.EndScrollView();
    }
}