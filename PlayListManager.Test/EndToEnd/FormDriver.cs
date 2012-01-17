using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PlayListManager.Test.EndToEnd
{
	internal class FormDriver
	{
		public FormDriver(Form form)
		{
			Form = form;
		}

		public Form Form { get; private set; }

		public static FormDriver GetFormByName(string name)
		{
			try
			{
				var start = DateTime.Now;
				Form ret = null;
				while (ret == null)
				{
					Thread.Yield();
					ret = Application.OpenForms.Cast<Form>().Where(form => form.Name == name).FirstOrDefault();
					if ((DateTime.Now - start) > TimeSpan.FromSeconds(5))
						throw new ApplicationException(string.Format("Unable to find Form with name: '{0}'", name));
				}
				return new FormDriver(ret);
			}
			catch (Exception ex)
			{
				Console.WriteLine(@"GetMainForm exception {0}", ex);

				throw;
			}
		}

		public static FormDriver GetMainForm()
		{
			try
			{
				var currentProcess = Process.GetCurrentProcess();
				var start = DateTime.Now;
				Form ret = null;
				while (ret == null)
				{
					Thread.Yield();
					ret = Application.OpenForms.Cast<Form>().Where(form => form.Handle == currentProcess.MainWindowHandle).FirstOrDefault();
					if ((DateTime.Now - start) > TimeSpan.FromSeconds(5))
						throw new ApplicationException("Unable to find GetMainForm");
				}
				return new FormDriver(ret);
			}
			catch (Exception ex)
			{
				Console.WriteLine(@"GetMainForm exception {0}", ex);
				throw;
			}
		}

		public T GetControl<T>(string name) where T : Control
		{
			var ctrl = GetControl<T>(Form, name);
			if (ctrl == null)
				throw new ApplicationException(string.Format("Unable to find control {0}:'{1}'", typeof(T), name));
			return ctrl;
		}

		private static T GetControl<T>(Control parent, string name) where T : Control
		{
			if (parent.Name == name)
				return (T)parent;
			foreach (Control ctrl in parent.Controls)
			{
				if (ctrl.Name == name)
					return (T)ctrl;
				var ret = GetControl<T>(ctrl, name);
				if (ret != null)
					return ret;
			}
			return null;
		}

		public void Invoke(Action act)
		{
			Form.Invoke(act);
		}
	}
}