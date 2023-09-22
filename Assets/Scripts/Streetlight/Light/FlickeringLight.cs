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


    private void Start() {
        light = GetComponent<Light>();
        range = light.range;
        startingIntesity = light.intensity;
    }
    public void Turn(bool on_off) {
        Debug.Log("Encendido"+on_off);

        gameObject.SetActive(on_off);
        if(on_off) {
            AnimatedTurnOn();
        }
    }

    private void Update() {
        time += Time.deltaTime * (1 - Random.Range(-speedRandomness, speedRandomness)) * Mathf.PI;
        light.intensity = startingIntesity + Mathf.Sin(time * flickersPerSecond) * flickerIntesity;
    }

    private void AnimatedTurnOn() {
        light.range = 0;
        PSManager.instance.Play("fire_boom", this.transform, Vector3.zero, Quaternion.identity);
        // Crea un Tween que va desde 0 hasta el rangoMaximo
        DOTween.To(() => light.range, x => light.range = x, range, .5f)
            .SetEase(Ease.OutQuad) // Puedes ajustar la curva de easing según tus necesidades
            .OnUpdate(() => {
                // Opcional: Puedes realizar acciones adicionales durante el Tween
                float valorActual = light.range;
            });
    }

}
