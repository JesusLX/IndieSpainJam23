using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter : MonoBehaviour, ILighter {
    private IStreetlight myLight;
    private void Start() {
        myLight = GetComponentInParent<IStreetlight>();
    }
    public void TurnOn() {
        myLight.TurnOn();
    }
    public void TurnOff() {
        myLight.TurnOff();
    }

}
