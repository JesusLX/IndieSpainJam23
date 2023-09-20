using UnityEngine;

namespace isj23.Detector {
    public class GroundDetection : MonoBehaviour, IGroundDetection {
        public Transform groundCheck;
        public LayerMask groundLayer;
        public float groundCheckRadius = 0.2f;
        public bool canCheckGround = true;
        public bool isGround = true;
        
        public void CanCheckGround(bool can) {
            canCheckGround = can;
        }

        /// <summary>
        /// Use phisics to check if is touchin the ground
        /// </summary>
        /// <returns></returns>
        public bool IsGrounded() {
            if(canCheckGround) {
                SetGrounded(Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer));
            }
            return isGround;
        }

        public void SetGrounded(bool grounded) {
            isGround = grounded;
        }
    } 
}
