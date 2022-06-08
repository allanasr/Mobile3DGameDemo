using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Singleton
{
    public class Singleton<Type> : MonoBehaviour where Type : MonoBehaviour
    {
        public static Type Instance;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = GetComponent<Type>();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}

