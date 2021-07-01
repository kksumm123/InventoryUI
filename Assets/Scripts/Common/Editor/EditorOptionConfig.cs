using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;


public class EditorOptionConfig : EditorWindow
{
    [MenuItem("Tools/Option")]
    static void Init()
    {
        GetWindow(typeof(EditorOptionConfig));
    }

    Vector2 mPos = Vector2.zero;
    string goldText;

    void OnGUI()
    {
        mPos = GUILayout.BeginScrollView(mPos);
        GUILayout.BeginHorizontal();
        {
            goldText = GUILayout.TextField(goldText);
            if (GUILayout.Button("Set Gold"))
            {
                //골드생성하기
                int gold = int.Parse(goldText);
                UserData.SetGold(gold);
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("PlayerPrefs Delete All"))
            {
                if (EditorUtility.DisplayDialog(
                    "경고", "정말 다 지울거야?", "어"))
                {
                    PlayerPrefs.DeleteAll();
                }
            }
        }
        GUILayout.EndHorizontal();

        if (GUILayout.Button("테스트 버튼"))
        {
            Debug.Log("테스트 버튼 누름");
        }

        for (OptionType i = OptionType.StartIndex + 1; i < OptionType.LastIndex; i++)
        {
            GUILayout.BeginHorizontal();
            {
                bool tempBool = EditorOption.Options[i];

                EditorOption.Options[i] = GUILayout.Toggle(EditorOption.Options[i], i.ToString());

                if (tempBool != EditorOption.Options[i])
                {
                    string key = "DevOption_" + i;
                    EditorPrefs.SetInt(key, EditorOption.Options[i] == true ? 1 : 0);
                }
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndScrollView();
    }
}