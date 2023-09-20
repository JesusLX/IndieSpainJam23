using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public float flickerIntesity = .2f;
    public float flickersPerSecond = 3f;
    public float speedRandomness = 1f;

    private float time;
    private float startingIntesity;
    private Light light;


    private void Start() {
        light = GetComponent<Light>();
        startingIntesity = light.intensity;
    }

    private void Update() {
        time += Time.deltaTime * (1 - Random.Range(-speedRandomness, speedRandomness)) * Mathf.PI;
        light.intensity = startingIntesity + Mathf.Sin(time * flickersPerSecond) * flickerIntesity;
    }

}
