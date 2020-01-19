using UnityEngine;

public abstract class UIController<T> : UIMenu where T : UIView {
	private T view;

	public T View {
		get {
			return this.view;
		} set {
			this.Detach(this.view);
			this.Attach(this.view = value);
		}
	}

	protected abstract void Detach(T view);
	protected abstract void Attach(T view);
}