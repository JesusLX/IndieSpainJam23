using UnityEngine;

namespace isj23.Inputs {
    public class KeyboardMovementInput : MonoBehaviour, IMovementInput {
        public Vector3 GetMovementInput() {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            //float moveVertical = Input.GetAxisRaw("Vertical");
            float moveVertical = 0;

            Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
            movement = transform.forward * movement.z + transform.right * movement.x;
            movement.Normalize();

            return movement;
        }
        public float GetMovementAxis() {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            return moveHorizontal;
        }

        /// <summary>
        /// Returns if Space is pressed down
        /// </summary>
        /// <returns></returns>
        public bool JumpInputDown() {
            return Input.GetKeyDown(KeyCode.Space);
        }
        public bool JumpInput() {
            return Input.GetKey(KeyCode.Space);
        }
        public bool JumpInputUp() {
            return Input.GetKeyUp(KeyCode.Space);
        }
    } 
}