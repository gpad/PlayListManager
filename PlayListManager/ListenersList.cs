using System;
using System.Collections.Generic;

namespace PlayListManager
{
	internal class ListenersList<T>
	{
		private readonly List<T> m_Listeners = new List<T>();

		public void Raise(Action<T> action)
		{
			m_Listeners.ForEach(action);
		}

		public void Add(T listener)
		{
			m_Listeners.Add(listener);
		}

		public void Remove(T listener)
		{
			m_Listeners.Remove(listener);
		}
	}
}