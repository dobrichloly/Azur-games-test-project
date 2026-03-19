using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelController))]
public class LevelControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelController controller = (LevelController)target;

        if (GUILayout.Button("Generate Level"))
        {
            controller.GenerateLevel();
        }

        if (GUILayout.Button("Clear Level"))
        {
            controller.ClearLevel();
        }
    }
}