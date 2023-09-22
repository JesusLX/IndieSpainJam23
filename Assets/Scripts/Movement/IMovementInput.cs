using UnityEngine;

public interface IMovementInput {
    Vector3 GetMovementInput();
    float GetMovementAxis();
    bool JumpInput();
    bool JumpInputDown();
    bool JumpInputUp();
}