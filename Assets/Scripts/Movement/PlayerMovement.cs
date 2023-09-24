using DG.Tweening;
using isj23.Characters;
using isj23.ParticlesPool;
using isj23.ST;
using System.Threading;
using UnityEditor;
using UnityEngine;


namespace isj23.Movement {
    public class PlayerMovement : Movement {

        private IMovementInput input;
        private Rigidbody rb;
        private ITerrainDetector groundDetector;
        private ITerrainDetector leftWallDetector;
        private ITerrainDetector rightWallDetector;

        public bool isGrounded = true;
        public bool isLeftTouching = true;
        public bool isRightTouching = true;
        private bool charginChump = false;

        public Animator animator;
        public GameObject groundDetectorGO;
        public GameObject leftWallDetectorGO;
        public GameObject rightWallDetectorGO;

        private Countdown stopGroundCheckerCD;
        private Coroutine coroutineCD;
        private float maxTime = .5f;

        private float wallJumpForce = 8;
        private float jumpForce = 0;
        public float jumpForceStart = 8;
        public float jumpForceMax = 12;
        public float jumpForceGrouth = 6f;
        private bool isJumping = false;
        private bool isFalling = false;
        private bool positioning = false;

        public float velocidadRotation = 100.0f; // Velocidad de rotaci�n, puedes ajustarla en el Inspector.
        private float maxAnguloRotation = 35.0f;
        public Transform body;
        public Transform pivote;

        public float originalGravity = -9.81f;
        public float lowGravity = -8.0f;
        public float currentGravity = -1.0f;

        private Tween DOresetPos;

        private void Start() {
            input = GetComponent<IMovementInput>();
            rb = GetComponent<Rigidbody>();
            groundDetector = groundDetectorGO.GetComponent<ITerrainDetector>();
            leftWallDetector = leftWallDetectorGO.GetComponent<ITerrainDetector>();
            rightWallDetector = rightWallDetectorGO.GetComponent<ITerrainDetector>();
        }
        public void StartCountdown() {
            stopGroundCheckerCD = new Countdown(maxTime);
            stopGroundCheckerCD.OnTimeOut = (groundDetectorTimeOut);
            if (coroutineCD != null) {
                StopCoroutine(coroutineCD);
            }
            coroutineCD = StartCoroutine(stopGroundCheckerCD.StartCountdown());
        }
        private void groundDetectorTimeOut() {
            groundDetector.CanCheckTerrain(true);
        }
        private void Update() {
            if (canMove) {

                if (!isGrounded && groundDetector.IsTouching()) {
                    isJumping = false;
                    charginChump = false;
                    animator.ResetTrigger("onFall");
                    animator.SetTrigger("onGround");
                    ResetRotationZ();
                    isFalling = false;
                }
                isGrounded = groundDetector.IsTouching();
                if (!isGrounded && leftWallDetector.IsTouching() && !isLeftTouching) {
                    isJumping = false;
                    charginChump = false;
                    //animator.SetTrigger("onLeftWall");
                    PivotRotationZ(-25);
                    Debug.Log("onLeftWall");
                }
                isLeftTouching = leftWallDetector.IsTouching();
                if (!isGrounded && rightWallDetector.IsTouching() && !isLeftTouching && !isRightTouching) {
                    PivotRotationZ(25);
                    isJumping = false;
                    charginChump = false;
                    //animator.SetTrigger("onRightWall");
                    Debug.Log("onRightWall");
                }
                isRightTouching = rightWallDetector.IsTouching();

                ChangingGravity();
                if (!charginChump && (isGrounded || (!isGrounded && (isLeftTouching || isRightTouching))) && input.JumpInput()) {
                    animator.SetTrigger("onChargeJump");
                    charginChump = true;
                    ResetJumpForce();
                }
                if ((isGrounded || (!isGrounded && (isLeftTouching || isRightTouching))) && input.JumpInputDown()) {
                    animator.SetTrigger("onChargeJump");
                    charginChump = true;
                    ResetJumpForce();
                }
                if (charginChump && input.JumpInput()) {
                    ChagerJumpForce();
                }

                if (charginChump && (isGrounded || (!isGrounded && (isLeftTouching || isRightTouching))) && input.JumpInputUp()) {
                    TryJump();
                }
                if (!isGrounded && !isLeftTouching && !isRightTouching && !isJumping) {
                    animator.SetTrigger("onFall");
                    charginChump = false;
                }


            }
        }

        private void ChangingGravity() {
            if (!isGrounded && (isLeftTouching || isRightTouching)) {
                currentGravity = lowGravity;
                if (rb.velocity.y > 0 && !isJumping) {
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                }
            } else {
                if (rb.velocity.y > 0 && isJumping) {
                    currentGravity = originalGravity * 1.5f;
                    isFalling = true;
                } else {
                    currentGravity = originalGravity;
                }
            }
            Physics.gravity = new Vector3(0, currentGravity, 0);
        }

        private void FixedUpdate() {
            TryMove();
        }
        private void ChagerJumpForce() {
            if (jumpForce < jumpForceMax) {
                jumpForce += jumpForceGrouth * Time.deltaTime;
            }
        }
        private void ResetJumpForce() {
            jumpForce = jumpForceStart;
        }
        private void TryJump() {
            if (!positioning) {
                animator.SetTrigger("onJump");
                charginChump = false;

                Jump();
                groundDetector.CanCheckTerrain(false);
                groundDetector.SetTouching(false);
                StartCountdown();
            }
        }
        private void Jump() {
            float rotationZ = -body.eulerAngles.z;
            // Convertir la rotaci�n en Z en un vector de direcci�n 2D
            Vector2 direccion2D = new Vector2(Mathf.Cos(rotationZ * Mathf.Deg2Rad), Mathf.Sin(rotationZ * Mathf.Deg2Rad));

            // Convertir la direcci�n 2D en una direcci�n en 3D
            Vector3 direccionLanzamiento = new Vector3(direccion2D.y, direccion2D.x, 0.0f);
            // Aplicar una fuerza en la direcci�n calculada al Objeto a Lanzar
            rb.AddForce(direccionLanzamiento * jumpForce, ForceMode.Impulse);
            PSManager.instance.Play("jump", null, transform.position, body.rotation);
            ResetJumpForce();
            ResetRotationZ(true);
            isJumping = true;
            AudioManager.Instance.Play("jump", true, true);
        }

        #region Movement
        public override void Init(ICharacter character) {
            Stats stats = character.Stats;
            UpdateStats(stats);
            character.OnStatsChanged.AddListener(UpdateStats);
        }

        void Rotate() {
            // Obtener el valor de entrada horizontal (izquierda y derecha)
            float inputHorizontal = -input.GetMovementAxis();

            // Calcular el �ngulo de rotaci�n
            float rotation = inputHorizontal * velocidadRotation * Time.deltaTime;

            // Aplicar la rotaci�n al objeto
            body.Rotate(Vector3.forward, rotation);

            // Limitar la rotaci�n dentro de los l�mites
            float rotationActual = body.eulerAngles.z;
            rotationActual = (rotationActual > 180) ? rotationActual - 360 : rotationActual;
            float nuevaRotation = Mathf.Clamp(rotationActual, -maxAnguloRotation, maxAnguloRotation);

            // Aplicar la rotaci�n limitada al objeto
            body.rotation = Quaternion.Euler(0, 0, nuevaRotation);
        }
        void ResetRotationZ(bool canAnimate = true) {
            float doOrNot = Random.Range(0f, 1f);
            Debug.Log(body.rotation.z);
            if (canAnimate && doOrNot > .8f && (body.rotation.z >= 0f)) {
                var move = 360f;
                DOresetPos = body.DORotate(new Vector3(0f, move, 0f), .5f, RotateMode.FastBeyond360).SetEase(Ease.OutQuad);
            } else {
                DOresetPos = body.DORotate(new Vector3(0f, 0f, 0f), 1.0f).SetEase(Ease.OutQuad);
            }


        }
        void PivotRotationZ(float degrees) {
            // Utiliza DoTween para establecer la rotaci�n Z a 0 grados en 1 segundo.
            if (DOresetPos != null && DOresetPos.IsActive()) {
                DOresetPos.Kill();
            }
            positioning = true;
            body.DORotate(new Vector3(0f, 0f, degrees), 0.3f).SetEase(Ease.OutQuad).OnComplete(() => positioning = false).OnKill(() => positioning = false);

        }
        public override void TryMove() {
            if (canMove) {
                if (isGrounded && !charginChump) {
                    Vector3 movement = input.GetMovementInput();
                    movement *= stats.MoveSpeed;

                    rb.velocity = movement;
                } else if (!isGrounded && !charginChump) {
                    Vector3 movement = input.GetMovementInput();
                    movement *= stats.MoveSpeed / 5;

                    rb.velocity += movement * Time.fixedDeltaTime;
                } else if (charginChump && isGrounded) {
                    rb.velocity = Vector3.zero;
                    Rotate();
                }
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            }
        }
        #endregion 
    }

}