using System;
using CookingPrototype.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace CookingPrototype.UI {
	[RequireComponent(typeof(RectTransform))]
	public class BaseWindow : MonoBehaviour {

		public Button CloseButton = null;
		protected bool _isInit = false;

		protected GameplayController gc;

		protected virtual void Init() {
			gc = GameplayController.Instance;
			
			CloseButton.onClick.AddListener(gc.CloseGame);
		}

		public virtual void Show() {
			if ( !_isInit ) {
				Init();
			}
		}

		protected virtual void Show(Action customShow) {
			if ( !_isInit ) {
				Init();
			}
			
			customShow.Invoke();
			
			gameObject.SetActive(true);
		}
		
		public virtual void Hide() {
			gameObject.SetActive(false);
		}
	}
}
