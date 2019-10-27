using UnityEngine;

namespace org.stickin.upup.menus {
    public enum MenuType {
        MAIN,
        SETTINGS,
        PLAY,
        ENDGAME,
        PAUSE
    }
    
    public class BaseMenu : MonoBehaviour {
        #region Serialized Fields
        [SerializeField] private MenuType _type;
        [SerializeField] private bool _needHideOtherMenus = true;
        [SerializeField] private Animator _animator;
        #endregion

//        #region Private Properties
//        private bool _isShow;
//        #endregion

        public bool IsShow { get; private set; }
        public MenuType Type => _type;
        public bool NeedHideOtherMenus => _needHideOtherMenus;
        
        #region Public Methods
        public void Init() {
            IsShow = false;
            gameObject.SetActive(false);
        }

        public void Show() {
            if (!IsShow) {
                IsShow = true;
                gameObject.SetActive(true);
                if (_animator != null) {
                    ResetTriggers();
                    _animator.SetTrigger("show");
                }
            }
        }

        public void Hide() {
            if (IsShow) {
                IsShow = false;
                if (_animator != null) {
                    ResetTriggers();
                    _animator.SetTrigger("hide");
                } else {
                    EndHideAnimation();
                }
            }
        }
        #endregion

        #region Events
        public void EndHideAnimation() {
            gameObject.SetActive(false);
        }
        #endregion

        #region Private Methods
        private void ResetTriggers() {
            _animator.ResetTrigger("show");
            _animator.ResetTrigger("hide");
        }
        #endregion
    }
}