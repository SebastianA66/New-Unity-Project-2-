using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoHome
{
    public class Enemy : MonoBehaviour
    {
        public Transform waypointParent;
        public float moveSpeed = 2f;
        public float stoppingDistance = 1f;

        // Creates a collection of Transforms
        private Transform[] waypoints;
        private int currentIndex = 1;

        // Use this for initialization
        void Start()
        {
            // Getting children of waypointParent
            waypoints = waypointParent.GetComponentsInChildren<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            Patrol();
        }

        void Patrol()
        {
            Transform point = waypoints[currentIndex];

            float distance = Vector3.Distance(transform.position, point.position);
            if (distance < stoppingDistance)
            {
                // currentIndex = currentIndex + 1
                currentIndex++;
                if (currentIndex >= waypoints.Length)
                {
                    currentIndex = 1;
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, point.position, moveSpeed * Time.deltaTime);
        }
    }
}