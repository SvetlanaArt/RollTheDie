using UnityEditor;
using UnityEngine;

namespace RollTheDie.SidesEditor
{
    /// <summary>
    /// Show in the Inspector side values to edit
    /// </summary>
    [CustomEditor(typeof(ValuesUpdate))]
    public class ValuesUpdateEditor : Editor
    {
        SerializedProperty m_sideValues;

        void OnEnable()
        {
            m_sideValues = serializedObject.FindProperty("values");
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            ValuesUpdate sideValues = (ValuesUpdate)target;

            ShowEditableValues(sideValues);
            serializedObject.ApplyModifiedProperties();

            if (GUI.changed)
            {
                sideValues.UpdateText();
                UnityEditor.EditorUtility.SetDirty(sideValues);
            }

            serializedObject.Update();

        }

        private void ShowEditableValues(ValuesUpdate values)
        {
            m_sideValues.isExpanded = EditorGUILayout.Foldout(m_sideValues.isExpanded, new GUIContent("Side velues"));
            if (m_sideValues.isExpanded)
            {
                EditorGUI.indentLevel++;

                // The field for item count
                EditorGUILayout.LabelField("Count of sides: " + m_sideValues.arraySize);

                // draw item fields
                for (var i = 0; i < m_sideValues.arraySize; i++)
                {
                    var item = m_sideValues.GetArrayElementAtIndex(i);
                    EditorGUILayout.PropertyField(item, new GUIContent($"{values.GetSideName(i)}"));
                }

                EditorGUI.indentLevel--;
            }
        }
    }
}

  