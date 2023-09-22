using DG.Tweening;
using isj23.Characters;
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
        private float jumpForceStart = 6;
        private float jumpForceMax = 12;
        private float jumpForceGrouth = 6f;
        private bool isJumping = false;

        public float velocidadRotacion = 100.0f; // Velocidad de rotaci�n, puedes ajustarla en el Inspector.
        private float maxAnguloRotacion = 30.0f;
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
            Debug.Log("TIEMPOOOOO");
            groundDetector.CanCheckTerrain(true);
        }
        private void Update() {
      
            if (!isGrounded && groundDetector.IsTouching()) {
                isJumping = false;
                charginChump = false;
                animator.SetTrigger("onGround");
                ResetRotationZ();

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
            
            if ((isGrounded || (!isGrounded && (isLeftTouching || isRightTouching))) && input.JumpInputDown()) {
                animator.SetTrigger("onChargeJump");
                charginChump = true;
                ResetJumpForce();
            }
            if(charginChump && input.JumpInput()) {
                ChagerJumpForce();
            }
            
            if (charginChump && (isGrounded || (!isGrounded && (isLeftTouching || isRightTouching))) && input.JumpInputUp()) {
                animator.SetTrigger("onJump");
                charginChump = false;

                Jump();
                groundDetector.CanCheckTerrain(false);
                groundDetector.SetTouching(false);
                StartCountdown();
            }

            if (!isGrounded && !isLeftTouching && !isRightTouching && !isJumping) {
                animator.SetTrigger("onFall");
                charginChump = false;
            }

        }

        private void ChangingGravity() {
            if (!isGrounded && (isLeftTouching || isRightTouching)) {
                currentGravity = lowGravity;
                if (rb.velocity.y > 0 && !isJumping) {
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                }
            } else {
                currentGravity = originalGravity;
            }
            Physics.gravity = new Vector3(0, currentGravity, 0);
        }

        private void FixedUpdate() {
            TryMove();
        }
        private void ChagerJumpForce() {
            if (jumpForce < jumpForceMax) {
                jumpForce += jumpForceGrouth*Time.deltaTime;
            }
        }
        private void ResetJumpForce() {
            jumpForce = jumpForceStart;
        }
        private void Jump() {
            float rotationZ = -body.eulerAngles.z;
            // Convertir la rotaci�n en Z en un vector de direcci�n 2D
            Vector2 direccion2D = new Vector2(Mathf.Cos(rotationZ * Mathf.Deg2Rad), Mathf.Sin(rotationZ * Mathf.Deg2Rad));

            // Convertir la direcci�n 2D en una direcci�n en 3D
            Vector3 direccionLanzamiento = new Vector3(direccion2D.y, direccion2D.x, 0.0f);
            // Aplicar una fuerza en la direcci�n calculada al Objeto a Lanzar
            rb.AddForce(direccionLanzamiento * jumpForce, ForceMode.Impulse);
            ResetJumpForce();
            ResetRotationZ();
            isJumping = true;
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
            float rotacion = inputHorizontal * velocidadRotacion * Time.deltaTime;

            // Aplicar la rotaci�n al objeto
            body.Rotate(Vector3.forward, rotacion);

            // Limitar la rotaci�n dentro de los l�mites
            float rotacionActual = body.eulerAngles.z;
            rotacionActual = (rotacionActual > 180) ? rotacionActual - 360 : rotacionActual;
            float nuevaRotacion = Mathf.Clamp(rotacionActual, -maxAnguloRotacion, maxAnguloRotacion);

            // Aplicar la rotaci�n limitada al objeto
            body.rotation = Quaternion.Euler(0, 0, nuevaRotacion);
        }
        void ResetRotationZ() {
            // Utiliza DoTween para establecer la rotaci�n Z a 0 grados en 1 segundo.

           DOresetPos = body.DORotate(new Vector3(0f, 0f, 0f), 1.0f).SetEase(Ease.OutQuad);

        }
        void PivotRotationZ(float degrees) {
            // Utiliza DoTween para establecer la rotaci�n Z a 0 grados en 1 segundo.
            if(DOresetPos != null && DOresetPos.IsActive()) {
                DOresetPos.Kill();
            }
            body.DORotate(new Vector3(0f, 0f, degrees), 0.5f).SetEase(Ease.OutQuad);

        }
        public override void TryMove() {
            if (canMove && isGrounded && !charginChump) {
                Vector3 movement = input.GetMovementInput();
                movement *= stats.MoveSpeed;

                rb.velocity = movement;
            }else if (canMove && !isGrounded && !charginChump)
            {
                Vector3 movement = input.GetMovementInput();
                movement *= stats.MoveSpeed/5;

                rb.velocity += movement * Time.fixedDeltaTime;
            } else if(canMove  && charginChump && isGrounded) {
                rb.velocity = Vector3.zero;
                Rotate();
            }
        }
        #endregion 
    }

}