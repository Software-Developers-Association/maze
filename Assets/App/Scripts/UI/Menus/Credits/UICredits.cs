using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is an example of not using a Software Pattern. As you can see, things can get bloated and also
/// is less flexable to work with and increases code entanglement.
/// </summary>
public class UICredits : UIMenu {
	[SerializeField]
	private Text header;
	[SerializeField]
	private Localization localization;

	protected override void Start() {
		base.Start();

		// Because there is no central logic for this example,
		// localization must be done manually.
		// We could not react to changes in the language in real-time so this
		// method of applying a Localization would be janky at best.
		if(this.localization == null) return;

		Localization.Phrase phrase;
		Localization.Phrase.Local local;

		if(this.localization.GetPhrase("Header", out phrase)) {
			if(phrase.GetLocal(DataManager.Instance.AppData.Lang, out local)) {
				// no null checking is done, this is very dangerous
				this.header.text = local.Text;
			}
		}
	}

	/// <summary>
	/// This is a method that will be called outside via a UnityEvent (rigged to be a button)
	/// the issue is there is no guarentee this method is actually ever invoked. We hope the Button's
	/// onClick event is registered but now the View is over stepping it's bounds of the control flow.
	/// </summary>
	public void OnClose() {
		UIManager.OnBack();
	}
}
