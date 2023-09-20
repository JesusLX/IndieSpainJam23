using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Countdown {

    public Action OnTimeOut;
    public Action<float> OnTimeUpdate;

    private float remainingTime;
    private bool isCountingDown;

    public Countdown(float time) {
        remainingTime = time;
    }

    public IEnumerator StartCountdown() {
        isCountingDown = true;
        return UpdateCorroutine();
    }

    public void StopCountdown() {
        isCountingDown = false;
    }

    private void Update(float deltaTime) {
        if (isCountingDown) {
            Debug.Log(remainingTime);
            remainingTime -= deltaTime;
            OnTimeUpdate?.Invoke(remainingTime);
        
            if (remainingTime <= 0) {
                StopCountdown();
                remainingTime = 0;
            }
        }
    }
    public IEnumerator UpdateCorroutine() {
        while (isCountingDown) {
            if (isCountingDown) {
                Update(Time.deltaTime);
            }
            yield return new WaitForEndOfFrame();
        }
        if(remainingTime <= 0) {
            OnTimeOut?.Invoke();
        }
    }

    public bool IsCountingDown() {
        return isCountingDown;
    }

    public float GetRemainingTime() {
        return remainingTime;
    }
}
