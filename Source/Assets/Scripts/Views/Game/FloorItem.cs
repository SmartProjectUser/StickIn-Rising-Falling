using UnityEngine;

public class FloorItem : MonoBehaviour {
    [SerializeField] private MeshRenderer[] _walls;
    
    public void Init(int height, Color color, Vector3 pos) {
        transform.localPosition = pos;

        foreach (var wall in _walls) {
            wall.transform.localScale = new Vector3(wall.transform.localScale.x, height, wall.transform.localScale.z);
            wall.transform.localPosition = new Vector3(wall.transform.localPosition.x, -height / 2f, wall.transform.localPosition.z);
//            wall.transform.localEulerAngles = Vector3.zero;
            wall.material.color = color;
        }
    }
}
