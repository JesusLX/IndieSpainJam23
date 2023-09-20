using UnityEngine;
using isj23.Pools;
using System;

namespace isj23.ParticlesPool {

    public class PSPoolItem : PoolItem {
        public bool UseOnParticleSystemStopped = true;

        private void Awake() {
            ParticleSystem particleSystem = GetComponent<ParticleSystem>();

            var main = particleSystem.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }
        public void OnParticleSystemStopped() {
            Kill();
        }
    }
}
