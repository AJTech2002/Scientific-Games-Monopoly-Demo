using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T _instance = null;
    public static T Instance
    {
        get
        {
            if (!_instance)
                _instance = GameObject.FindObjectsOfType<T>().FirstOrDefault();
            return _instance;
        }
    }
}