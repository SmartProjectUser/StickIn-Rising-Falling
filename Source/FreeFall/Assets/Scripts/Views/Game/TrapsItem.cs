using System.Collections;
using System.Collections.Generic;
using org.stickin.controllers;
using UnityEngine;

public class TrapsItem : MonoBehaviour
{
    [SerializeField] private TrapItem[] _trapsPrefabs;
    
    private Transform _target;
    private Transform _camera;
    
    private List<TrapItem> _freeTraps;
    private List<TrapItem> _usedTraps;
    private float _lastY;
    private bool _isGenerateTraps;


    void Start()
    {
        GeneratePullTraps();
        
        _target = GameplayController.Instance.Player.transform;
        _camera = GameplayController.Instance.Camera.transform;

        GameplayController.Instance.OnStartGame += OnStartGame;
        GameplayController.Instance.OnEndGame += OnEndGame;
    }

    private void OnStartGame() {
        _lastY = _target.transform.position.y - 100;
        _isGenerateTraps = true;
    }

    private void OnEndGame() {
        
    }

    private void GeneratePullTraps() {
        _freeTraps = new List<TrapItem>();

        for (var i = 0; i < 3; i++) {
            foreach (var prefab in _trapsPrefabs) {
                var trap = Instantiate(prefab, transform);
                _freeTraps.Add(trap);
                trap.gameObject.SetActive(false);
            }
        }
    }
    
    private void Update()
    {
        if (_isGenerateTraps && _target != null) {
            if (_target.transform.position.y - _lastY < 200) {
                GenerateTrap();
            }
        }
    }

    private void GenerateTrap() {
        var trap = GetFreeTrap();
        trap.gameObject.SetActive(true);
        trap.transform.localEulerAngles = new Vector3(0, 90 * Random.Range(0, 4), 0);
        trap.transform.position = new Vector3(0, _lastY, 0);
        _lastY -= Random.Range(50, 80);
    }

    private TrapItem GetFreeTrap() {
        if (_freeTraps != null && _freeTraps.Count > 0) {
            var rndIndex = Random.Range(0, _freeTraps.Count);
            var trap = _freeTraps[rndIndex];
            _freeTraps.RemoveAt(rndIndex);

            return trap;
        }

        return Instantiate(_trapsPrefabs[Random.Range(0, _trapsPrefabs.Length)], transform);
    }
}
