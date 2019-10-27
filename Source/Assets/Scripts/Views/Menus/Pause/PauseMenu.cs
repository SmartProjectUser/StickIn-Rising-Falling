using org.stickin.controllers;
using UnityEngine;
using UnityEngine.UI;

namespace org.stickin.upup.menus {
    public class PauseMenu : BaseMenu {
        #region Serialized Fields
        [SerializeField] private Button _homeBtn; 
        [SerializeField] private Button _resumeBtn;
        #endregion
        
        #region Private Methods
        private void Start() {
            _homeBtn.onClick.AddListener(OnClickHome);
            _resumeBtn.onClick.AddListener(OnClickResume);
        }

        private void OnClickHome() {
            GameplayController.Instance.Unpause();
//            MenusController.Instance.Show(MenuType.MAIN);
            LoadingScreenManager.LoadScene("Game");
        }
        
        private void OnClickResume() {
            GameplayController.Instance.Unpause();
            MenusController.Instance.Show(MenuType.PLAY);
        }
        #endregion
    }
}