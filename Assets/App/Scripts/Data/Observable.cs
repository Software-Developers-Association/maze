using System.Collections.Generic;

public abstract class Observable : IObservable {
	private Dictionary<string, object> properties = new Dictionary<string, object>();
	private Dictionary<string, List<object>> listeners = new Dictionary<string, List<object>>();

	protected virtual bool GetValue(string name, out object value) {
		if(this.properties.ContainsKey(name) == false) {
			value = null;
			return false;
		}

		value = this.properties[name];
		return true;
	}

	protected virtual bool GetValue<T>(string name, out T value) {
		object o;

		if(this.GetValue(name, out o)) {
			value = (T)o;

			return true;
		}

		value = default(T);
		return false;
	}

	protected virtual void OnUpdate<T>(string name, T value) {
		if(this.properties.ContainsKey(name) == false) {
			this.properties.Add(name, null);
		}

		if(this.properties[name] != null && value != null) {
			if(this.properties[name].GetType() != value.GetType()) {
				throw new System.InvalidCastException("Cannot change the type of an existing property entry!");
			}
		}

		var pValue = this.properties[name];

		this.properties[name] = value;

		if(Equals(pValue, value) == false) {
			this.Notify(name, value);
		}
	}

	protected virtual void Notify<T>(string name, T value) {
		if(this.listeners.ContainsKey(name) == false) return;

		var listeners = this.listeners[name];

		for(int i = 0; i < listeners.Count; ++i) {
			try {
				((System.Action<T>)listeners[i]).Invoke(value);
			} catch { }
		}
	}

	public virtual void Subscribe(string property, System.Action<object> onValueChanged) {
		if(this.listeners.ContainsKey(property) == false)
			this.listeners.Add(property, new List<object>());

		this.listeners[property].Add(onValueChanged);
	}

	public virtual void Subscribe<T>(string property, System.Action<T> onValueChanged) {
		if(this.listeners.ContainsKey(property) == false)
			this.listeners.Add(property, new List<object>());

		this.listeners[property].Add(onValueChanged);
	}

	public virtual void Unsubscribe<T>(string property, System.Action<T> onValueChanged) {
		if(this.listeners.ContainsKey(property) == false) return;

		this.listeners[property].Remove(onValueChanged);
	}
}

public interface IObservable {
	void Subscribe<T>(string property, System.Action<T> onValueChanged);
	void Unsubscribe<T>(string property, System.Action<T> onValueChanged);
}