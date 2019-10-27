using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace org.stickin.upup.views.game {
    public class GroundItem : MonoBehaviour {
        [SerializeField] private Transform _groundTilePrefab;

        private int _height = 20;
        private int _countTilesInHeight = 4;
        private float _scaleTile;
        private List<Transform> _freeTiles;
        private List<Transform> _tiles;
        private float x;
        
        private void Start() {
            x = 0f;
            _scaleTile = _height / (float) _countTilesInHeight;
            _tiles = new List<Transform>();
            CreateFreeTiles(50);

            GenerateGround();
        }

        private void CreateFreeTiles(int count) {
            _freeTiles = new List<Transform>();
            for(var i = 0; i < count; i++) {
                _freeTiles.Add(CreateTile());
            }
        }

        private Transform CreateTile() {
            var tile = Instantiate(_groundTilePrefab, transform);
            tile.transform.localScale = new Vector3(_scaleTile, 1, _scaleTile);
            
            return tile;
        }

        private Transform GetTile() {
            if (_freeTiles.Count > 0) {
                var tile = _freeTiles[0];
                _freeTiles.RemoveAt(0);
                return tile;
            }

            return CreateTile();
        }
        
        private void GenerateGround() {
            for (int i = 0; i < 20; i++) {
                AddLine();
            }
        }

        private void AddLine() {
            for (var i = 0; i < _countTilesInHeight; i++) {
                var tile = GetTile();
                tile.transform.localPosition = new Vector3(x, 0, (i - _countTilesInHeight / 2f + 0.5f) * _scaleTile);
                tile.transform.localScale = tile.transform.localScale * 0.98f;
            }

            x += _scaleTile;
        }
    }
}