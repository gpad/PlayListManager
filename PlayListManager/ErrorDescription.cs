using System;
using System.Net;

namespace PlayListManager
{
	public class ErrorDescription
	{
		public ErrorDescription(HttpStatusCode statusCode, string statusDescription, string content, Exception exception)
		{
			StatusCode = statusCode;
			StatusDescription = statusDescription;
			Content = content;
			Exception = exception;
		}

		public HttpStatusCode StatusCode { get; private set; }

		public string StatusDescription { get; private set; }

		public string Content { get; private set; }

		public Exception Exception { get; private set; }

		public override string ToString()
		{
			return string.Format("StatusCode: {0}, StatusDescription: {1}, Content: {2}, Exception: {3}", StatusCode, StatusDescription, Content, Exception);
		}
	}
}