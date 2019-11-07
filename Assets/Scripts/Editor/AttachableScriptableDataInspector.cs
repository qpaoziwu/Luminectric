using UnityEditor;

[CustomEditor(typeof(AttachableScriptableData))]
public class AttachableScriptableDataInspector : Editor
{
    public override void OnInspectorGUI()
    {
        SerializedProperty itemType = serializedObject.FindProperty("itemType");
        SerializedProperty itemName = serializedObject.FindProperty("itemName");
        SerializedProperty attachValue = serializedObject.FindProperty("attachValue");


        EditorGUILayout.PropertyField(itemType);
        EditorGUILayout.PropertyField(itemName);
        EditorGUILayout.PropertyField(attachValue, true);

        if (itemType.enumValueIndex == 0)
        {
            SerializedProperty source = serializedObject.FindProperty("source");
            EditorGUILayout.PropertyField(source, true);
        }

        if (itemType.enumValueIndex == 1)
        {
            SerializedProperty medium = serializedObject.FindProperty("medium");
            EditorGUILayout.PropertyField(medium, true);
        }

        if (itemType.enumValueIndex == 2)
        {
            SerializedProperty output = serializedObject.FindProperty("output");
            EditorGUILayout.PropertyField(output, true);
        }

        if (itemType.enumValueIndex == 3)
        {
            SerializedProperty shell = serializedObject.FindProperty("shell");
            EditorGUILayout.PropertyField(shell, true);
        }

        if (itemType.enumValueIndex == 4)
        {
            SerializedProperty protocol = serializedObject.FindProperty("protocol");
            EditorGUILayout.PropertyField(protocol, true);
        }


        serializedObject.ApplyModifiedProperties();
    }
}
