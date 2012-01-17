namespace PlayListManager
{
	public interface Archive
	{
		void Connect();
		void AddNewPlayList(string name);
		void Save(PlayListContent playListContent);
	}
}