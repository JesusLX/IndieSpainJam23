using isj23.ParticlesPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Streetlight : MonoBehaviour, IStreetlight {
    private UnityEvent<IStreetlight, bool> onStreetlightTurn = new UnityEvent<IStreetlight, bool>();

    internal UnityEvent<IStreetlight, bool> OnStreetlightTurn { get => onStreetlightTurn; set => onStreetlightTurn = value; }
    private ILight _light;
    private ILighter lighter;
    public string turnOnParticles = "fire_boom";
    private void Start() {
        _light = GetComponentInChildren<ILight>(true);
        lighter = GetComponentInChildren<ILighter>(true);
    }
    [ContextMenu("TurnOn")]
    public void TurnOn() {
        if (!_light.IsOn()) {
        PSManager.instance.Play(turnOnParticles, _light.Transform, Vector3.zero, Quaternion.identity);
        _light.Turn(true);
        }
    }
    [ContextMenu("TurnOff")]
    public void TurnOff() {
        _light.Turn(false);
    }
}
