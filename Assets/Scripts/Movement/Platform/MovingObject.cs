using isj23.Detector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {
    public Vector3 initialPosition; // The initial position
    public Vector3 finalPosition;   // The final position
    public float speed = 2.0f;     // Movement speed

    private bool goingToFinal = true; // Indicates if we are moving towards the final position

    void Start() {
        // Initially set the position to the initial position
        transform.position = initialPosition;
    }

    void Update() {
        // Determine the movement direction
        Vector3 direction = goingToFinal ? (finalPosition - transform.position) : (initialPosition - transform.position);

        // Calculate the remaining distance to the destination
        float distance = direction.magnitude;

        // Normalize the direction to achieve a constant speed
        Vector3 normalizedMovement = direction.normalized;

        // Calculate the movement speed per frame
        float frameSpeed = speed * Time.deltaTime;

        // If we are close enough to the destination, change direction
        if (distance <= frameSpeed) {
            goingToFinal = !goingToFinal;
        }

        // Move the object in the calculated direction and speed
        transform.Translate(normalizedMovement * frameSpeed);
    }

    private void OnCollisionEnter(Collision col) {
        // Verificar si el objeto colisionado tiene un componente transform (es un GameObject)
       
    }

    private void OnTriggerEnter(Collider col) {
        GroundDetection objetoColisionado;

        if (col.gameObject.TryGetComponent(out objetoColisionado)) {
            // Hacer que este objeto se convierta en hijo del objeto colisionado
            objetoColisionado.transform.parent.parent = transform;
        }
    }
    private void OnTriggerExit(Collider col) {
        GroundDetection objetoColisionado;

        if (col.gameObject.TryGetComponent(out objetoColisionado)) {
            // Hacer que este objeto se convierta en hijo del objeto colisionado
            objetoColisionado.transform.parent.parent = null;
        }
    }
}
