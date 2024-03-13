using RollTheDie.Desktop;
using RollTheDie.Die;
using UnityEngine;

namespace RollTheDie.Controllers
{
    /// <summary>
    /// Control die rolling process
    /// </summary>
    [RequireComponent(typeof(Roller))]
    public class RollingController : MonoBehaviour
    {
        [SerializeField] DragDrop dragDropChecker;
        [SerializeField] Result result;

        private Border[] borders;

        private Roller roller;

        void Start()
        {
            InitComponents();
            InitEvents();
            InitBorders();
        }

        private void InitBorders()
        {
            borders = FindObjectsOfType<Border>();
            foreach (var border in borders)
            {
                roller.OnStartRolling += border.Enable;
                roller.OnStopRolling += border.Disable;
            }
        }

        private void InitEvents()
        {
            roller.OnPickUp += OnPickUp;
            roller.OnStopRolling += OnStopRolling;
            roller.OnNoRolling += OnNoRolling;
            dragDropChecker.OnStartDragging += OnStartDragging;
            dragDropChecker.OnDrop += OnDrop;
        }

        private void InitComponents()
        {
            roller = GetComponent<Roller>();
        }

        private void OnPickUp()
        {
            result.Wait();
        }

        private void OnStopRolling()
        {
            result.Find();
            dragDropChecker.EnableDrag();
        }

        private void OnNoRolling()
        {
            result.Non();
            dragDropChecker.EnableDrag();
        }

        private void OnStartDragging()
        {
            _ = roller.PickingUp();
        }

        private void OnDrop()
        {
            roller.StartRolling(dragDropChecker.GetDragForce());
        }

        public void RollByButton()
        {
            dragDropChecker.DisableDrag();
            roller.Roll();
        }

    }

}
