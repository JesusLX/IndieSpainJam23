using isj23.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace isj23.UIScreen {
    public class WinScreen : MonoBehaviour, IUiScreen {

        public Button playButton;

        private void Awake() {
            if(playButton != null) {
                playButton.onClick.AddListener(Play);
            }
        }

        public void Play() {
            GameManager.instance.Play();
        }

        public void Show() {
            gameObject.SetActive(true);
        }

        public void Hide() {
            gameObject.SetActive(false);
        }
    }

}