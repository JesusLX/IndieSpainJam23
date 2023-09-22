using UnityEngine;

namespace isj23.Detector {
    public class WallDetector : MonoBehaviour, ITerrainDetector {
        public Transform terrainChecker;
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
                SetTouching(Physics.CheckSphere(terrainChecker.position, groundCheckRadius, groundLayer));
            }
            return isGround;
        }

        public void SetTouching(bool grounded) {
            isGround = grounded;
        }
    } 
}
