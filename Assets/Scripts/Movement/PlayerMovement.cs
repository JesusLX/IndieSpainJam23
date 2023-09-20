using isj23.Characters;
using isj23.ST;
using System.Threading;
using UnityEngine;


namespace isj23.Movement {
    public class PlayerMovement : Movement {

        private IMovementInput input;
        private Rigidbody rb;
        private IGroundDetection groundDetection;

        public bool isGrounded = true;
        private bool charginChump = false;

        public Animator animator;
        public GameObject groundDetectorGO;

        private Countdown stopGroundCheckerCD;
        private Coroutine coroutineCD;
        private float maxTime = .5f;
        private void Start() {
            input = GetComponent<IMovementInput>();
            rb = GetComponent<Rigidbody>();
            groundDetection = groundDetectorGO.GetComponent<IGroundDetection>();
        }
        public void StartCountdown() {
            stopGroundCheckerCD = new Countdown(maxTime);
            stopGroundCheckerCD.OnTimeOut = (groundDetectionTimeOut);
            if (coroutineCD != null) {
                StopCoroutine(coroutineCD);
            }
            coroutineCD = StartCoroutine(stopGroundCheckerCD.StartCountdown());
        }
        private void groundDetectionTimeOut() {
            Debug.Log("TIEMPOOOOO");
            groundDetection.CanCheckGround(true);
        }
        private void Update() {
            if (!isGrounded && groundDetection.IsGrounded()) {
                animator.SetTrigger("onGround");
            }
            isGrounded = groundDetection.IsGrounded();

            if (isGrounded && input.JumpInput()) {
                animator.SetTrigger("onChargeJump");
                charginChump = true;
            }
            if (charginChump && isGrounded && input.JumpInputUp()) {
                animator.SetTrigger("onJump");
                charginChump = false;

                Jump();
                groundDetection.CanCheckGround(false);
                groundDetection.SetGrounded(false);
                StartCountdown();
            }
        }

        private void FixedUpdate() {
            TryMove();
        }

        private void Jump() {
            rb.AddForce(Vector3.up * stats.JumpForce, ForceMode.Impulse);
        }

        #region Movement
        public override void Init(ICharacter character) {
            Stats stats = character.Stats;
            UpdateStats(stats);
            character.OnStatsChanged.AddListener(UpdateStats);
        }

        public override void TryMove() {
            if (canMove && isGrounded && !charginChump) {
                Vector3 movement = input.GetMovementInput();
                movement *= stats.MoveSpeed;

                // Mantener la velocidad vertical actual al saltar
                Vector3 currentVelocity = rb.velocity;
                movement.y = currentVelocity.y;

                rb.velocity = movement;
            }
        }
        #endregion 
    }

}