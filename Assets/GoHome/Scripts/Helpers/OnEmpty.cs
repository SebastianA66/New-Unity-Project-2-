using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GoHome
{
    public class OnEmpty : MonoBehaviour
    {
        public UnityEvent onEmpty;
        

        // Update is called once per frame
        void Update()
        {
            // Is there no children left
            if(transform.childCount ==0)
            {
                // Invoke the UnityEvent
                onEmpty.Invoke();
                // Disable gameObject
                gameObject.SetActive(false);
            }
        }
    }
}


