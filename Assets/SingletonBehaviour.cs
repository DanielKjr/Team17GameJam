using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;



public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {

            if (instance == null)
            {
                instance = FindObjectOfType<T>() as T;
                if (instance == null)
                {
                    instance = new GameObject().AddComponent<T>();
                    instance.name = instance.GetType().ToString();
                    DontDestroyOnLoad(instance);
                }

            }
            return instance;
        }
    }


    private void Awake()
    {
        if (instance != Instance && instance != null)
        {
            //Destroy(this);
            //throw new SystemException("Duplicate Singleton");
        }
        else
        {
            instance = (T)this;
        }

    }
}

