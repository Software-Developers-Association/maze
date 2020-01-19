using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public abstract class UIMenu : MonoBehaviour {
	[SerializeField]
	private bool hideUnderneath = false;

	private Canvas canvas;

	public Canvas Canvas {
		get {
			if(this.canvas == null) this.canvas = this.GetComponent<Canvas>();

			return this.canvas;
		}
	}

	public bool HideUnderneath {
		get {
			return this.hideUnderneath;
		}
	}

	protected virtual void Start() {
		UIManager.Attach(this);
	}
}