using UnityEngine;

namespace isj23.Detector {
    public class GroundDetection : MonoBehaviour, ITerrainDetector {
        public Transform groundCheck;
        public LayerMask groundLayer;
        public float groundCheckRadius = 0.2f;
        public bool canCheckGround = true;
        public bool isGround = true;
        
        public void CanCheckTerrain(bool can) {
            canCheckGround = can;
        }

        /// <summary>
        /// Use phisics to check if is touchin the ground
        /// </summary>
        /// <returns></returns>
        public bool IsTouching() {
            if(canCheckGround) {
                SetTouching(Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer));
            }
            return isGround;
        }

        public void SetTouching(bool grounded) {
            isGround = grounded;
        }

        private void OnDrawGizmos() {
            // Establecer el color del gizmo
            Gizmos.color = Color.red;

            // Dibujar una esfera en la posición del objeto con el radio especificado
            Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
        }
    } 
}
