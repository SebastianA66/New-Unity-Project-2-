using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{

    public class Spawner : MonoBehaviour
    {
        // List of groups used in the game
        public GameObject[] groups;
        // Reference to the next element that spawns
        public int nextIndex = 0;
        // Spawn the next random group element
        public void SpawnNext()
        {
            // Check if the game is not over
            // Spawn the next group
            Instantiate(groups[nextIndex], transform.position, Quaternion.identity);
            // Get the next index randomly
            nextIndex = Random.Range(0, groups.Length);
            // Remove any empty parents
        }
        // Use this for initialization
        void Start()
        {
            // Run initial spawn
            SpawnNext();
        }

        
    }
}
