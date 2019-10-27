using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace org.stickin.upup {
	public class LoadingScreenManager : MonoBehaviour {
		#region Constants

		static string LADING_SCENE_NAME = "Loading";

		#endregion

		#region Serialized fields

		[SerializeField] private Image _fadeOverlay;

		[Header("Timing Settings")] [SerializeField]
		private float _waitOnLoadEnd = 0.2f;

		[SerializeField] private float _fadeDuration = 0.2f;

		[Header("Loading Settings")] [SerializeField]
		private ThreadPriority _loadThreadPriority;

		#endregion

		#region Private Properties

		private float _lastProgress;
		private AsyncOperation _operation;

		#endregion

		#region Static Properties

		public static string SceneToLoad1;
		public static string SceneToLoad2;

		#endregion

		#region Public Methods

		public static void LoadScene(string loadScene) {
			LoadScenes(loadScene, String.Empty);
		}

		public static void LoadScenes(string loadScene1, string loadScene2) {
			Application.backgroundLoadingPriority = ThreadPriority.High;
			SceneToLoad1 = loadScene1;
			SceneToLoad2 = loadScene2;
			SceneManager.LoadScene(LADING_SCENE_NAME, LoadSceneMode.Additive);
		}

		#endregion

		#region Private Methods

		private void Start() {
			_fadeOverlay.canvasRenderer.SetAlpha(0);
			_fadeOverlay.color = Color.black;

			StartCoroutine(LoadAsync(SceneToLoad1));
		}

		private IEnumerator LoadAsync(string loadScene) {
			yield return null;

			FadeOut();
			yield return new WaitForSeconds(_fadeDuration);

			RemoveUnusedScenes();

			StartOperation(loadScene);

			while (_operation.isDone == false) {
				yield return null;

				if (Mathf.Approximately(_operation.progress, _lastProgress) == false) {
					_lastProgress = _operation.progress;
				}
			}

			Camera.main.enabled = false;
			SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneToLoad1));
			yield return new WaitForSeconds(_waitOnLoadEnd);

			FadeIn();
			yield return new WaitForSeconds(_fadeDuration);

			SceneManager.UnloadSceneAsync(LADING_SCENE_NAME);
		}

		private void StartOperation(string loadScene) {
			Application.backgroundLoadingPriority = _loadThreadPriority;
			_operation = SceneManager.LoadSceneAsync(loadScene, LoadSceneMode.Additive);

			if (SceneToLoad2.Length > 0)
				SceneManager.LoadScene(SceneToLoad2, LoadSceneMode.Additive);
		}

		private void FadeIn() {
			_fadeOverlay.CrossFadeAlpha(0, _fadeDuration, true);
		}

		private void FadeOut() {
			_fadeOverlay.CrossFadeAlpha(1, _fadeDuration, true);
		}

		private void RemoveUnusedScenes() {
			List<string> removeScenesNames = new List<string>();

			for (int i = 0; i < SceneManager.sceneCount; i++) {
				if (SceneManager.GetSceneAt(i).name.CompareTo(LADING_SCENE_NAME) != 0)
					removeScenesNames.Add(SceneManager.GetSceneAt(i).name);
			}

			for (int i = 0; i < removeScenesNames.Count; i++) {
				SceneManager.UnloadSceneAsync(removeScenesNames[i]);
			}
		}

		#endregion
	}
}