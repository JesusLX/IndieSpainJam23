using DG.Tweening;
using isj23.ParticlesPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour, ILight {
    public float flickerIntesity = .2f;
    public float flickersPerSecond = 3f;
    public float speedRandomness = 1f;

    private float time;
    private float startingIntesity;
    private Light light;
    private float range;

    public Transform Transform => this.transform;

    private void Awake() {
        light = GetComponent<Light>();
        range = light.range;
        startingIntesity = light.intensity;
    }
    public void Turn(bool on_off) {

        if(light.enabled = (on_off)) {
            AnimatedTurnOn();
        }
    }
    public bool IsOn() {
        return light.enabled;
    }
    private void Update() {
        time += Time.deltaTime * (1 - Random.Range(-speedRandomness, speedRandomness)) * Mathf.PI;
        light.intensity = startingIntesity + Mathf.Sin(time * flickersPerSecond) * flickerIntesity;
    }

    private void AnimatedTurnOn() {
        light.range = 0;
        // Crea un Tween que va desde 0 hasta el rangoMaximo
        DOTween.To(() => light.range, x => light.range = x, range, .5f)
            .SetEase(Ease.OutQuad) // Puedes ajustar la curva de easing según tus necesidades
            .OnUpdate(() => {
                // Opcional: Puedes realizar acciones adicionales durante el Tween
                float valorActual = light.range;
            });
        AudioManager.Instance.Play("light_on", true, true);
    }

}
