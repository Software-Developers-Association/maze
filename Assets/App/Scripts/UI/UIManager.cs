using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public sealed class UIManager : Singleton<UIManager> {
	private Stack<UIMenu> menus = new Stack<UIMenu>();
	private Transform menuParent;

	protected override void Awake() {
		base.Awake();

		this.menuParent = new GameObject("MenuParent").transform;

		this.menuParent.SetParent(this.transform);
		this.menuParent.localPosition = Vector3.zero;
		this.menuParent.localRotation = Quaternion.identity;

		SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
		SceneManager.sceneLoaded += SceneManager_sceneLoaded;
	}

	private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1) {
		var active = this.menus.ToArray();

		this.menus.Clear();

		for(int i = 0; i < active.Length; ++i) {
			Destroy(active[i].gameObject);
		}
	}

	private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1) {
		Debug.Log(arg0.name + " to " + arg1.name);
	}

	private void Start() {
		var eventSystem = Object.FindObjectOfType<UnityEngine.EventSystems.EventSystem>();

		if(eventSystem) {
			eventSystem.transform.SetParent(this.transform);
		} else {
			var asset = Resources.Load<UnityEngine.EventSystems.EventSystem>("UI/EventSystem");

			if(asset == null) throw new System.Exception("Could not find EventSystem in UI/EventSystem!");

			eventSystem = Object.Instantiate(asset);

			eventSystem.transform.SetParent(this.transform);
		}
	}

	public static void Attach<T>(T instance) where T : UIMenu {
		foreach(var menu in Instance.menus) {
			if(menu == instance) {
				Debug.Log("Instance found " + menu.name);
				return;
			}
		}

		instance.Canvas.sortingOrder = Instance.menus.Count;

		instance.transform.SetParent(Instance.menuParent);

		Instance.menus.Push(instance);

		Debug.Log("Attaching...");
	}

	public static void Open<T>(string name) where T : UIMenu {
		var asset = Resources.Load<T>("UI/Menus/" + name);

		if(asset == null) {
			throw new System.Exception("Menu of type " + typeof(T).Name + " could not be located in UI/Menus/" + name + "!");
		}

		asset.gameObject.SetActive(false);

		if(asset.HideUnderneath) {
			foreach(var m in Instance.menus) {
				m.gameObject.SetActive(false);

				if(m.HideUnderneath) {
					break;
				}
			}
		}

		var menu = Object.Instantiate<T>(asset);

		menu.gameObject.name = name;
		menu.Canvas.sortingOrder = Instance.menus.Count;
		Instance.menus.Push(menu);

		menu.gameObject.SetActive(true);
		asset.gameObject.SetActive(true);
	}

	public static void Close() {
		if(Instance.menus.Count == 0) return;

		var menu = Instance.menus.Pop();

		Destroy(menu.gameObject);

		foreach(var m in Instance.menus) {
			m.gameObject.SetActive(true);

			if(m.HideUnderneath) break;
		}
	}                           
}