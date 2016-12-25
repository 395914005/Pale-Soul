using System;
using UnityEngine;

public class Singleton<T_Behavior> : MonoBehaviour where T_Behavior : MonoBehaviour
{
    protected static T_Behavior _instance;

    public virtual void AwakeSingleton()
    {
    }

    public static T_Behavior Instance
    {
        get
        {
            if (Singleton<T_Behavior>._instance == null)
            {
                Singleton<T_Behavior>._instance = GameObject.FindObjectOfType<T_Behavior>();
                //(Singleton<T_Behavior>._instance as Singleton<T_Behavior>).AwakeSingleton();
            }
            if (Singleton<T_Behavior>._instance == null)
            {
                My_Debug.LogError(string.Format("An instance of type {0} is needed in the scene, but none was found.",
                    typeof(T_Behavior)));

                return null;
            }
            return Singleton<T_Behavior>._instance;
        }
    }
}
