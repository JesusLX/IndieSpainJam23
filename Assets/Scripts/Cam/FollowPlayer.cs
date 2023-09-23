using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    public Transform character; // The character's transform
    public float sphereRadius = 5f; // The radius of the invisible sphere
    public float minY = 0f; // The minimum Y position for the camera
    public float centeringOffset = 0.2f; // Offset to center the camera slightly more


    void Update() {
        if (character != null) {
            // Get the character's position
            Vector3 characterPosition = character.position;

            // Ensure that the camera maintains the same Z position
            characterPosition.z = transform.position.z;

            // Calculate the distance between the camera and the character in the X and Y axes
            Vector2 cameraXY = new Vector2(transform.position.x, transform.position.y);
            Vector2 characterXY = new Vector2(characterPosition.x, characterPosition.y);

            float distance = Vector2.Distance(cameraXY, characterXY);

            // Calculate the direction from the character to the camera
            Vector2 direction = (cameraXY - characterXY).normalized;

            // If the character is outside the sphere, move the camera back inside with an offset
            if (distance > sphereRadius) {
                // Move the camera to the edge of the sphere with an offset in the X and Y axes
                Vector2 newPositionXY = characterXY + direction * (sphereRadius - centeringOffset);

                // Update the camera's position while preserving the Z coordinate
                transform.position = new Vector3(newPositionXY.x, newPositionXY.y, transform.position.z);
            }

            // Limit the camera's Y position
            if (transform.position.y < minY) {
                transform.position = new Vector3(transform.position.x, minY, transform.position.z);
            }
        }
    }
}
