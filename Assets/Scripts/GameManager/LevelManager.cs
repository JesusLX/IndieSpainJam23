using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager> {
    public void LoadScene(string sceneName) {
        // Cargar una escena por su nombre
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadScene() {
        // Recargar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame() {
        // Salir del juego (solo funciona en la compilaci�n)
        Application.Quit();
    }
}
