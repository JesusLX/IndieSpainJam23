using isj23.Characters;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace isj23.Movement {
    public class AutomaticMovement : Movement {

        private ICharacter player;
        public float rotationSpeed = 2f;
        private bool notTooClose;
        public UnityEvent OnPlayerTooClose;

        void Update() {
            TryMove();
        }

        private IEnumerator TooCloseAlert() {
            while (true) {
                if (!notTooClose) {
                    OnPlayerTooClose?.Invoke();
                    yield return null;
                } else {
                    yield return null;
                }
            }
        }

        /// <summary>
        /// Slowly look at the target
        /// </summary>
        /// <param name="target">Target to look at</param>
        private void LookAt(Transform target) {
            Vector3 targetDirection = target.transform.position - transform.position;
            targetDirection.y = 0f;

            if (targetDirection != Vector3.zero) {
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }

        #region Movement
        public override void Init(ICharacter player) {
            stats = player.Stats.Movement;
            this.player = player;
            UpdateCanMove(true);
        }

        /// <summary>
        /// If can move and has target, move fordward until get too close to the target
        /// </summary>
        public override void TryMove() {
            if (canMove && player != null) {
                LookAt(player.Transform);

                float distance = Vector3.Distance(transform.position, player.Transform.position);
                if (notTooClose = (distance > 1)) {
                    transform.position += transform.forward * stats.MoveSpeed * Time.deltaTime;
                } else if (distance <= 1) {

                }
            }
        }
        
        internal override void OnEnableEvents() {
            base.OnEnableEvents();
            StartCoroutine(TooCloseAlert());
        }

        internal override void OnDisableEvents() {
            base.OnDisableEvents();
            StopCoroutine(TooCloseAlert());
        } 
        #endregion
    }

}