using isj23.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace isj23.UIScreen {
    public class GameOverScreen : MonoBehaviour, IUiScreen {
        public Button retryBtn;
        public Button mainMenuButton;
        public Curtain closeCurtain;

        private void Start() {
            retryBtn.onClick.AddListener(() => Retry());
            mainMenuButton.onClick.AddListener((() => GoMainScene()));
        }
        public void Show() {
            gameObject.SetActive(true);

        }
        public void GoMainScene() {
            TimeManager.instance.StopPlayTime();
            closeCurtain.onPartycleSystemStopped.AddListener(() => { LevelManager.instance.LoadScene("MainMenu"); });
            UIManager.instance.FadeOut();
            closeCurtain.Play();
            Hide();
        }
        public void Retry() {
            TimeManager.instance.StopPlayTime();
            closeCurtain.onPartycleSystemStopped.AddListener(() => { LevelManager.instance.ReloadScene(); });
            UIManager.instance.FadeOut();
            closeCurtain.Play();
            Hide();
        }

        public void Hide() => gameObject.SetActive(false);
    }

}