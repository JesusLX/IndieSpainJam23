using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

    public static T instance { get; private set; }

    protected virtual void Awake() {
        if (instance != null) {
            Debug.LogError("A instance already exists");
            Destroy(this); //Or GameObject as appropriate
            return;
        }
        instance = this.GetComponent<T>();
    }
}