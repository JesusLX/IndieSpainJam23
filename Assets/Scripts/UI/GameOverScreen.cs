using isj23.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace isj23.UIScreen {
    public class GameOverScreen : MonoBehaviour, IUiScreen {
        public Button retryBtn;

        private void Start() {
            retryBtn.onClick.AddListener(()=>LevelManager.instance.ReloadScene());
        }
        public void Show() {
            gameObject.SetActive(true);
         
        }

        public void Hide() => gameObject.SetActive(false);
    }

}