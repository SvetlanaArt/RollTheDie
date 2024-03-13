using System;
using UnityEngine;

namespace RollTheDie.Die
{
    /// <summary>
    /// Find a side on the top of the die and calculate result
    /// </summary>
    public class Result : MonoBehaviour
    {
        [SerializeField] LayerMask layerMask;

        public event Action<int, int> OnGetNewResult;
        public event Action OnWaitResult;
        public event Action OnNoResult;


        public int Total { get; private set; }

        private void Start()
        {
            Total = 0;
            OnNoResult();
        }

        // using Raycast to find the up side of the die
        public void Find()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.up, out hit, Mathf.Infinity, layerMask))
            {
                SideManager sideManager = hit.collider.gameObject.GetComponent<SideManager>();
                int result = sideManager.GetNumber();
                Total += result;
                OnGetNewResult(result, Total);
            }
        }

        public void Wait()
        {
            OnWaitResult();
        }

        public void Non()
        {
            OnNoResult();
        }

    }

}