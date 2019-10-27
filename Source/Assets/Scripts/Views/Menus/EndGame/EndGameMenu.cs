using org.stickin.controllers;
using UnityEngine;
using UnityEngine.UI;

namespace org.stickin.upup.menus {
    public class EndGameMenu : BaseMenu {
        [SerializeField] private Button _replayBtn;
        [SerializeField] private Text _scoreText;
        [SerializeField] private Text _highscoreText;
        private void Start() {
            _replayBtn.onClick.AddListener(OnClickReplay);
        }

        private void OnEnable() {
            _scoreText.text = "SCORE: " + GameplayController.Instance.Score;
            if (GameplayController.Instance.Score > SettingsController.Instance.Highscore) {
                SettingsController.Instance.SetHighscore(GameplayController.Instance.Score);
            }
            _highscoreText.text = "HIGHSCORE: " + SettingsController.Instance.Highscore;
        }

        private void OnClickReplay() {
            LoadingScreenManager.LoadScene("Game");
        }
    }
}