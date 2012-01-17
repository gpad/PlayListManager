using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using NUnit.Framework;
using RestSharp;
using RestSharp.Deserializers;
using System.Linq;

namespace PlayListManager.Test.EndToEnd
{
	internal class FakeMediaServer
	{
		// ReSharper disable InconsistentNaming
		public class playlists
		{
			public List<playlist> Items { get; set; }
		}

		public class playlist
		{
			public int id { get; set; }
			public string name { get; set; }
			public List<image> images { get; set; }

			public override string ToString()
			{
				return string.Format("id: {0}, name: {1}", id, name);
			}
		}

		public class image
		{
			public int id { get; set; }
			public string name { get; set; }
		}
		// ReSharper restore InconsistentNaming

		private readonly RestClient m_Client;

		public FakeMediaServer()
		{
			m_Client = new RestClient("http://127.0.0.1:3000/");
		}

		public PlayListContent AddNewPlayList(NewPlayList newPlayList)
		{
			var request = new RestRequest
			{
				Resource = "playlists.xml",
				RootElement = "playlist",
				XmlNamespace = "",
				RequestFormat = DataFormat.Xml,
				Method = Method.POST
			};
			request.AddBody(new playlist { name = newPlayList.Name, images = newPlayList.ImageIds.Select(imageId => new image { id = imageId.Id }).ToList() });
			var response = m_Client.Execute(request);
			if (response.StatusCode != System.Net.HttpStatusCode.Created)
			{
				throw new ApplicationException(string.Format("Unable to addNewPlayList"));
			}

			var deserializer = new XmlDeserializer { Namespace = "", RootElement = "" };
			var savedPlayList = deserializer.Deserialize<playlist>(response);

			return new PlayListContent(savedPlayList.id, savedPlayList.name, savedPlayList.images.Select(img => new PlayListItem(new ImageId(img.id), img.name)));
		}

		public void Reset()
		{
			var request = new RestRequest
			{
				Resource = "reset.xml",
				RootElement = "",
				XmlNamespace = "",
				RequestFormat = DataFormat.Xml,
				Method = Method.POST
			};
			var response = m_Client.Execute(request);
			if (response.StatusCode != HttpStatusCode.OK)
			{
				throw new ApplicationException(string.Format("Unable to reset server"));
			}
		}

		public void HasReceivedPlayListWith(ImageId imageId)
		{
			ProbeFor(5000, 500, () =>
			                    	{

			                    		var request = new RestRequest
			                    		              	{
			                    		              		Resource = "playlists.xml",
			                    		              		RootElement = "playlists",
			                    		              		XmlNamespace = "",
			                    		              		RequestFormat = DataFormat.Xml,
			                    		              		Method = Method.GET
			                    		              	};
			                    		var response = m_Client.Execute(request);

			                    		if (response.StatusCode != HttpStatusCode.OK)
			                    		{
			                    			throw new ApplicationException(string.Format("Unable to get playlists"));
			                    		}

			                    		var deserializer = new XmlDeserializer {Namespace = "", RootElement = ""};
			                    		var playLists = deserializer.Deserialize<RestArchive.PlayLists>(response);

			                    		Assert.That(GetImageIdsOf(playLists), Contains.Item(imageId));
			                    	});
		}

		private static void ProbeFor(int timeOut, int polling, Action action)
		{
			var start = Environment.TickCount;
			while (true)
			{
				try
				{
					action();
					return;
				}
				catch (Exception)
				{
					if (Environment.TickCount - start > timeOut)
						throw;
					Thread.Sleep(polling);
				}
			}
		}

		private static ImageId[] GetImageIdsOf(RestArchive.PlayLists pls)
		{
			return pls.Items.SelectMany(pl => pl.images).Select(img => new ImageId(img.id)).ToArray();
		}
	}
}