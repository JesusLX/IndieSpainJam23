using isj23.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace isj23.EnemyPool {
    public partial class EnemyManager : Singleton<EnemyManager> {
        public List<EnemyPool> enemyPools;

        [Header("Test")]
        public string testId;
        public bool testMultishootAtStart = false;
        private Coroutine testMultishootCor;

        private void Start() {
            enemyPools ??= new();
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
        /// Test Function "Play" with the position and rotation of this manager using this.testId to select the Enemy to play
        /// </summary>
        [ContextMenu("Test/Play")]
        public void TestPlay() {
            Play(
                testId,
                null ,
                transform.position, 
                transform.rotation);
        }
        [ContextMenu("Test/Random")]
        public void TestPlayRandom() {
            PlayRandom(
                null,
                transform.position,
                transform.rotation);
        }
        /// <summary>
        /// Test Function "Play" using this manager transform as parent and this.testId to select the Enemy to play
        /// </summary>
        [ContextMenu("Test/PlayAttached")]
        public void TestPlayAttached() {
            Play(testId, gameObject.transform,Vector3.zero,Quaternion.identity);
        }

        /// <summary>
        /// Invoke the particle system using the pool
        /// </summary>
        /// <param name="poolId">Id to find the pool</param>
        /// <param name="parent">Transform nullable to set as the Enemy Parent</param>
        /// <param name="position">Vector3 where to position and Play the Enemy</param>
        /// <param name="rotation">Rotation of the Enemy</param>
        /// <returns>The Enemy invoked</returns>
        public Enemy Play(string poolId, Transform parent, Vector3 position, Quaternion rotation) {
            if(position == null) {
                position = Vector3.zero;
            }
            EnemyPool pool = FindPool(poolId);
            var ps = pool.Play(parent, position, rotation);

            return ps.GetComponent<Enemy>();
        }

        private EnemyPool FindPool(string poolId) {
            EnemyPool pool = enemyPools.Find(pool => pool.Id == poolId);
            return pool;
        }
        private EnemyPool FindRandom() {
            EnemyPool pool = enemyPools[Random.Range(0,enemyPools.Count-1)];
            return pool;
        }

        public Enemy PlayRandom( Transform parent, Vector3 position, Quaternion rotation) {
            if (position == null) {
                position = Vector3.zero;
            }
            EnemyPool pool = FindRandom();
            var ps = pool.Play(parent, position, rotation);

            return ps.GetComponent<Enemy>();
        }
    }
}
