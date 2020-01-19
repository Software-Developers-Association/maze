public sealed class DataManager : Singleton<DataManager> {
	private AppData appData = new AppData();

	public static AppData GetAppData() {
		return DataManager.Instance.appData;
	}
}