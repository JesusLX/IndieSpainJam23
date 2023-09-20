using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace isj23.ParticlesPool {
    public partial class PSManager : Singleton<PSManager> {
        public List<ParticleSystemPool> pSPools;

        [Header("Test")]
        public string testId;
        public bool testMultishootAtStart = false;
        private Coroutine testMultishootCor;

        private void Start() {
            pSPools ??= new();
            if (testMultishootAtStart) {
                testMultishootCor = StartCoroutine(Multishot());
            }
        }

        private IEnumerator Multishot() {
            while (testMultishootAtStart) {
                TestPlay();
                yield return 0f;
            }
        }
        /// <summary>
        /// Test Function "Play" with the position and rotation of this manager using this.testId to select the ParticleSystem to play
        /// </summary>
        [ContextMenu("Test/Play")]
        public void TestPlay() {
            Play(
                testId,
                null ,
                transform.position, 
                transform.rotation);
        }
        /// <summary>
        /// Test Function "Play" using this manager transform as parent and this.testId to select the ParticleSystem to play
        /// </summary>
        [ContextMenu("Test/PlayAttached")]
        public void TestPlayAttached() {
            Play(testId, gameObject.transform,Vector3.zero,Quaternion.identity);
        }

        /// <summary>
        /// Invoke the particle system using the pool
        /// </summary>
        /// <param name="poolId">Id to find the pool</param>
        /// <param name="parent">Transform nullable to set as the ParticleSystem Parent</param>
        /// <param name="position">Vector3 where to position and Play the ParticleSystem</param>
        /// <param name="rotation">Rotation of the ParticleSystem</param>
        /// <returns>The ParticleSystem invoked</returns>
        public ParticleSystem Play(string poolId, Transform parent, Vector3 position, Quaternion rotation) {
            if(position == null) {
                position = Vector3.zero;
            }
            ParticleSystemPool pool = FindPool(poolId);
            var ps = pool.Play(parent, position, rotation);

            return ps.GetComponent<ParticleSystem>();
        }

        private ParticleSystemPool FindPool(string poolId) {
            ParticleSystemPool pool = pSPools.Find(pool => pool.Id == poolId);
            return pool;
        }
    }
}
