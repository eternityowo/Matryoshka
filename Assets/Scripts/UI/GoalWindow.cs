using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace CookingPrototype.UI {
	public class GoalWindow : BaseWindow {
		public TMP_Text GoalText    = null;
		public Button   OkButton    = null;

		protected override void Init() {
			base.Init();

			OkButton   .onClick.AddListener(gc.StartGame);
		}

		public override void Show () {
			base.Show();
			
			GoalText.text      = $"{gc.OrdersTarget}";
			
			gameObject.SetActive(true);

			// можно заменить на. сэкономим 4-6 (если считать пустые для разделения) строк кода. Хотя KISS
			// base.Show(() => { 
			// 		GoalText.text = $"{gc.OrdersTarget}";
			// 	});
		}
	}
}
