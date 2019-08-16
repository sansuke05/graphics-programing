using UnityEngine;
using UnityEditor;
using System.IO;

public class EditorWindowSample : EditorWindow {

    /// <summary>
    /// ScriptableObjectSampleの変数
    /// </summary>
    private ScriptableObjectSample _sample;

    /// <summary>
    /// アセットパス
    /// </summary>
    private const string ASSET_PATH = "Assets/Resources/ScriptableObjectSample.asset";

    private void OnGUI() {
        if(_sample == null) {
            _sample = ScriptableObject.CreateInstance<ScriptableObjectSample>();
        }

        using (new GUILayout.HorizontalScope()) {
            _sample.SampleIntValue = EditorGUILayout.IntField("数量", _sample.SampleIntValue);
        }
        using (new GUILayout.HorizontalScope()) {
            if (GUILayout.Button("書き込み")) {
                Export();
            }
        }
    }

    private void Export() {
        // 新規の場合は作成
        if (!AssetDatabase.Contains(_sample as UnityEngine.Object)) {
            string directory = Path.GetDirectoryName(ASSET_PATH);
            if (!Directory.Exists(directory)) {
                Directory.CreateDirectory(directory);
            }
            // アセット作成
            AssetDatabase.CreateAsset(_sample, ASSET_PATH);
        }
        // インスペクターから設定できないようにする
        _sample.hideFlags = HideFlags.NotEditable;
        // 更新通知
        EditorUtility.SetDirty(_sample);
        // 保存
        AssetDatabase.SaveAssets();
        // エディタを最新の状態に
        AssetDatabase.Refresh();
    }

    [MenuItem("Editor/Sample")]
    private static void Create() {
        // 生成
        GetWindow<EditorWindowSample>("にうむ物質生成");
    }
}