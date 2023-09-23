using isj23;
using isj23.Managers;
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

    public void Init() {
        Debug.Log("inicio");
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

            if(remainingSeconds <= 0) {
                onLightOff?.Invoke();
            }
        }
    }

    public void AddLightTime() {
        if (remainingSeconds < startingSeconds) {
            remainingSeconds += addtionalSeconds;
        }
    }

    #region ITimeAffected
    public void OnPlayTimeStarts() {
        Init();
        timeRunning = true;
    }

    public void OnPlayTimeRestore() {
        timeRunning = true;
    }

    public void OnPlayTimeStops() {
        timeRunning = false;
    }

    public void AttachTimeEvents() {
        Debug.Log("Atacjando");
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