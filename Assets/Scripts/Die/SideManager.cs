using System;
using TMPro;
using UnityEngine;

namespace RollTheDie.Die
{
    /// <summary>
    /// Allow to get side value 
    /// </summary>
    public class SideManager : MonoBehaviour
    {
        [SerializeField] TextMeshPro number;

        public int GetNumber()
        {
            string text = number.text.Replace('.',' ');
            return Convert.ToInt32(text);
        }

    }
}
 
