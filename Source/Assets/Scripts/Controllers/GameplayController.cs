using System;
using System.Collections;
using System.Collections.Generic;
using org.stickin.upup.menus;
using org.stickin.upup.views.game;
using UnityEngine;

namespace org.stickin.controllers {
    public class GameplayController : MonoBehaviour {
        public static GameplayController Instance { get; private set; }
    
        [SerializeField] private PlayerItem _player;
        [SerializeField] private GroundItem _ground;
        [SerializeField] private CameraItem _camera;
        public PlayerItem Player => _player;
        public GroundItem Ground => _ground;
        public CameraItem Camera => _camera;

        public event Action OnStartGame;
        public event Action OnEndGame;
        private bool _isPlaying;
        private float _score;
        public int Score => (int) _score;

        private void Awake()
        {
            Instance = this;
        }

        public void StartGame() {
            _isPlaying = true;
            _score = 0;
            OnStartGame?.Invoke();
        }
        
        public void EndGame() {
            if (_isPlaying) {
                _isPlaying = false;
                OnEndGame?.Invoke();
                MenusController.Instance.Show(MenuType.ENDGAME);
            }
        }

        public void Pause() {
            Time.timeScale = 0;
        }
        
        public void Unpause() {
            Time.timeScale = 1;
        }

        private void Update() {
            if (_isPlaying && Time.timeScale > 0) {
                _score += 50 * Time.deltaTime;
            }
        }
    }
}