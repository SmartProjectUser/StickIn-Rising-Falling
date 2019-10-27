using org.stickin.controllers;
using UnityEngine;
using UnityEngine.UI;

namespace org.stickin.upup.menus {
    public class MainMenu : BaseMenu {
        #region Serialized Fields
        [SerializeField] private Button _playBtn; 
        [SerializeField] private Button _settingsBtn;
        #endregion
        
        #region Private Methods
        private void Start() {
            _playBtn.onClick.AddListener(OnClickPlay);
            _settingsBtn.onClick.AddListener(OnClickSettings);
        }

        private void OnClickPlay() {
            MenusController.Instance.Show(MenuType.PLAY);
            GameplayController.Instance.StartGame();
        }
        
        private void OnClickSettings() {
            if (MenusController.Instance.IsShow(MenuType.SETTINGS)) {
                MenusController.Instance.Hide(MenuType.SETTINGS);
            } else {
                MenusController.Instance.Show(MenuType.SETTINGS);
            }
        }
        #endregion
    }
}