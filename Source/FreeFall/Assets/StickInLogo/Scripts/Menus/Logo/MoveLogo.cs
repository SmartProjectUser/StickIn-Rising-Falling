using UnityEngine;

namespace org.stickin.upup.menus {
	public class MoveLogo : MonoBehaviour {
		
		#region Serialized Fields
		[SerializeField] private Animator _animator;
		[SerializeField] private Animator _animatorEye1;
		[SerializeField] private Animator _animatorEye2;
		#endregion

		#region Private Methods
		private void Start() {
			Application.targetFrameRate = 60;

			Invoke("Move", 0.8f);
		}

		private void Move() {
			_animator.SetTrigger("run");
		}
		#endregion

		#region Animator Events
		public void Angry() {
			if (_animatorEye1 != null) {
				_animatorEye1.SetTrigger("angry");
			}

			if (_animatorEye2 != null) {
				_animatorEye2.SetTrigger("angry");
			}
		}

		public void GoNextScene() {
			LoadingScreenManager.LoadScene("Game");
		}
		#endregion
	}
}