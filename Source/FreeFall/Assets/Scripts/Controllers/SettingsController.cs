using UnityEngine;

namespace org.stickin.controllers {
    public class SettingsController : MonoBehaviour {
        public static SettingsController Instance { get; private set; }
        public bool IsSound { get; private set; }
        public bool IsMusic { get; private set; }
        public bool IsVibration { get; private set; }
        
        public int Highscore { get; private set; }
        public int Score { get; private set; }

        #region Public Methods
        public void SetSound(bool value) {
            IsSound = value;
            PlayerPrefs.SetInt("SoundEnabled", IsSound ? 1 : 0);
        }

        public void SetMusic(bool value) {
            IsMusic = value;
            PlayerPrefs.SetInt("MusicEnabled", IsMusic ? 1 : 0);
        }

        public void SetVibration(bool value) {
            IsVibration = value;
            PlayerPrefs.SetInt("VibrationEnabled", IsVibration ? 1 : 0);
        }

        public void VibrateButton() {
            if (IsVibration) {
                Handheld.Vibrate();
            }
        }

        public void SetHighscore(int value) {
            Highscore = value;
            PlayerPrefs.SetInt("Highscore", Highscore);
        }
        #endregion
        
        #region Private Methods
        private void Awake() {
            Instance = this;

            IsSound = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
            IsMusic = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
            IsVibration = PlayerPrefs.GetInt("VibrationEnabled", 1) == 1;
            Highscore = PlayerPrefs.GetInt("Highscore", 0);
        }
        #endregion
    }
}