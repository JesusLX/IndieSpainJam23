using isj23.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace isj23.UIScreen {
    public class WinScreen : MonoBehaviour, IUiScreen {

        public Button nextButton;
        public Button mainMenuButton;
        public string nextScene;

        private void Awake() {
            if(nextButton != null) {
                nextButton.onClick.AddListener(GoNextScene);
            }
            mainMenuButton.onClick.AddListener((() => GoMainScene()));
        }

        public void GoNextScene() {
            LevelManager.instance.LoadScene(nextScene);
        }
        public void GoMainScene() {
            LevelManager.instance.LoadScene("MainMenu");
        }

        public void Show() {
            gameObject.SetActive(true);
        }

        public void Hide() {
            gameObject.SetActive(false);
        }
    }

}