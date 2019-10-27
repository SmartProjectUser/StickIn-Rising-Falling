using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundItem : MonoBehaviour {
    [SerializeField] private BackgroundData[] _data;
    [SerializeField] private Image _imageBg;
    [SerializeField] private Image _imageGradient;

    private void Start() {
        RandomColor(false);
    }

    private void RandomColor(bool withAnim = true) {
        if (_data != null && _data.Length > 0) {
            var rndIndex = Random.Range(0, _data.Length);
//            rndIndex = 0;
            var bgData = _data[rndIndex];

            if (withAnim) {
                StartCoroutine(ToColors(bgData.ColorBg, bgData.ColorGradient));
            } else {
                _imageBg.color = bgData.ColorBg;
                _imageGradient.color = bgData.ColorGradient;
            }
        }
    }

    public IEnumerator ToColors(Color colorBg, Color colorGradient) {
        var ElapsedTime = 0.0f;
        var TotalTime = 0.5f;
        
        while (ElapsedTime < TotalTime) {
            ElapsedTime += Time.deltaTime;
            _imageBg.color = Color.Lerp(_imageBg.color, colorBg, (ElapsedTime / TotalTime));
            _imageGradient.color = Color.Lerp(_imageGradient.color, colorGradient, (ElapsedTime / TotalTime));
            yield return null;
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.C)) {
            RandomColor();
        }
    }
}
