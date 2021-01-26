using UnityEngine;
using UnityEngine.UI;

using CookingPrototype.Controllers;

using TMPro;

namespace CookingPrototype.UI {
	public sealed class WinWindow : BaseWindow {
		public Image    GoalBar     = null;
		public TMP_Text GoalText    = null;
		public Button   OkButton    = null;

		protected override void Init() {
			base.Init();

			OkButton   .onClick.AddListener(gc.CloseGame);
		}

		public override void Show () {
			base.Show();

			GoalText.text      = $"{gc.TotalOrdersServed}/{gc.OrdersTarget}";
			GoalBar.fillAmount = Mathf.Clamp01((float) gc.TotalOrdersServed / gc.OrdersTarget);
			
			gameObject.SetActive(true);
		}
	}
}
