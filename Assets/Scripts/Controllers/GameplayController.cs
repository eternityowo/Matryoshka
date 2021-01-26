using System;

using UnityEngine;

using CookingPrototype.Kitchen;
using CookingPrototype.UI;

using JetBrains.Annotations;

namespace CookingPrototype.Controllers {
	public sealed class GameplayController : MonoBehaviour {
		public static GameplayController Instance { get; private set; }

		public GameObject TapBlock   = null;
		public BaseWindow WinWindow  = null;
		public BaseWindow LoseWindow = null;
		public BaseWindow GoalWindow = null;


		int _ordersTarget = 0;
		//private bool _isGoalWindowNotNull;

		public int OrdersTarget {
			get { return _ordersTarget; }
			set {
				_ordersTarget = value;
				TotalOrdersServedChanged?.Invoke();
			}
		}

		public int        TotalOrdersServed { get; private set; } = 0;

		public event Action TotalOrdersServedChanged;

		void Awake() {
			//_isGoalWindowNotNull = GoalWindow != null;
			if ( Instance != null ) {
				Debug.LogError("Another instance of GameplayController already exists");
			}
			Instance = this;
		}

		void Start() {
			Restart();
		}

		void OnDestroy() {
			if ( Instance == this ) {
				Instance = null;
			}
		}

		void Init() {
			TotalOrdersServed = 0;
			TotalOrdersServedChanged?.Invoke();
		}

		public void CheckGameFinish() {
			if ( CustomersController.Instance.IsComplete ) {
				EndGame(TotalOrdersServed >= OrdersTarget);
			}
		}

		void EndGame(bool win) {
			ShowWindow(win ? WinWindow : LoseWindow);
		}

		void HideWindows() {
			//TapBlock?.SetActive(false);
			//WinWindow?.Hide();
			//LoseWindow?.Hide();
			
			// correct
			// (GoalWindow != null) 
			// or
			//if (_isGoalWindowNotNull) {
			//	GoalWindow.Hide();
			//}
			
			// compact or ?pack to Dictionary?
			HideWindows(WinWindow, LoseWindow, GoalWindow);
		}

		[UsedImplicitly]
		public bool TryServeOrder(Order order) {
			if ( !CustomersController.Instance.ServeOrder(order) ) {
				return false;
			}

			TotalOrdersServed++;
			TotalOrdersServedChanged?.Invoke();
			CheckGameFinish();
			return true;
		}

		void ShowWindow(BaseWindow window) {
			Time.timeScale = 0f;
			TapBlock.SetActive(true);
			window.Show();
		}

		void HideWindows(params BaseWindow[] windows) {
			TapBlock?.SetActive(false);
			foreach ( var window in windows ) {
				window.Hide();
			}
		}

		public void StartGame() {
			Time.timeScale = 1f;
			HideWindows();
		}

		public void Restart() {
			Init();
			CustomersController.Instance.Init();
			OrdersController.Instance.Init();
			HideWindows();

			ShowWindow(GoalWindow);

			foreach ( var place in FindObjectsOfType<AbstractFoodPlace>() ) {
				place.FreePlace();
			}
		}

		public void CloseGame() {
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}
	}
}
