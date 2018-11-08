﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GoHome
{
    public class OnGoal : MonoBehaviour
    {
        public UnityEvent onGoal;

        private void OnTriggerEnter(Collider other)
        {
            // Run event on trigger enter
            onGoal.Invoke();
        }

    }

}
