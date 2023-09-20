using isj23.Managers;
using UnityEngine;

namespace isj23.Inputs {
    public class MouseLook : MonoBehaviour, ITimeAffected {
        public float mouseSensitivity = 100f;

        private IRotationInput rotationInput;
        private Transform playerTransform;

        private float xRotation = 0f;
        private bool canMoveCam = false;


        private void Start() {
            rotationInput = GetComponent<IRotationInput>();
            playerTransform = transform.parent;
        }
        private void OnEnable() {
            AttachTimeEvents();
        }
        private void OnDisable() {
            DetachTimeEvents();
        }

        private void Update() {
            if (canMoveCam) {
                Vector2 rotationInputValues = rotationInput.GetRotationInput();
                float mouseX = rotationInputValues.x * mouseSensitivity * Time.deltaTime;
                float mouseY = rotationInputValues.y * mouseSensitivity * Time.deltaTime;

                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                playerTransform.Rotate(Vector3.up * mouseX);
            }
        }
        public void UpdateCanMoveCam(bool can) {
            canMoveCam = can;
        }
        #region ITimeAffected
        public void OnPlayTimeStarts() {
            Cursor.lockState = CursorLockMode.Locked;
            UpdateCanMoveCam(true);
        }

        public void OnPlayTimeRestore() {
            Cursor.lockState = CursorLockMode.Locked;
            UpdateCanMoveCam(true);
        }

        public void OnPlayTimeStops() {
            UpdateCanMoveCam(false);
            Cursor.lockState = CursorLockMode.None;
        }

        public void AttachTimeEvents() {
            TimeManager.instance.OnPlayTimeStart.AddListener(OnPlayTimeStarts);
            TimeManager.instance.OnPlayTimeStop.AddListener(OnPlayTimeStops);
            TimeManager.instance.OnPlayTimeRestore.AddListener(OnPlayTimeRestore);
        }

        public void DetachTimeEvents() {
            TimeManager.instance.OnPlayTimeStart.RemoveListener(OnPlayTimeStarts);
            TimeManager.instance.OnPlayTimeStop.RemoveListener(OnPlayTimeStops);
            TimeManager.instance.OnPlayTimeRestore.RemoveListener(OnPlayTimeRestore);
        }

        #endregion
    }

}