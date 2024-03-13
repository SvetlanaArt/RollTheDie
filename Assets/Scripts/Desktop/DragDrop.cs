using System;
using System.Collections;
using UnityEngine;

namespace RollTheDie.Desktop
{
    /// <summary>
    /// Fixes mouse dragging events and calculate mouse force
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class DragDrop : MonoBehaviour
    {
        public event Action OnStartDragging;
        public event Action OnDrop;

        private Coroutine dragginCoroutine;

        private Collider checkCollider;

        // helps calculate force of mouse move
        private Vector3 pickUpPosition;
        private Vector3 dropPosition;
        private float timeDragging;

        void Start()
        {
            checkCollider = GetComponent<Collider>();
            EnableDrag();
        }

        public void StartDragging()
        {
            OnStartDragging();
            pickUpPosition = Input.mousePosition;
            dragginCoroutine = StartCoroutine(Dragging());
        }

        public void StopDragging()
        {
            dropPosition = Input.mousePosition;
            StopCoroutine(dragginCoroutine);
            DisableDrag();
            OnDrop();
        }

        private IEnumerator Dragging()
        {
            while (true)
            {
                timeDragging += Time.deltaTime;
                yield return null;
            }
        }

        public Vector3 GetDragForce()
        {
            Vector3 force = (dropPosition - pickUpPosition) / timeDragging;
            // change vector orientation to 3D surface of Desctop
            force.z = force.y;
            force.y = 0;
            return force;
        }

        public void DisableDrag()
        {
            checkCollider.enabled = false;
        }

        public void EnableDrag()
        {
            checkCollider.enabled = true;
            timeDragging = 0;
        }
    }

}

