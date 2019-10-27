using System.Collections.Generic;
using org.stickin.upup.menus;
using UnityEngine;

namespace org.stickin.controllers {
    public class MenusController : MonoBehaviour {
        
        [SerializeField] private BaseMenu[] _menus;
        
        public static MenusController Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Init();
            Show(MenuType.MAIN);
        }

        #region Private Properties
        private Dictionary<MenuType, BaseMenu> _menusMap;
        #endregion
        
        #region Public Methods
        public void Init() {
            if (_menus != null) {
                _menusMap = new Dictionary<MenuType, BaseMenu>();

                foreach (var menu in _menus) {
                    _menusMap[menu.Type] = menu;
                    menu.Init();
                }
            } else {
                Debug.LogError("Fail init menus. Menus is null");
            }
        }

        public void Show(MenuType type) {
            if (_menusMap != null && _menusMap.ContainsKey(type)) {
                var menu = _menusMap[type];

                if (menu.NeedHideOtherMenus) {
                    HideMenus();
                }
                
                menu.Show();
            }
        }
        
        public void Hide(MenuType type) {
            if (_menusMap != null && _menusMap.ContainsKey(type)) {
                _menusMap[type].Hide();
            }
        }

        public bool IsShow(MenuType type) {
            if (_menusMap != null && _menusMap.ContainsKey(type)) {
                return _menusMap[type].IsShow;
            }

            return false;
        }

        #endregion
        
        #region Private Methods
        private void HideMenus() {
            foreach (var menu in _menusMap) {
                menu.Value.Hide();
            }
        }
        #endregion
    }
}