using org.stickin.controllers;
using UnityEngine;

public class CameraItem : MonoBehaviour {
    
    [SerializeField] private Animator _animator;
    
    #region Private Methods
    private PlayerItem _player;
    private Vector3 _pos;
    private float _distance;
    #endregion

    #region Private Methods
    private void Start() {
        _pos = transform.localPosition;
        _player = GameplayController.Instance.Player;
        _distance = _player.transform.position.y - transform.position.y;

        GameplayController.Instance.OnEndGame += OnEndGame;
    }

    private void OnEndGame() {
        _animator.SetTrigger("shake");
    }
    private void LateUpdate()
    {
        _pos.y = _player.transform.position.y - _distance;
        transform.localPosition = _pos;
    }    
    #endregion
}
