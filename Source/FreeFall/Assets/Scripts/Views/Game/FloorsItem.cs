using System.Collections.Generic;
using org.stickin.controllers;
using UnityEngine;

public class FloorsItem : MonoBehaviour {
    [SerializeField] private FloorItem _floorItemPrefab;
    [SerializeField] private Transform _ground;
    [SerializeField] private Color[] _themeColors;
    
    private float y;
    private Color _floorColor;
    private List<FloorItem> _freeFloors;
    private List<FloorItem> _usedFloors;
    private Transform _target;
    private Transform _camera;
    
    void Start() {
        y = 20;
        
        _target = GameplayController.Instance.Player.transform;
        _camera = GameplayController.Instance.Camera.transform;

        _floorColor = _themeColors[Random.Range(0, _themeColors.Length)];
//        _floorColor = new Color(
//            Random.Range(0f, 1f), 
//            Random.Range(0f, 1f), 
//            Random.Range(0f, 1f), 
//            1f);

//        int r = (int) (_floorColor.r * 255);
//        int g = (int) (_floorColor.g * 255); 
//        int b = (int) (_floorColor.b * 255);
//        string str = r.ToString("X2") + g.ToString("X2") + b.ToString("X2");
//        
//        Debug.Log("Start color = " + str);

        GenerateFloors(30);
    }

    private void GenerateFloors(int count)
    {
        _usedFloors = new List<FloorItem>();
        _freeFloors = new List<FloorItem>(); 

        for (var i = 0; i < count; i++)
        {
            AddedFloor(Random.Range(15, 20));
        }
    }

    private void Update()
    {
        if (_target != null)
        {
            if (_target.position.y < y + 340)
            {
                AddedFloor(Random.Range(15, 20));
            }

            foreach (var floor in _usedFloors)
            {
                if (_camera.position.y + 40 < floor.transform.position.y - floor.transform.localScale.y)
                {
                    _usedFloors.Remove(floor);
                    _freeFloors.Add(floor);
                    floor.gameObject.SetActive(false);
                    break;
                }
            }
        }
    }

    private FloorItem GetFreeFloor()
    {
        if (_freeFloors.Count > 0)
        {
            var floor = _freeFloors[0];
            _freeFloors.RemoveAt(0);
            return floor;
        }

        return Instantiate(_floorItemPrefab, transform);
    }
    
    void AddedFloor(int height)
    {
        var floor = GetFreeFloor();
        floor.gameObject.SetActive(true);
        _usedFloors.Add(floor);
        floor.Init(height, _floorColor, new Vector3(0, y, 0));
        y -= height;
        GenerateRandomColor();
        
        _ground.localPosition = new Vector3(0, y, 0);
    }

    void GenerateRandomColor() {
        _floorColor += new Color(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
    }
}
