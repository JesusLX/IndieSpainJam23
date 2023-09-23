using isj23.Characters;
using isj23.ST;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DamageZone : MonoBehaviour, ICharacter {
    [SerializeField]
    private Stats stats;

    public Stats Stats { get => stats; set => stats = value; }

    public UnityEvent<Stats> OnStatsChanged => null;

    public Transform Transform => this.transform;

    public void AddExp(float experience) {
    }

    public void Die(ICharacter assasing) {
    }

    public void Hit(float damage, ICharacter damagedBy) {
    }

    public void Init() {
    }

    public void UpdateStats(Stats stats) {
    }

    void OnCollisionEnter(Collision col) {
        ICharacter character;
        if (col.gameObject.TryGetComponent(out character)) {
        Debug.Log(col.gameObject.name);
            character.Hit(Stats.Attack.Damage,this);
        }
    }

}

