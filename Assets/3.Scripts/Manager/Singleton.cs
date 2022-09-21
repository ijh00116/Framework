using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackTree
{
    public abstract class Monosingleton<T>:MonoBehaviour where T:Monosingleton<T>
    {
        private static T instance;
        public static T Instance
        {
            get 
            { 
                if(instance == null)
                {
                    instance=FindObjectOfType<T>();
                    if (instance == null)
                    {
                        instance = new GameObject(typeof(T).Name).AddComponent<T>();
                    }
                    else
                    {
                        instance.Init();
                    }
                }
                

                
                return instance;
            }
            
        }

        protected virtual void Awake()
        {
            DontDestroyOnLoad(gameObject);

            if (instance == null)
            {
                instance = this as T;
                instance.Init();
            }
            else
            {
                DestroyImmediate(instance);
            }
        }

        protected virtual void Init()
        {
            
        }
    }

    public class Singleton<T> where T : new()
    {
        private static System.Lazy<T> lazy = new System.Lazy<T>(() => new T());

        public static T Instance { get { return lazy.Value; } }

        protected Singleton()
        {
        }

    }
}
