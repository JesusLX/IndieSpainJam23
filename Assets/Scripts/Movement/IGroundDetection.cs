internal interface IGroundDetection {
    bool IsGrounded();
    void SetGrounded(bool grounded);
    void CanCheckGround(bool can);
}