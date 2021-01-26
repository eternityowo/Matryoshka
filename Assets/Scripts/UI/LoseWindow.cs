using UnityEngine;
using UnityEngine.UI;

using  CookingPrototype.Controllers;

using TMPro;

namespace CookingPrototype.UI {
	public sealed class LoseWindow : BaseWindow {
		public Image    GoalBar      = null;
		public TMP_Text GoalText     = null;
		public Button   ReplayButton = null;
		public Button   ExitButton   = null;

		protected override void Init() {
			base.Init();

			ReplayButton.onClick.AddListener(gc.Restart);
			ExitButton  .onClick.AddListener(gc.CloseGame);
		}

		public override void Show() {
			base.Show();

			GoalBar.fillAmount = Mathf.Clamp01((float) gc.TotalOrdersServed / gc.OrdersTarget);
			GoalText.text = $"{gc.TotalOrdersServed}/{gc.OrdersTarget}";
			
			gameObject.SetActive(true);
		}
	}
}
