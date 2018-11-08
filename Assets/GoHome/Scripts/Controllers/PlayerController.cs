using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GoHome
{
    public class PlayerController : MonoBehaviour
    {
        public float speed = 10f;
        public float maxVelocity = 20f;
        public Rigidbody rigid;
        public UnityEvent onDeath;
        public ParticleSystem explosionParticles;
        private Vector3 startPosition;

        private void Start()
        {
            // Record starting position
            startPosition = transform.position;
            // Detatch particles from children
            explosionParticles.transform.SetParent(transform.parent);
        }

        // Constrict the velocity per update
        private void Update()
        {
            Vector3 vel = rigid.velocity;
            if(Mathf.Abs(vel.x) > maxVelocity)
            {
                vel.x = vel.normalized.x * maxVelocity;
            }
            if(Mathf.Abs(vel.z) > maxVelocity)
            {
                vel.z = vel.normalized.z * maxVelocity;
            }
            rigid.velocity = vel;
        }
        // Collect item on trigger enter
        private void OnTriggerEnter(Collider other)
        {
            // Try checking if player hits a killbox
            if(other.name.Contains("KillBox") || other.name.Contains("Enemy"))
            {
                // Then we are gonna die
                Death();
            }
            // Try getting collectable component from other collider
            Collectable collectable = other.GetComponent<Collectable>();
            // If it's not null
            if (collectable)
            {
                // Collect the item
                collectable.Collect();
            }
        }
        // Input method for movement
        public void Move(float inputH, float inputV)
        {
            // Generate direction from input (horizontal and vertical)
            Vector3 direction = new Vector3(inputH, 0, inputV);
            // Set velocity to direction with speed
            Vector3 vel = rigid.velocity;
            vel.x = direction.x * speed;
            vel.z = direction.z * speed;
            rigid.velocity = vel;
            
        }
        public void Death()
        {
            // Setting position of particles to player pos
            explosionParticles.transform.position = transform.position;
            explosionParticles.Play();
            //Reset transform position to start
            transform.position = startPosition;
            rigid.velocity = Vector3.zero;
            onDeath.Invoke();

            
        }
    }
}

