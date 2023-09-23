using DG.Tweening;
using isj23.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace isj23.UIScreen {
    public class MainMenu : MonoBehaviour, IUiScreen {

        public Button playButton;
        public Curtain closeCurtain;

        private void Awake() {
            if (playButton != null) {
                playButton.onClick.AddListener(Play);
            } else {
                closeCurtain.onPartycleSystemStopped.AddListener(() => GameManager.instance.Play(true));
            }
        }

        public void Play() {
            closeCurtain.Play();
            GameManager.instance.Play(playButton != null);
        }

        public void Show() {
            gameObject.SetActive(true);
            if (playButton == null) {
                Play();
            }
        }

        public void Hide() {
            gameObject.SetActive(false);
        }
    }

}