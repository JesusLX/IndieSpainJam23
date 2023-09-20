using isj23.Movement;
using isj23.ParticlesPool;
using isj23.Pools;
using isj23.ST;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace isj23.Characters {
    public class Enemy : PoolItem, ICharacter {
        public string SpawnPSKey;
        public string DiePSKey;

        #region ICharacter
        [SerializeField] private Stats basicStats;
        private Stats currentStats;

        public Stats Stats { get => currentStats; set => currentStats = value; }
        private UnityEvent<Stats> onStatsChanged = new UnityEvent<Stats>();
        public Transform Transform => this.transform;
        #endregion
        
        public UnityEvent<Stats> OnStatsChanged => onStatsChanged;

        private IMovement movementController;

        void Start() {
            GetComponent<AutomaticMovement>().OnPlayerTooClose.AddListener(Attack);

        }

        /// <summary>
        /// Attack with all the weapons
        /// </summary>
        public void Attack() {
        }

        #region ICharacter
        public void Init() {
            Stats = ScriptableObject.CreateInstance<Stats>() + basicStats;

            movementController = GetComponent<IMovement>();
            movementController.Init(FindObjectOfType<Player>());


            PSManager.instance.Play(SpawnPSKey, null, transform.position, Quaternion.LookRotation(Vector3.up));

        }



        public void Hit(float damage, ICharacter damagedTo) {
            Stats.Health.CurrentHealth -= damage;
            if (Stats.Health.CurrentHealth <= 0) {
                Die(damagedTo);
            }

        }

        public void Die(ICharacter assasing) {
            PSManager.instance.Play(DiePSKey, null, transform.position, Quaternion.identity);
            assasing.AddExp(
                Stats.Level.Experience
                );
            Kill();
        }

        public void AddExp(float experience) {
            Stats.Level.Experience += experience;
        }

        public void UpdateStats(Stats stats) {
            this.Stats += stats;
            OnStatsChanged?.Invoke(this.Stats);
        }
        #endregion
    } 
}
