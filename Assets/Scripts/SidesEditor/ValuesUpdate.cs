using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEditor;

namespace RollTheDie.SidesEditor
{
    /// <summary>
    /// Contain data about sides
    /// </summary>
    [CreateAssetMenu(fileName = "NewSideValues", menuName = "ScriptableObjects/SIdeValues", order = 1)]

    public class ValuesUpdate : ScriptableObject
    {

        [SerializeField][HideInInspector] List<int> values = new List<int>();
        [SerializeField][HideInInspector] List<string> names = new List<string>();
        [SerializeField][HideInInspector] List<TextMeshPro> texts = new List<TextMeshPro>();

        public void Add(string name, int value, TextMeshPro text)
        {
            if (text == null)
                return;
            // if any data are not available
            if (values == null || names == null || texts == null)
            {
                values = new List<int>();
                names = new List<string>();
                texts = new List<TextMeshPro>();
            }
            if (values.Count < value)
            {
                text.text = ValueToText(value);
                names.Add(name);
                values.Add(value);
            }
            else
            {
                text.text = ValueToText(values[value - 1]);
            }
            texts.Add(text);
        }

        // Update text values in TextMeshPro components
        public void UpdateText()
        {
            for (int i = 0; i < texts.Count && i < values.Count; i++)
            {
                texts[i].text = ValueToText(values[i]);
                EditorUtility.SetDirty(texts[i]);

            }
        }

        public void Clear(bool onlyTexts)
        {
            texts.Clear();
            if (onlyTexts)
            {
                return;
            }
            names.Clear();
            values.Clear();
        }

        public string GetSideName(int i)
        {
            if (i < names.Count && i >= 0)
            {
                return names[i];
            }
            return "";
        }

        public int GetValue(int i)
        {
            if (i < values.Count && i >= 0)
            {
                return values[i];
            }
            return 0;
        }

        /// <summary>
        /// Add '.' to numbers from 6 and 9 to distinguish them upside down.
        /// </summary>
        /// <param name="value">number</param>
        /// <returns></returns>
        private string ValueToText(int value)
        {
            string text = value.ToString();
            char[] chars = text.ToCharArray();
            // if text contains oly symbols '6' and '9'
            if (text.All(c => c == '6' || c == '9'))
            {
                text += '.';
            }
            return text;
        }

    }

}
