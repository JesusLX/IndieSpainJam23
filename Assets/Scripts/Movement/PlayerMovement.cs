using DG.Tweening;
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

        private float jumpForce = 0;
        private float jumpForceStart = 6;
        private float jumpForceMax = 12;
        private float jumpForceGrouth = 6f;

        public float velocidadRotacion = 100.0f; // Velocidad de rotaci�n, puedes ajustarla en el Inspector.
        private float maxAnguloRotacion = 30.0f;
        public Transform body;
        public Transform pivote;

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

            if (isGrounded && input.JumpInputDown()) {
                animator.SetTrigger("onChargeJump");
                charginChump = true;
                ResetJumpForce();
                Debug.Log("Cargandoooo");
            }
            if(charginChump && input.JumpInput()) {
                ChagerJumpForce();
            }
            if (charginChump && isGrounded && input.JumpInputUp()) {
                animator.SetTrigger("onJump");
                charginChump = false;

                Jump();
                groundDetection.CanCheckGround(false);
                groundDetection.SetGrounded(false);
                StartCountdown();
                ResetRotationZ();
            }
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
            body.DORotate(new Vector3(0f, 0f, 0f), 1.0f).SetEase(Ease.OutQuad);
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

                rb.velocity += movement*Time.fixedDeltaTime;
            } else if(canMove  && charginChump) {
                rb.velocity = Vector3.zero;
                Rotate();
            }
        }
        #endregion 
    }

}