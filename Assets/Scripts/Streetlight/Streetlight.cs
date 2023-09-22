using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Streetlight : MonoBehaviour, IStreetlight {
    private UnityEvent<IStreetlight, bool> onStreetlightTurn = new UnityEvent<IStreetlight, bool>();

    internal UnityEvent<IStreetlight, bool> OnStreetlightTurn { get => onStreetlightTurn; set => onStreetlightTurn = value; }
    private ILight _light;
    private ILighter lighter;
    private void Start() {
        _light = GetComponentInChildren<ILight>();
        lighter = GetComponentInChildren<ILighter>();
    }
    [ContextMenu("TurnOn")]
    public void TurnOn() {

        _light.Turn(true);
    }
    [ContextMenu("TurnOff")]
    public void TurnOff() {
        _light.Turn(false);
    }
}
