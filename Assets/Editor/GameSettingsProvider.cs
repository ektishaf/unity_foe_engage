using UnityEditor;
using UnityEngine;

static class GameSettingsProvider
{
    [SettingsProvider]
    public static SettingsProvider CreateProvider()
    {
        var provider = new SettingsProvider("Project/Foe Engage", SettingsScope.Project)
        {
            guiHandler = (searchContext) =>
            {
                var settings = GameSettingsLoader.Settings;
                if (settings == null)
                {
                    EditorGUILayout.HelpBox("GameSettings asset not found.", MessageType.Error);
                    return;
                }

                SerializedObject so = new SerializedObject(settings);
                EditorGUILayout.PropertyField(so.FindProperty("gridWidth"));
                EditorGUILayout.PropertyField(so.FindProperty("gridHeight"));
                EditorGUILayout.PropertyField(so.FindProperty("cellSize"));
                EditorGUILayout.PropertyField(so.FindProperty("cellPrefab"));

                EditorGUILayout.PropertyField(so.FindProperty("cursorCellPrefab"));

                EditorGUILayout.PropertyField(so.FindProperty("moveCellPrefab"));
                EditorGUILayout.PropertyField(so.FindProperty("moveAnimDuration"));

                EditorGUILayout.PropertyField(so.FindProperty("attackCellPrefab"));

                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(so.FindProperty("defaultUnitPrefab"));
                EditorGUILayout.PropertyField(so.FindProperty("defaultMovementPoints"));
                EditorGUILayout.PropertyField(so.FindProperty("defaultAttackRange"));
                EditorGUILayout.PropertyField(so.FindProperty("defaultVisionRange"));

                EditorGUILayout.PropertyField(so.FindProperty("fogCellPrefab"));

                EditorGUILayout.PropertyField(so.FindProperty("unitsPerTeam"));

                EditorGUILayout.PropertyField(so.FindProperty("selectKey"));
                EditorGUILayout.PropertyField(so.FindProperty("moveKey"));

                so.ApplyModifiedProperties();
            }
        };

        return provider;
    }
}
