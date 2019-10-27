using org.stickin.controllers;
using UnityEngine;
using UnityEngine.UI;

namespace org.stickin.upup.menus {
    public class PlayMenu : BaseMenu {
        #region Serialized Fields
        [SerializeField] private Button _pauseBtn;
        [SerializeField] private Text _scoreText;
        #endregion
        
        #region Private Methods
        private void Start() {
            _pauseBtn.onClick.AddListener(OnClickPause);
        }

        private void OnClickPause() {
            GameplayController.Instance.Pause();
            MenusController.Instance.Show(MenuType.PAUSE);
        }

        private void Update() {
            _scoreText.text = GameplayController.Instance.Score.ToString();
        }

        #endregion
    }
}