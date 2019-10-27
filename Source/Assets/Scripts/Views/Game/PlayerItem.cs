using System.Collections;
using org.stickin.controllers;
using UnityEngine;

public class PlayerItem : MonoBehaviour {
    [SerializeField] private Animator _animator;
    [SerializeField] private float _relaxSpeed = 20;
    [SerializeField] private float _fastSpeed = 40;
    [SerializeField] private Collider[] _parts;

    private Vector3 _pos;
    private float _currentSpeed;
    private Vector2 _moveVec;
    private Rigidbody _rigBody;
    

    private void Start()
    {
        _rigBody = GetComponent<Rigidbody>();
        _pos = transform.localPosition;
        _currentSpeed = _relaxSpeed;
        GameplayController.Instance.OnStartGame += OnStartGame;
        GameplayController.Instance.OnEndGame += OnEndGame;
        
        Invoke("RandomEffect", Random.Range(3, 6));
    }

    private void OnStartGame() {
        _animator.SetTrigger("run");
        StartCoroutine(ToSpeed(_fastSpeed));
    }

    private void OnEndGame() {
        _animator.SetTrigger("idle");
    }

    private void FixedUpdate()
    {
        if (_animator.enabled) {
            _rigBody.velocity = new Vector3(_moveVec.x, -_currentSpeed, _moveVec.y);
            _moveVec = Vector2.zero;
        }
    }

    public IEnumerator ToSpeed(float newSpeed) {
        var ElapsedTime = 0.0f;
        var TotalTime = 0.3f;
        
        while (ElapsedTime < TotalTime) {
            ElapsedTime += Time.deltaTime;
            _currentSpeed = Mathf.Lerp(_currentSpeed, newSpeed, ElapsedTime / TotalTime);
            yield return null;
        }
    }

    public void Move(Vector2 pos) {
        _moveVec = pos;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Trap") {
            Dead();
            GameplayController.Instance.EndGame();
        }
    }

    private void Dead() {
        _animator.enabled = false;
        Destroy(_rigBody);

        foreach (var part in _parts) {
            part.enabled = true;
            var body = part.gameObject.AddComponent<Rigidbody>();
            body.velocity = new Vector3(0, -30, 0);
        }
    }

    private void RandomEffect() {
        _animator.SetTrigger("effect" + Random.Range(1, 5));
        
        Invoke("RandomEffect", Random.Range(5, 10));
    }
}
