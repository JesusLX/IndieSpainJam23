using UnityEngine;

public interface IMovementInput {
    Vector3 GetMovementInput();
    bool JumpInput();
    bool JumpInputUp();
}