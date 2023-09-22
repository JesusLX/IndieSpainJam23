using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterDetector : MonoBehaviour, ITerrainDetector {
    public Transform terrainChecker;
    public LayerMask terrainLayer;
    public float terrainCheckRadius = 0.2f;
    public bool canCheckTerrain = true;
    public bool isTerrain = true;

    public void CanCheckTerrain(bool can) {
        canCheckTerrain = can;
    }

    /// <summary>
    /// Use phisics to check if is touchin the ground
    /// </summary>
    /// <returns></returns>
    public bool IsTouching() {
        if (canCheckTerrain) {
            Collider[] objetosCercanos = Physics.OverlapSphere(terrainChecker.position, terrainCheckRadius,terrainLayer);

            foreach (Collider objeto in objetosCercanos) {
                Debug.Log("Objeto cercano: " + objeto.name);
                objeto.GetComponent<ILighter>().TurnOn();
            }
        }
        return isTerrain;
    }

    public void SetTouching(bool grounded) {
        isTerrain = grounded;
    }

    private void OnDrawGizmos() {
        // Establecer el color del gizmo
        Gizmos.color = Color.yellow;

        // Dibujar una esfera en la posici�n del objeto con el radio especificado
        Gizmos.DrawSphere(terrainChecker.position, terrainCheckRadius);
    }

    private void Update() {
        if (IsTouching()) {
            IsTouching();
        }
    }
}