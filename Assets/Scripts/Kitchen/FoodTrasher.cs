using System;

using UnityEngine;

using JetBrains.Annotations;

namespace CookingPrototype.Kitchen {
	[RequireComponent(typeof(FoodPlace))]
	public sealed class FoodTrasher : MonoBehaviour {

		FoodPlace _place = null;
		float     _timer = 0f;
		float     _reset = 0.2f;
		bool      _click = false;

		void Start() {
			_place = GetComponent<FoodPlace>();
			_timer = Time.realtimeSinceStartup;
		}

		/// <summary>
		/// Освобождает место по двойному тапу если еда на этом месте сгоревшая.
		/// </summary>
		[UsedImplicitly]
		public void TryTrashFood() {
			if(_place.IsFree || _place.CurFood.CurStatus != Food.FoodStatus.Overcooked)
				return;
    
			if ( _click && _timer + _reset >= Time.realtimeSinceStartup) {
				_click = false;
				_place.FreePlace();
			}
			else {
				_click = true;
				_timer = Time.realtimeSinceStartup;
			}
		}
	}
}
