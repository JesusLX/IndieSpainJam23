using isj23.ParticlesPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Curtain : MonoBehaviour {
    public ParticleSystem ps;
    public UnityEvent onPartycleSystemStopped = new UnityEvent();

    public void Play() {
        ps.Play();
    }

    private void OnParticleSystemStopped() {
        onPartycleSystemStopped?.Invoke();
    }
}
