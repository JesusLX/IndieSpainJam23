using isj23.ST;
using UnityEngine;
using UnityEngine.Events;

namespace isj23.Characters {
    public interface ICharacter {
        public Stats Stats { get; set; }
        public UnityEvent<Stats> OnStatsChanged { get; }
        public Transform Transform { get; }
        public void Init();

        //public void AddWeapon(IWeapon weapon);
        void Hit(float damage, ICharacter damagedBy);
        void Die(ICharacter assasing);
        void AddExp(float experience);
        void UpdateStats(Stats stats);
    } 
}