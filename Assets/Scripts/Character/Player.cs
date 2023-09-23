using isj23.Managers;
using isj23.Movement;
using isj23.ST;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace isj23.Characters {
    public class Player : MonoBehaviour, ICharacter {
        //public HashSet<IWeapon> weapons;

        #region ICharacter
        [SerializeField] private Stats basicStats;
        private Stats currentStats;
        public Stats Stats { get => currentStats; set => currentStats = value; }
        private UnityEvent<Stats> onStatsChanged = new UnityEvent<Stats>();
        public Transform Transform => transform;
        #endregion

        public UnityEvent<Stats.LevelST> OnExperienceChanged = new UnityEvent<Stats.LevelST>();
        public UnityEvent<Stats> OnStatsChanged => onStatsChanged;

        public UnityEvent OnEnemyKilled = new();
        public UnityEvent OnDie = new();

        public LighterDetector lighterDetector;
        public LightLifeController lightLifeController;

        private void OnEnable() {
            onStatsChanged = new UnityEvent<Stats>();
            //FindObjectOfType<LevelManager>().OnLevelUp.AddListener(LevelUp);
            AttachTimeEvents();
        }
        private void OnDisable() {
            DetachTimeEvents();
        }
        private void Start() {
            lighterDetector.OnLighterTurnedOn.AddListener(lightLifeController.AddLightTime);
        }

        public void LevelUp(Stats.LevelST level) {
        }

        #region ICharacter
        public void Init() {
            transform.rotation = Quaternion.identity;
            Stats = ScriptableObject.CreateInstance<Stats>();
            UpdateStats(basicStats);
            //weapons = new HashSet<IWeapon>(GetComponentsInChildren<IWeapon>());
            //foreach (var weapon in weapons) {
            //    AddWeapon(weapon);
            //}
            GetComponent<IMovement>().Init(this);
        }

        //public void AddWeapon(IWeapon weapon) {
        //    weapon.Init(this);
        //    weapons.Add(weapon);
        //}

        public void Hit(float damage, ICharacter assasing) {
            Stats.Health.CurrentHealth -= damage;
            if (Stats.Health.CurrentHealth <= 0) {
                Die(assasing);
            }
            OnStatsChanged?.Invoke(Stats);
        }

        public void Die(ICharacter assasing) {
            GameManager.instance.GameOver();
            OnDie?.Invoke();
        }

        public void AddExp(float experience) {
            OnEnemyKilled?.Invoke();
            Stats.Level.Experience += experience;
            OnExperienceChanged?.Invoke(Stats.Level);
        }

        public void UpdateStats(Stats stats) {
            this.Stats += stats;
            OnStatsChanged?.Invoke(this.Stats);
        }
        #endregion

        #region ITimeAffected
        public void OnPlayTimeStarts() {
            Init();
        }

        public void OnPlayTimeRestore() {
        }

        public void OnPlayTimeStops() {
        }

        public void AttachTimeEvents() {
            TimeManager.instance.OnPlayTimeStart.AddListener(OnPlayTimeStarts);
            TimeManager.instance.OnPlayTimeStop.AddListener(OnPlayTimeStops);
            TimeManager.instance.OnPlayTimeRestore.AddListener(OnPlayTimeRestore);
        }

        public void DetachTimeEvents() {
            TimeManager.instance.OnPlayTimeStart.RemoveListener(OnPlayTimeStarts);
            TimeManager.instance.OnPlayTimeStop.RemoveListener(OnPlayTimeStops);
            TimeManager.instance.OnPlayTimeRestore.RemoveListener(OnPlayTimeRestore);
        }

        #endregion
    }

}