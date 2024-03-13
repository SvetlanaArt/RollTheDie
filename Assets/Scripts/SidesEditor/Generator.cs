using System;
using System.Collections.Generic;
using RollTheDie.Die;
using TMPro;
using UnityEditor;
using UnityEngine;


namespace RollTheDie.SidesEditor
{
    /// <summary>
    /// Generate sides in Editor mode
    /// </summary>
    public class Generator : MonoBehaviour
    {
        [SerializeField] ValuesUpdate values;
        [SerializeField] string valueObjectPath = "Assets/Scripts/SidesEditor/SideValues/NewSideValues.asset";
        [SerializeField] GameObject prefabSide;
        [SerializeField] float sideThickness = 0.01f;
        [SerializeField] bool newValuesGenerate;

        private Collider sidesCollider;

        /// <summary>
        /// Finds the normal of the cube, determines the side position and 
        /// places objects there
        /// </summary>
        public void GenerateSides()
        {
            if (values == null)
                values = NewValuesObject();
            DeletePreviousSides();
            sidesCollider = GetComponent<Collider>();
            if (sidesCollider is MeshCollider)
            {
                MeshCollider meshCollider = (MeshCollider)sidesCollider;
                List<Vector3> normalSides = FindNormals(meshCollider);
                float ratio = meshCollider.bounds.size.x;
                for (int i = 0; i < normalSides.Count; i++)
                {
                    Vector3 sidePosition = FindPosition(meshCollider, normalSides[i] * ratio);
                    CreateSide(normalSides[i], sidePosition, i, ratio);
                }
            }
        }

        //
        private ValuesUpdate NewValuesObject()
        {
            ValuesUpdate valuesObject = ScriptableObject.CreateInstance<ValuesUpdate>();
            UnityEditor.AssetDatabase.CreateAsset(valuesObject, valueObjectPath);
            UnityEditor.AssetDatabase.SaveAssets();
            // Set object as changed
            UnityEditor.EditorUtility.SetDirty(valuesObject);
            return valuesObject;
        }

        private List<Vector3> FindNormals(MeshCollider meshCollider)
        {
            Mesh mesh = meshCollider.sharedMesh;
            if (mesh == null)
            {
                return null;
            }
            Vector3[] normals = mesh.normals;
            return extractNonRepeatedElemets(normals);
        }

        private void DeletePreviousSides()
        {
            SideManager[] sides = GetComponentsInChildren<SideManager>();
            foreach (SideManager side in sides)
            {
                DestroyImmediate(side.gameObject);
            }
            values.Clear(!newValuesGenerate);
        }

        // Create GameObject from prefab in sidePosition
        private void CreateSide(Vector3 normal, Vector3 sidePosition, int number, float size)
        {
            Quaternion targetRotation = Quaternion.LookRotation(-normal, Vector3.up);
            GameObject newObject = CreateChildFromPrefab(prefabSide, sidePosition, targetRotation);          if (newObject != null)
            {
                newObject.name = prefabSide.name + " " + number;
                // make collider size enough to get game result
                BoxCollider collider = newObject.GetComponent<BoxCollider>();
                collider.size = new Vector3(size, size, sideThickness);
                SetSideValue(newObject, number);
            }
        }

        // set and save value to edit from the Inspector
        private void SetSideValue(GameObject side, int value)
        {
            TextMeshPro textMeshPro = side.GetComponentInChildren<TextMeshPro>();
            // hide textMeshPro to protect against accidental changes
            textMeshPro.hideFlags = HideFlags.NotEditable;
             values.Add(side.name, value + 1, textMeshPro);
        }

        private GameObject CreateChildFromPrefab(GameObject prefab, Vector3 sidePosition, Quaternion targetRotation)
        {
            GameObject newObject = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            if (newObject == null)
                return null;
            newObject.transform.position = sidePosition;
            newObject.transform.rotation = targetRotation;
            newObject.transform.SetParent(transform);
            return newObject;
        }

        private Vector3 FindPosition(MeshCollider collider, Vector3 normalRay)
        {
            Vector3 point = normalRay + transform.position;
            Vector3 sidePosition = collider.ClosestPoint(point);
            return sidePosition;
        }

        private List<Vector3> extractNonRepeatedElemets(Vector3[] elements)
        {
            List<Vector3> nonRepeatedElemets = new List<Vector3>();
            List<Vector3> normalRoundedSides = new List<Vector3>();
            Vector3 roundedNormal;
            foreach (Vector3 element in elements)
            {
                roundedNormal = element.RoundTo(2);
                if (!normalRoundedSides.Contains(roundedNormal))
                {
                    normalRoundedSides.Add(roundedNormal);
                    nonRepeatedElemets.Add(element);
                }
            }
            return nonRepeatedElemets;
        }
    }

}
