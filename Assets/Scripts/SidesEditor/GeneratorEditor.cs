using UnityEditor;
using UnityEngine;

namespace RollTheDie.SidesEditor
{
  /// <summary>
  /// Add button to the Inspector to generate new sides
  /// </summary>
  [CustomEditor(typeof(Generator))]
  public class GeneratorEditor : Editor
  {

    public override void OnInspectorGUI()
    {
      DrawDefaultInspector();
      Generator sideGenerator = (Generator)target;

      if (GUILayout.Button("Regenerate Sides"))
      {
        sideGenerator.GenerateSides();
      }
      serializedObject.Update();

    }
  }

}
