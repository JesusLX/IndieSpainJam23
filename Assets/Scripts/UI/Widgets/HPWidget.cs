using isj23.Characters;
using isj23.ST;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace isj23.UIScreen.UIWidgets {
    public class HPWidget : MonoBehaviour, IWidget {
        public Slider slider;
        public TextMeshProUGUI currentHp;
        public TextMeshProUGUI maxHp;
        public Stats.HealthST health;


        private void Start() {
            Player player = FindObjectOfType<Player>();
            player.OnStatsChanged.AddListener(OnStatsChange);
        }
  
        public void OnStatsChange(Stats stats) {
            SetData(stats.Health);
        }

        public void SetData(Stats.HealthST Health) {
            maxHp.text = Health.MaxHealth.ToString();
            currentHp.text = Health.CurrentHealth.ToString();
            slider.value = Health.CurrentHealth / Health.MaxHealth;
        }

        #region IWidget
        public void Init() {
            Player player = FindObjectOfType<Player>();
            health = player.Stats.Health;
            SetData(health);
        }

        public void Show() {
        }

        public void Hide() {
        }

        #endregion
    }

}