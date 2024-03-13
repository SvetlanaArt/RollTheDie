using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using System;
using System.Collections;

namespace RollTheDie.Die
{
    /// <summary>
    /// Pick up the Die and roll it with the given force
    /// </summary>
    public class Roller : MonoBehaviour
    {
        [SerializeField] private GameObject die;
        [Header("Pick up")]
        [SerializeField] private float pickUpPositionY = 2f;
        [SerializeField] private float pickUpDuration = 0.5f;
        [Header("Roll")]
        [SerializeField] private float defaultRollingForce = 200f;
        [SerializeField] private float minResultedForce = 150;
        [SerializeField] private float maxForce = 800f;
        [SerializeField] private float maxTorque;

        public event Action OnPickUp;
        public event Action OnStopRolling;
        public event Action OnNoRolling;
        public event Action OnStartRolling;

        private Rigidbody rigidbodyDie;
        // die position on the table before it starts moving
        private Vector3 tablePosition;

        private void Start()
        {
            rigidbodyDie = die.GetComponent<Rigidbody>();
            InitRolling();
        }

        public void InitRolling()
        {
            rigidbodyDie.useGravity = false;
            tablePosition = die.transform.position;
        }

        public async void Roll()
        {
            await PickingUp();
            Vector3 force = GetDefaultForce();
            StartRolling(force);
        }

        private Vector3 GetDefaultForce()
        {
            Vector2 force = UnityEngine.Random.insideUnitCircle.normalized *
                                    defaultRollingForce;
            // change vector orientation to 3D surface of Desctop
            return new Vector3(force.x, 0, force.y);
        }

        public async Task PickingUp()
        {
            OnPickUp();
            if (!Mathf.Approximately(die.transform.position.y, pickUpPositionY))
            {
                Vector3 upPosition = die.transform.position;
                upPosition.y += pickUpPositionY;
                // animation picking up die
                die.transform.DOMove(upPosition, pickUpDuration)
                            .SetEase(Ease.OutBack);
                // delay until animation ends
                await Task.Delay(TimeSpan.FromSeconds(pickUpDuration));
            }
        }


        public void StartRolling(Vector3 force)
        {
            if (force.magnitude < minResultedForce)
            {
                ReturnPosition();
                return;
            }
            OnStartRolling();
            rigidbodyDie.useGravity = true;
            // limit the rolling force
            force = Vector3.Min(force, maxForce * Vector3.one);
            force = Vector3.Max(force, -maxForce * Vector3.one);
            rigidbodyDie.AddForce(force);
            // add random torque
            rigidbodyDie.AddTorque(UnityEngine.Random.insideUnitSphere * maxTorque);
            StartCoroutine(Rolling());
        }

        private void ReturnPosition()
        {
            die.transform.DOMove(tablePosition, pickUpDuration)
                .SetEase(Ease.InBack).OnComplete(NoRolling);
        }

        private void NoRolling()
        {
            InitRolling();
            OnNoRolling();
        }

        private IEnumerator Rolling()
        {
            while (!rigidbodyDie.IsSleeping())
            {
                yield return null;
            }
            OnStopRolling();
            InitRolling();
        }

    }

}
