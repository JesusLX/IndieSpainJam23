using isj23.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace isj23.UIScreen {
    public class GameOverScreen : MonoBehaviour, IUiScreen {
        public Button retryBtn;
        public Button mainMenuButton;

        private void Start() {
            retryBtn.onClick.AddListener(() => LevelManager.instance.ReloadScene());
            mainMenuButton.onClick.AddListener((() => GoMainScene()));
        }
        public void Show() {
            gameObject.SetActive(true);

        }
        public void GoMainScene() {
            LevelManager.instance.LoadScene("MainMenu");
        }

        public void Hide() => gameObject.SetActive(false);
    }

}