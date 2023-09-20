using System;
using UnityEngine;

namespace isj23.ST {
    [CreateAssetMenu(fileName = "New Stat", menuName = "isj23/Stats")]
    public class Stats : ScriptableObject {
        [Serializable]
        public struct HealthST {

            [SerializeField] private float maxHealth;
            [SerializeField] private float currentHealth;
            public float MaxHealth { get => maxHealth; set => maxHealth = value; }
            public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
            public static Stats.HealthST operator +(Stats.HealthST a, Stats.HealthST b) {
                a.MaxHealth += b.MaxHealth;
                a.CurrentHealth += b.CurrentHealth;
                if (a.currentHealth > a.MaxHealth) a.CurrentHealth = a.MaxHealth;
                return a;
            }
        }
        [Serializable]
        public struct MovementST {

            [SerializeField] private float moveSpeed;
            [SerializeField] private float jumpForce;
            public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
            public float JumpForce { get => jumpForce; set => jumpForce = value; }
            public static Stats.MovementST operator +(Stats.MovementST a, Stats.MovementST b) {
                a.moveSpeed += b.moveSpeed;
                a.jumpForce += b.jumpForce;
                return a;
            }
        }
        [Serializable]
        public struct AttackST {

            [SerializeField] private float damage;
            [SerializeField] private float rafaga;
            [SerializeField] private float rafagaCountdown;
            [SerializeField] private float shootCountdown;
            [SerializeField] private Vector3 shootMargenError;

            public float Damage { get => damage; set => damage = value; }
            public float RafagaCountdown { get => rafagaCountdown; set => rafagaCountdown = value; }
            public float Rafaga { get => rafaga; set => rafaga = value; }
            public float ShootCountdown { get => shootCountdown; set => shootCountdown = value; }
            public Vector3 ShootMarginError { get => shootMargenError; set => shootMargenError = value; }

            public Vector3 ApplyShootMargenError(Vector3 positionToApply) {
                return GetShootMargenError(positionToApply, this.ShootMarginError);
            }

            public static Vector3 GetShootMargenError(Vector3 positionToApply, Vector3 marginError) {
                var rX = UnityEngine.Random.Range(-marginError.x, marginError.x);
                var rY = UnityEngine.Random.Range(-marginError.y, marginError.y);
                var rZ = UnityEngine.Random.Range(-marginError.z, marginError.z);
                Vector3 errorPoint = new Vector3(
                    positionToApply.x + rX,
                    positionToApply.y + rY,
                    positionToApply.z + rZ
                    );
                return errorPoint;
            }
            public static Stats.AttackST operator +(Stats.AttackST a, Stats.AttackST b) {
                a.Damage += b.Damage;
                a.Rafaga += b.Rafaga;
                a.RafagaCountdown += b.RafagaCountdown;
                a.ShootCountdown += b.ShootCountdown;
                a.ShootMarginError += b.ShootMarginError;
                return a;
            }
        }

        [Serializable]
        public struct LevelST {

            [SerializeField] private int level;
            [SerializeField] private float experience;
            public int Level { get => level; set => level = value; }
            public float Experience { get => experience; set => experience = value; }
            public static Stats.LevelST operator +(Stats.LevelST a, Stats.LevelST b) {
                a.level += b.level;
                a.experience += b.experience;
                return a;
            }
        }
        public Stats.HealthST Health;
        public Stats.MovementST Movement;
        public Stats.AttackST Attack;
        public Stats.LevelST Level;

        public static Stats operator +(Stats a, Stats b) {
            a.Health += b.Health;
            a.Movement += b.Movement;
            a.Attack += b.Attack;
            a.Level += b.Level;
            return a;
        }
    } 
}