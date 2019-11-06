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

        serializedObject.ApplyModifiedProperties();
    }
}
