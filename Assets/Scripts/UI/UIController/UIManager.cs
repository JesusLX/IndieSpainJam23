using isj23.UIScreen;

namespace isj23.Managers {
    public class UIManager : Singleton<UIManager> {
        public MainMenu MainMenuPanel;
        public GameOverScreen GameOverPanel;
        public BattleScreen BattleScreenPanel;

        public void Show(IUiScreen screen) {
            screen.Show();
        }
        public void Hide(IUiScreen screen) {
            screen.Hide();
        }

        public void MainMenu() {
            //UIManager.instance.Hide(UIManager.instance.GameOverPanel);
            UIManager.instance.Show(UIManager.instance.MainMenuPanel);
            //UIManager.instance.Hide(UIManager.instance.BattleScreenPanel);
        }

        /// <summary>
        /// Prepare the Canvas for the game
        /// </summary>
        public void PlayMenu() {
            //UIManager.instance.Hide(UIManager.instance.GameOverPanel);
            UIManager.instance.Hide(UIManager.instance.MainMenuPanel);
            //UIManager.instance.Show(UIManager.instance.BattleScreenPanel);
        }


        /// <summary>
        /// Prepare the Canvas and open the Game Over screen
        /// </summary>
        public void GameOverMenu() {

            //UIManager.instance.Hide(UIManager.instance.BattleScreenPanel);
            UIManager.instance.Hide(UIManager.instance.MainMenuPanel);
            //UIManager.instance.Show(UIManager.instance.GameOverPanel);
        }
    } 
}
