using isj23;
using isj23.Managers;
using isj23.ParticlesPool;
using isj23.ST;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightLifeController : MonoBehaviour, ITimeAffected {

    private float startingIntesity;
    private Light light;
    public float startingSeconds = 100;
    public float remainingSeconds;
    public float addtionalSeconds = 2f;
    public bool timeRunning = false;
    public UnityEvent onLightOff = new UnityEvent();
    public ParticleSystem firePS;

    private void Start() {
        light = GetComponent<Light>();
        startingIntesity = light.intensity;
    }

    private void OnEnable() {
        AttachTimeEvents();
    }
    private void OnDisable() {
        DetachTimeEvents();
    }

    public void Init(Stats.HealthST health) {
        Debug.Log("inicio");
        startingSeconds = health.MaxHealth;
        remainingSeconds = startingSeconds;
    }

    private void Update() {
        if (remainingSeconds > 0) {
            UpdateLightTime();
        }
    }

    private void UpdateLightTime() {
        if (timeRunning) {

            remainingSeconds -= Time.deltaTime;

            float timeElapsed = startingSeconds - remainingSeconds;
            float lightPercentage = 1 - timeElapsed / startingSeconds;

            light.intensity = startingIntesity * lightPercentage;
            BlendColor(remainingSeconds);
            if (remainingSeconds <= 0) {
                onLightOff?.Invoke();
                firePS.Stop();
                PSManager.instance.Play("puf", null, light.transform.position, Quaternion.identity);
            }
        }
    }

    public void AddLightTime() {
        if (remainingSeconds < startingSeconds) {
            remainingSeconds += addtionalSeconds;
        }
        if (remainingSeconds > startingSeconds) {
            remainingSeconds = startingSeconds;
        }
    }
    public void SubstractLightTime(float time) {
            remainingSeconds -= time;
        if(remainingSeconds <= 0) {
            remainingSeconds = 0.1f;
        }
    }
    public Renderer renderer; // The object's Renderer component
    public Color initialColor; // Initial color in hexadecimal RGB format
    public Color finalColor;   // Final color in hexadecimal RGB format

    private float elapsedTime = 0f;
    private void BlendColor(float elapsedTime) {

        if (elapsedTime >= 0) {
            // Interpolate between the initial and final color
            float t = elapsedTime / startingSeconds;
            Color currentColor = Color.Lerp(finalColor, initialColor, t);

            // Update the material's emission field
            renderer.material.SetColor("_EmissionColor", currentColor);
        } else {
            // When the transition is complete, set the final color
            renderer.material.SetColor("_EmissionColor", finalColor);
        }
    }

    #region ITimeAffected
    public void OnPlayTimeStarts() {
        timeRunning = true;
    }

    public void OnPlayTimeRestore() {
        timeRunning = true;
    }

    public void OnPlayTimeStops() {
        timeRunning = false;
    }

    public void AttachTimeEvents() {
        TimeManager.instance.OnPlayTimeStart.AddListener(OnPlayTimeStarts);
        TimeManager.instance.OnPlayTimeStop.AddListener(OnPlayTimeStops);
        TimeManager.instance.OnPlayTimeRestore.AddListener(OnPlayTimeRestore);
    }

    public void DetachTimeEvents() {
        TimeManager.instance.OnPlayTimeStart.RemoveListener(OnPlayTimeStarts);
        TimeManager.instance.OnPlayTimeStop.RemoveListener(OnPlayTimeStops);
        TimeManager.instance.OnPlayTimeRestore.RemoveListener(OnPlayTimeRestore);
    }

    #endregion
}