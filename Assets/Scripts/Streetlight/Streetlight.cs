using isj23.ParticlesPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Streetlight : MonoBehaviour, IStreetlight {
    private UnityEvent<IStreetlight> onTurnOff = new UnityEvent<IStreetlight>();
    private UnityEvent<IStreetlight> onTurnOn = new UnityEvent<IStreetlight>();
    public UnityEvent<IStreetlight> OnTurnOff { get => onTurnOff; }
    public UnityEvent<IStreetlight> OnTurnOn { get => onTurnOn; }

    private ILight _light;
    private ILighter lighter;
    public string turnOnParticles = "fire_boom";
    private void Awake() {
        _light = GetComponentInChildren<ILight>(true);
        lighter = GetComponentInChildren<ILighter>(true);
    }
    [ContextMenu("TurnOn")]
    public void TurnOn() {
        if (!_light.IsOn()) {
            PSManager.instance.Play(turnOnParticles, _light.Transform, Vector3.zero, Quaternion.identity);
            _light.Turn(true);
            OnTurnOn?.Invoke(this);
        }
    }
    [ContextMenu("TurnOff")]
    public void TurnOff() {
        _light.Turn(false);
        OnTurnOff?.Invoke(this);
    }
}
