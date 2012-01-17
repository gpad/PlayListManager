using System;
using System.Collections.Generic;
using System.Net;
using RestSharp;
using RestSharp.Deserializers;
using System.Linq;

namespace PlayListManager
{
	public class RestArchive : Archive
	{
		// ReSharper disable InconsistentNaming
		public class PlayLists
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
		private readonly RestArchiveListener m_Listener;

		public RestArchive(string baseUrl, RestArchiveListener listener)
		{
			m_Listener = listener;
			m_Client = new RestClient { BaseUrl = baseUrl };
		}

		public void Connect()
		{
			var request = new RestRequest
			{
				Resource = "playlists.xml",
				RootElement = "playlists",
				XmlNamespace = "",
				RequestFormat = DataFormat.Xml,
				Method = Method.GET
			};
			m_Client.ExecuteAsync(request, OnEndDownload);
		}

		public void AddNewPlayList(string name)
		{
			var request = new RestRequest
			{
				Resource = "playlists.xml",
				RootElement = "playlist",
				XmlNamespace = "",
				RequestFormat = DataFormat.Xml,
				Method = Method.POST
			};
			request.AddBody(new playlist { name = name });
			m_Client.ExecuteAsync(request, OnEndUploadNewPlaylist);
		}

		public void Save(PlayListContent playListContent)
		{

			    var request = new RestRequest
			    {
			        Resource = "playlists/{id}.xml",
			        RootElement = "",
			        XmlNamespace = "",
			        RequestFormat = DataFormat.Xml,
			        Method = Method.PUT,
			    };
				request.AddParameter("id", playListContent.Id, ParameterType.UrlSegment);
				request.AddBody(CreateXmlPlayListFrom(playListContent));
				m_Client.ExecuteAsync(request, response => OnEndSaveModifiedPlayList(response));
		}

		private void OnEndDownload(RestResponse response)
		{
			if (response.StatusCode != HttpStatusCode.OK)
			{
				m_Listener.Error(CreateErrorDescription(response));
				return;
			}

			try
			{
				var deserializer = new XmlDeserializer { Namespace = "", RootElement = "" };
				var playLists = deserializer.Deserialize<PlayLists>(response);

				m_Listener.PlayListsDownloaded(playLists.Items.Select(CreatePlayList).ToArray());

			}
			catch (Exception exception)
			{
				m_Listener.Error(new ErrorDescription(response.StatusCode, response.StatusDescription, response.Content, exception));
			}
		}

		private static ErrorDescription CreateErrorDescription(RestResponse response)
		{
			return new ErrorDescription(response.StatusCode, response.StatusDescription, response.Content, null);
		}

		private void OnEndUploadNewPlaylist(RestResponse response)
		{
			if (response.StatusCode != HttpStatusCode.Created)
			{
				m_Listener.Error(CreateErrorDescription(response));
				return;
			}

			var deserializer = new XmlDeserializer { Namespace = "", RootElement = "" };
			var playList = deserializer.Deserialize<playlist>(response);

			m_Listener.NewPlayListUploaded(CreatePlayList(playList));
		}

		private PlayList CreatePlayList(playlist playList)
		{
			return new PlayList(new PlayListContent(playList.id, playList.name, playList.images.Select(CreatePlayListItem).ToArray()), this);
		}

		private static PlayListItem CreatePlayListItem(image img)
		{
			return new PlayListItem(new ImageId(img.id), img.name);
		}

		private static playlist CreateXmlPlayListFrom(PlayListContent playlist)
		{
			return new playlist
			{
				id = playlist.Id,
				name = playlist.Name,
				images = new List<image>(playlist.Items.Select(item => new image { id = item.ImageId.Id }))
			};
		}

		private void OnEndSaveModifiedPlayList(RestResponse response)
		{
			if (response.StatusCode != HttpStatusCode.OK)
			{
				m_Listener.Error(CreateErrorDescription(response));
				return;
			}
		}
	}
}