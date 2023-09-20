using UnityEngine;

namespace isj23.Detector {
    public class GroundDetection : MonoBehaviour, IGroundDetection {
        public Transform groundCheck;
        public LayerMask groundLayer;
        public float groundCheckRadius = 0.2f;

        /// <summary>
        /// Use phisics to check if is touchin the ground
        /// </summary>
        /// <returns></returns>
        public bool IsGrounded() {
            return Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        }
    } 
}
