using UnityEngine;

namespace RollTheDie.Desktop
{
    /// <summary>
    /// Manage border collider to be disable when game is waiting mouse click
    /// and to be enable when the die is rolling
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class Border : MonoBehaviour
    {
        private Collider wallCollider;

        void Start()
        {
            wallCollider = GetComponent<Collider>();
            Disable();
        }

        public void Enable()
        {
            wallCollider.enabled = true;
        }

        public void Disable()
        {
            wallCollider.enabled = false;
        }
    }
}
