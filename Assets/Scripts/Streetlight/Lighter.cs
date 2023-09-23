using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter : MonoBehaviour, ILighter {
    private IStreetlight myLight;
    private void Start() {
        myLight = GetComponentInParent<IStreetlight>();
    }
    public bool TurnOn() {
        return myLight.TurnOn();
    }
    public void TurnOff() {
        myLight.TurnOff();
    }

}
