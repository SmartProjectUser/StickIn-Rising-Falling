using System.Collections;
using System.Collections.Generic;
using org.stickin.controllers;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButtonItem : MonoBehaviour
{
    public enum Type {
        SOUND,
        MUSIC,
        VIBRATION
    }

    [SerializeField] private Type _type;
    [SerializeField] private Button _btn;
    [SerializeField] private GameObject _onGO;
    [SerializeField] private GameObject _offGO;

    private void Start() {
        _btn.onClick.AddListener(OnClick);
    }

    private void OnEnable() {
        RefreshView();
    }

    private void RefreshView() {
        var isOn = false;
        switch (_type) {
            case Type.SOUND:
                isOn = SettingsController.Instance.IsSound;
                break;

            case Type.MUSIC:
                isOn = SettingsController.Instance.IsMusic;
                break;

            case Type.VIBRATION:
                isOn = SettingsController.Instance.IsVibration;
                break;
        }
        
        _onGO.SetActive(isOn);
        _offGO.SetActive(!isOn);
    }

    private void OnClick() {
        switch (_type) {
            case Type.SOUND:
                SettingsController.Instance.SetSound(!SettingsController.Instance.IsSound);
                break;

            case Type.MUSIC:
                SettingsController.Instance.SetMusic(!SettingsController.Instance.IsMusic);
                break;

            case Type.VIBRATION:
                SettingsController.Instance.SetVibration(!SettingsController.Instance.IsVibration);
                break;
        }
        
        RefreshView();
    }
}
