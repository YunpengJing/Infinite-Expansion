using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace BigRookGames.Weapons
{
    public class ProjectileController : MonoBehaviour
    {
        // --- Config ---
        public float speed = 100;
        public LayerMask collisionLayerMask;

        // --- Explosion VFX ---
        public GameObject rocketExplosion;

        // --- Projectile Mesh ---
        public MeshRenderer projectileMesh;

        // --- Script Variables ---
        private bool targetHit;

        // --- Audio ---
        public AudioSource inFlightAudioSource;

        // --- VFX ---
        public ParticleSystem disableOnHit;

        private GameObject enemy;
        public float currentDamage; // 当前伤害


        void Start()
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy");
        }

        private void Update()
        {
            // --- Check to see if the target has been hit. We don't want to update the position if the target was hit ---
            if (targetHit) return;

            // --- moves the game object in the forward direction at the defined speed ---
            // transform.position += transform.forward * (speed * Time.deltaTime);
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);

        }


        /// <summary>
        /// Explodes on contact.
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {

            //if (collision.gameObject.name != "Airwall")
            //{

            // --- return if not enabled because OnCollision is still called if compoenent is disabled ---
            //if (!enabled) return;

            // --- Explode when hitting an object and disable the projectile mesh ---
            Explode();
                projectileMesh.enabled = false;
                targetHit = true;
                inFlightAudioSource.Stop();
                foreach (Collider col in GetComponents<Collider>())
                {
                    col.enabled = false;
                }
                disableOnHit.Stop();


                // --- Destroy this object after 2 seconds. Using a delay because the particle system needs to finish ---
                Destroy(gameObject, 5f);
           // }
        }

        
        private void OnTriggerEnter(Collider other)
        {

            //UnityEngine.Debug.Log(other.tag);

            GameObject hero = GameObject.FindWithTag("Hero");
            if (other.tag == "Enemy")
            {
                other.GetComponent<Enemy>().TakeDamage(currentDamage, hero);
            }

            if (other.tag != "Airwall")
            {
                Explode();
                projectileMesh.enabled = false;
                targetHit = true;
                inFlightAudioSource.Stop();
                foreach (Collider col in GetComponents<Collider>())
                {
                    col.enabled = false;
                }
                disableOnHit.Stop();
                Destroy(gameObject, 5f);


            }
            // --- Destroy this object after 2 seconds. Using a delay because the particle system needs to finish ---
        }
        
        /// <summary>
        /// Instantiates an explode object.
        /// </summary>
        private void Explode()
        {
            // --- Instantiate new explosion option. I would recommend using an object pool ---
            GameObject newExplosion = Instantiate(rocketExplosion, transform.position, rocketExplosion.transform.rotation, null);


        }
    }
}