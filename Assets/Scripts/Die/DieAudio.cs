using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollTheDie.Die
{
    /// <summary>
    /// Play audio when the die contact the desktop
    /// </summary>
    public class DieAudio : MonoBehaviour
    {
        [SerializeField] float maxVolumeVelocity = 700f;
        [SerializeField] Rigidbody dieRigidbody;
        private AudioSource audioSource;
        private int contactsCount;


        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            contactsCount = 0;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (audioSource != null)
            {
                float volume = CalculateVolume();
                AudioSource.PlayClipAtPoint(audioSource.clip, transform.position, volume);
            }
            contactsCount = collision.contactCount;
        }

        private void OnCollisionStay(Collision collision)
        {
            if(contactsCount < collision.contactCount && audioSource != null)
            {
                float volume = CalculateVolume();
                AudioSource.PlayClipAtPoint(audioSource.clip, transform.position, volume);
            }
            contactsCount = collision.contactCount;
        }

        private float CalculateVolume()
        {
            if (dieRigidbody == null)
                return 1f;
            return dieRigidbody.velocity.magnitude / maxVolumeVelocity;
        }
    }

}
