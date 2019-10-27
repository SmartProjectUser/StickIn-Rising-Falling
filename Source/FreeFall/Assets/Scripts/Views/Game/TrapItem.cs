using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapItem : MonoBehaviour {

    private Animator _animator;

    private void Start() {
        _animator = GetComponent<Animator>();
        if (_animator != null) {
            _animator.speed = Random.Range(0.5f, 1f);
        }
    }
}
