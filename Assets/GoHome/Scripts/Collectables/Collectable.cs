using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoHome
{
    public class Collectable : MonoBehaviour
    {
        public int value = 1;
        public void Collect()
        {
            GameManager.Instance.AddScore(value);
            // Destroy self for now - or do animations
            Destroy(gameObject);
        }

    }
}

