using isj23.UIScreen.UIWidgets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mute : MonoBehaviour, IWidget {
    public Image muted;
    public Image sound;
    private Button muteButton;

    private void Start() {
    }

    public void Hide() {
        this.enabled = false;

    }

    public void Init() {
        muteButton = GetComponent<Button>();
        muteButton.onClick.RemoveAllListeners();
        muteButton.onClick.AddListener(Click);
        if (AudioManager.Instance.GetVolume("background") == 0) {
            muted.enabled = true;
            sound.enabled = false;
        } else {
            muted.enabled = false;
            sound.enabled = true;
        }
    }

    public void Show() {
        this.enabled = true;
       
    }
    public void Click() {
        if (AudioManager.Instance.GetVolume("background") == 0) {
            AudioManager.Instance.ResetAllVolumes();
            muted.enabled = false;
            sound.enabled = true;
        } else {
            AudioManager.Instance.ChangeAllVolumes(0);
            muted.enabled = true;
            sound.enabled = false;
        }
    }
}
