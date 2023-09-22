using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLifeController : MonoBehaviour {

    private float startingIntesity;
    private Light light;
    public float startingSeconds = 100;
    public float remainingSeconds;
    public float addtionalSeconds = 2f;


    private void Start() {
        light = GetComponent<Light>();
        startingIntesity = light.intensity;
        remainingSeconds = startingSeconds;
    }

    private void Update() {
        if(remainingSeconds > 0) {
            UpdateLightTime();
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            AddLightTime();
        }

    }

    private void UpdateLightTime() {
        remainingSeconds -= Time.deltaTime;

        float timeElapsed = startingSeconds - remainingSeconds;
        float lightPercentage = 1 - timeElapsed / startingSeconds;

        light.intensity = startingIntesity * lightPercentage;
    }

    private void AddLightTime() {
        if(remainingSeconds < startingSeconds) {
            remainingSeconds += addtionalSeconds;
        }
    }

}