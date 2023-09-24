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
        public Curtain closeCurtain;

        private void Awake() {
            if(nextButton != null) {
                nextButton.onClick.AddListener(GoNextScene);
            }
            mainMenuButton.onClick.AddListener((() => GoMainScene()));
        }

        public void GoNextScene() {
            
            TimeManager.instance.StopPlayTime();
            closeCurtain.onPartycleSystemStopped.AddListener(() => { LevelManager.instance.LoadScene(nextScene); });
            UIManager.instance.FadeOut();
            closeCurtain.Play();
            Hide();
        }
        public void GoMainScene() {
            TimeManager.instance.StopPlayTime();
            closeCurtain.onPartycleSystemStopped.AddListener(() => { LevelManager.instance.LoadScene("MainMenu"); });
            UIManager.instance.FadeOut();
            closeCurtain.Play();
            Hide();
        }

        public void Show() {
            gameObject.SetActive(true);
        }

        public void Hide() {
            gameObject.SetActive(false);
        }
    }

}