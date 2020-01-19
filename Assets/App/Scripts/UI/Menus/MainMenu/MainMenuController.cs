﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
	private MainMenuView view;

	public MainMenuView View {
		get {
			return this.view;
		} set {
			this.Attach(value);
		}
	}

	protected virtual void Attach(MainMenuView view) {
		// unsubscribe from previous view
		if(this.view != null) {
			this.view.Play.onClick.RemoveListener(this.OnPlay);
			this.view.Score.onClick.RemoveListener(this.OnScore);
			this.view.Credits.onClick.RemoveListener(this.OnCredits);
			this.view.Menu.onClick.RemoveListener(this.OnMenu);
		}

		// subscribe to new view
		this.view = view;

		this.view.Play.onClick.AddListener(this.OnPlay);
		this.view.Score.onClick.AddListener(this.OnScore);
		this.view.Credits.onClick.AddListener(this.OnCredits);
		this.view.Menu.onClick.AddListener(this.OnMenu);

		this.view.Score.interactable = false;
		this.view.Credits.interactable = false;
		this.view.Menu.interactable = false;
	}

	protected virtual void OnPlay() {
		SceneManager.LoadScene("Game");
	}

	protected virtual void OnScore() {

	}

	protected virtual void OnCredits() {

	}

	protected virtual void OnMenu() {

	}
}