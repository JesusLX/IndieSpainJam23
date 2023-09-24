using isj23.UIScreen;
using System;
using UnityEngine;

namespace isj23.Managers {
    public class UIManager : Singleton<UIManager> {
        public MainMenu MainMenuPanel = null;
        public GameOverScreen GameOverPanel = null;
        public BattleScreen BattleScreenPanel = null;
        public WinScreen WinScreenPanel = null;
        public GameObject FadeOutPanel = null;
        public void Show(IUiScreen screen) {
            screen?.Show();
        }
        public void Hide(IUiScreen screen) {
            screen?.Hide();
        }

        public void MainMenu() {
            UIManager.instance.Hide(UIManager.instance.GameOverPanel);
            UIManager.instance.Show(UIManager.instance.MainMenuPanel);
            UIManager.instance.Hide(UIManager.instance.WinScreenPanel);
            //UIManager.instance.Hide(UIManager.instance.BattleScreenPanel);
        }

        /// <summary>
        /// Prepare the Canvas for the game
        /// </summary>
        public void PlayMenu() {
            UIManager.instance.Hide(UIManager.instance.GameOverPanel);
            UIManager.instance.Hide(UIManager.instance.MainMenuPanel);
            UIManager.instance.Hide(UIManager.instance.WinScreenPanel);
            //UIManager.instance.Show(UIManager.instance.BattleScreenPanel);
        }


        /// <summary>
        /// Prepare the Canvas and open the Game Over screen
        /// </summary>
        public void GameOverMenu() {

            //UIManager.instance.Hide(UIManager.instance.BattleScreenPanel);
            UIManager.instance.Hide(UIManager.instance.MainMenuPanel);
            UIManager.instance.Hide(UIManager.instance.WinScreenPanel);
            UIManager.instance.Show(UIManager.instance.GameOverPanel);
        }

        /// <summary>
        /// Prepare the Canvas and open the Game Over screen
        /// </summary>
        public void WinMenu() {

            //UIManager.instance.Hide(UIManager.instance.BattleScreenPanel);
            UIManager.instance.Hide(UIManager.instance.MainMenuPanel);
            UIManager.instance.Show(UIManager.instance.WinScreenPanel);
            UIManager.instance.Hide(UIManager.instance.GameOverPanel);
        }

        public void FadeOut() {
            FadeOutPanel.SetActive(true);
        }
    } 
}
