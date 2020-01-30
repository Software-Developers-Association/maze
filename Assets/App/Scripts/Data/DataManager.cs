public sealed class DataManager : Singleton<DataManager> {
	private AppData appData = new AppData();

	public AppData AppData {
		get {
			return this.appData;
		}
	}
}