using isj23.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetlightController : MonoBehaviour {
    private List<IStreetlight> streetlights;
    private int lightsOn = 0;
    private void Start() {
        streetlights = new List<IStreetlight>(GetComponentsInChildren<IStreetlight>());
        streetlights.ForEach(streetlight => { streetlight.TurnOff(); 
        streetlight.OnTurnOn.AddListener(OnStreetLightTurnOn);
        streetlight.OnTurnOff.AddListener(OnStreetLightTurnOff);
        });
        Debug.Log(streetlights.Count+" Farolas");
    }

    private void OnStreetLightTurnOff(IStreetlight streetlight) {
        lightsOn--;
        if(lightsOn < 0) {
            lightsOn = 0;
        }
    }
    private void OnStreetLightTurnOn(IStreetlight streetlight) {
        lightsOn++;
        if(lightsOn == streetlights.Count) {
            GameManager.instance.WinGame();
        }
    }
}
