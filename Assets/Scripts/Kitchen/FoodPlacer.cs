using UnityEngine;

using System.Collections.Generic;
using JetBrains.Annotations;
using System.Linq;

namespace CookingPrototype.Kitchen {
	public sealed class FoodPlacer : MonoBehaviour {
		public string                  FoodName = string.Empty;
		public List<AbstractFoodPlace> Places   = new List<AbstractFoodPlace>();
		public bool                    GroupPlace = false;

		[UsedImplicitly]
		public void TryPlaceFood() {
			if ( GroupPlace ) {
				var allFree = Places.Any(p => {
					var fp = p as FoodPlace;
					return fp != null && !fp.IsFree;
				});
				
				if ( allFree ) {
					return;
				}

				foreach ( var place in Places ) {
					place.TryPlaceFood(new Food(FoodName));
				}
			}
			else {
				foreach ( var place in Places ) {
					if ( place.TryPlaceFood(new Food(FoodName)) ) {
						return;
					}
				}
			}
		}
	}
}
