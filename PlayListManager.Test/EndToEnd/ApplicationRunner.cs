using System;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using NUnit.Framework;

namespace PlayListManager.Test.EndToEnd
{
	internal class ApplicationRunner
	{
		private Thread m_Thread;

		public void Start()
		{
			m_Thread = new Thread(Program.Main);
			m_Thread.Start();
		}

		public void Connect()
		{
			Sync(() => GetControl<Button>("buttonConnect").PerformClick());
		}

		public void AddNewPlayList(string name)
		{
			GetControl<TextBox>("textBoxName").Text = name;
			GetControl<Button>("buttonAddPlayList").PerformClick();
		}

		public void AddImageIdToSelectedPlayList(ImageId imageId)
		{
			GetControl<TextBox>("textBoxImageId").Text = imageId.Id.ToString();
			GetControl<Button>("buttonAddImageId").PerformClick();
		}

		public void SaveCurrentPlayList()
		{
			GetControl<Button>("buttonSavePlayList").PerformClick();
		}

		public void Stop()
		{
			Sync(() => GetMainFormDriver().Form.Close());
			if (!m_Thread.Join(TimeSpan.FromSeconds(10)))
				m_Thread.Abort();
		}

		public void HasShownPlayLists(PlayListContent[] playListsContent)
		{
			SyncAndProbe(() => Assert.That(DownloadedPlayLists(), Is.EquivalentTo(playListsContent)), 5000, 500);
		}

		public void HasSelectedPlayList(PlayListContent playListContent)
		{
			SyncAndProbe(() => Assert.That(SelectedPlayList(), Is.EqualTo(playListContent)), 5000, 500);

		}

		public void HasShownContentOf(PlayListContent playListContent)
		{
			SyncAndProbe(() => Assert.That(ShownImageIds(), Is.EquivalentTo(playListContent.Items)), 5000, 500);
		}

		public void HasSelectedPlayListWithName(string name)
		{
			SyncAndProbe(()=> Assert.That(SelectedPlayList().Name, Is.EqualTo(name)), 5000, 500);
		}

		public void HasShown(ImageId imageId)
		{
			SyncAndProbe(() => Assert.That(
				SelectedPlayList().Items.Select(pli => pli.ImageId).ToArray(), 
				Contains.Item(imageId)),
				5000, 500);
		}

		private static PlayListContent[] DownloadedPlayLists()
		{
			var combo = GetControl<ComboBox>("comboBoxArchive");
			return combo.Items.Cast<PlayListComboItem>().Select(
				plci => plci.PlayList.Content).ToArray();
		}

		private static PlayListContent SelectedPlayList()
		{
			var combo = GetControl<ComboBox>("comboBoxArchive");
			return combo.SelectedItem != null ? ((PlayListComboItem)combo.SelectedItem).PlayList.Content : null;
		}

		private static PlayListItem[] ShownImageIds()
		{
			return GetControl<ListView>("listViewPlayList").Items.Cast<ListViewItem>().Select(lvi => lvi.Tag).Cast<PlayListItem>().ToArray();
		}

		private static T GetControl<T>(string name) where T : Control
		{
			return GetMainFormDriver().GetControl<T>(name);
		}

		private static FormDriver GetMainFormDriver()
		{
			return FormDriver.GetMainForm();
		}

		private static void SyncAndProbe(Action action, int timeout, int polling)
		{
			Sync(() => Probe(action, timeout, polling));
		}

		private static void Sync(Action action)
		{
			Action act = () =>
			{
				Application.DoEvents();
				action();
			};
			GetMainFormDriver().Invoke(act);
		}

		private static void Probe(Action action, int timeout, int polling)
		{
			int start = Environment.TickCount;
			while (true)
			{
				try
				{
					action();
					return;

				}
				catch (Exception)
				{
					if ((Environment.TickCount - start) > timeout)
						throw;
					Application.DoEvents();
					Thread.Yield();
					Thread.Sleep(polling);
				}

			}
		}

	}
}