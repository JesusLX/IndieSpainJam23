using isj23.Managers;
using TMPro;
using UnityEngine;

namespace isj23.UIScreen {
    public class GameOverScreen : MonoBehaviour, IUiScreen {
        public TextMeshProUGUI seconds;
        public TextMeshProUGUI enemies;

        public void Show() {
            gameObject.SetActive(true);
         
        }

        public void Hide() => gameObject.SetActive(false);
    }

}