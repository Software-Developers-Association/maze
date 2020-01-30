using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public abstract class UIMenu : MonoBehaviour {
	[SerializeField]
	private bool hideUnderneath = false;
	[SerializeField]
	private bool ignoreOnBackRequest = false;

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

	public bool IgnoreOnBackRequest {
		get {
			return this.ignoreOnBackRequest;
		}
	}

	protected virtual void Start() {
		UIManager.Attach(this);
	}

	public virtual bool OnBack() {
		return this.IgnoreOnBackRequest ? false : true;
	}

	public virtual void Close(System.Action callback) {
		callback.Invoke();
	}

	public virtual void Open() {
		this.gameObject.SetActive(true);
	}

	public virtual void Show(bool show) {
		this.gameObject.SetActive(show);
	}
}