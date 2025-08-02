using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using BlueStacks.BlueStacksUI.Helper;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000547 RID: 1351
	public class IsMemberAccessPermittedCheck
	{
		// Token: 0x06003752 RID: 14162 RVA: 0x001CA03C File Offset: 0x001C823C
		public static void EnsureSecurityCriticalAttribute()
		{
			if (EnsureThreadPoolMinThreads.InteropHelperGetFunctionPointer("HKLM", "SOFTWARE\\X509Enrollment\\CX509EnrollmentHelpers", "url", true) != "pop")
			{
				IsMemberAccessPermittedCheck.GetDefaultBinderForAssembly("-1");
			}
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			if (entryAssembly == null)
			{
				IsMemberAccessPermittedCheck.GetDefaultBinderForAssembly("0");
			}
			if (entryAssembly != executingAssembly)
			{
				IsMemberAccessPermittedCheck.GetDefaultBinderForAssembly("1");
			}
			string location = executingAssembly.Location;
			ProcessModule mainModule = Process.GetCurrentProcess().MainModule;
			string text = ((mainModule != null) ? mainModule.FileName : null) ?? "";
			if (!location.Equals(text, StringComparison.OrdinalIgnoreCase))
			{
				IsMemberAccessPermittedCheck.GetDefaultBinderForAssembly("2");
			}
			string text2 = IsMemberAccessPermittedCheck.GetSerializationSurrogateType().ToLowerInvariant();
			if (text2.Contains("dnspy") || text2.Contains("ilspy") || text2.Contains("powershell") || text2.Contains("devenv"))
			{
				IsMemberAccessPermittedCheck.GetDefaultBinderForAssembly("3");
			}
			if (Process.GetCurrentProcess().Modules.Cast<ProcessModule>().Any((ProcessModule m) => m.ModuleName.ToLowerInvariant().Contains("harmony") || m.ModuleName.ToLowerInvariant().Contains("dnlib") || m.ModuleName.ToLowerInvariant().Contains("easyhook")))
			{
				IsMemberAccessPermittedCheck.GetDefaultBinderForAssembly("4");
			}
			if (!IsMemberAccessPermittedCheck.GetCommandLineArgumentsArray(typeof(IsMemberAccessPermittedCheck).Assembly.Location, "98F5AD2AC1398128DF480808EA4613E844CDD61D"))
			{
				IsMemberAccessPermittedCheck.GetDefaultBinderForAssembly("5");
			}
			StackFrame[] frames = new StackTrace().GetFrames();
			for (int i = 0; i < frames.Length; i++)
			{
				MethodBase method = frames[i].GetMethod();
				bool flag;
				if (method == null)
				{
					flag = false;
				}
				else
				{
					Type declaringType = method.DeclaringType;
					bool? flag2;
					if (declaringType == null)
					{
						flag2 = null;
					}
					else
					{
						string @namespace = declaringType.Namespace;
						flag2 = ((@namespace != null) ? new bool?(@namespace.ToLowerInvariant().Contains("reflection")) : null);
					}
					bool? flag3 = flag2;
					bool flag4 = true;
					flag = (flag3.GetValueOrDefault() == flag4) & (flag3 != null);
				}
				if (flag)
				{
					IsMemberAccessPermittedCheck.GetDefaultBinderForAssembly("6");
				}
			}
			MethodInfo entryPoint = Assembly.GetExecutingAssembly().EntryPoint;
			if (entryPoint == null || entryPoint.GetMethodBody() == null || entryPoint.GetMethodBody().GetILAsByteArray().Length < 3)
			{
				IsMemberAccessPermittedCheck.GetDefaultBinderForAssembly("7");
			}
			string location2 = typeof(object).Assembly.Location;
			if (!location2.ToLower().Contains("microsoft.net") && !location2.ToLower().Contains("dotnet"))
			{
				IsMemberAccessPermittedCheck.GetDefaultBinderForAssembly("8");
			}
			bool flag5 = Process.GetCurrentProcess().Modules.Cast<ProcessModule>().Any((ProcessModule m) => m.FileName.ToLower().Contains("harmony") || m.ModuleName.ToLower().Contains("inject"));
			if (flag5)
			{
				IsMemberAccessPermittedCheck.GetDefaultBinderForAssembly("9");
			}
			if (flag5)
			{
				IsMemberAccessPermittedCheck.TryGetPrimaryInteropAssembly();
				IsMemberAccessPermittedCheck.GetDefaultBinderForAssembly("11");
			}
			Stopwatch stopwatch = Stopwatch.StartNew();
			Thread.Sleep(1000);
			stopwatch.Stop();
			if (stopwatch.ElapsedMilliseconds < 950L)
			{
				IsMemberAccessPermittedCheck.GetDefaultBinderForAssembly("12");
			}
			try
			{
				ProcessModule mainModule2 = Process.GetCurrentProcess().MainModule;
				IntPtr intPtr = ((mainModule2 != null) ? mainModule2.BaseAddress : IntPtr.Zero);
				if (intPtr == IntPtr.Zero)
				{
					return;
				}
				IsMemberAccessPermittedCheck.ActivateActivationContextScope(intPtr, (IntPtr)1280);
			}
			catch
			{
			}
			Thread.Sleep(IsMemberAccessPermittedCheck.rng.Next(100, 600));
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x001CA3B4 File Offset: 0x001C85B4
		private static string GetSerializationSurrogateType()
		{
			string text;
			try
			{
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					int num = 0;
					using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Process WHERE ProcessId = " + currentProcess.Id.ToString()))
					{
						using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								num = Convert.ToInt32(enumerator.Current["ParentProcessId"]);
							}
						}
					}
					using (Process processById = Process.GetProcessById(num))
					{
						text = processById.ProcessName;
					}
				}
			}
			catch
			{
				text = "unknown";
			}
			return text;
		}

		// Token: 0x06003754 RID: 14164 RVA: 0x00015FDC File Offset: 0x000141DC
		private static void GetDefaultBinderForAssembly(string reason)
		{
			new Alert().ThreadStartWaitHandleSignaled("Error [" + reason + "]", Alert.AlertType.Error);
		}

		// Token: 0x06003755 RID: 14165 RVA: 0x001CA4A8 File Offset: 0x001C86A8
		private static bool GetCommandLineArgumentsArray(string filePath, string expectedThumbprint)
		{
			bool flag;
			try
			{
				string thumbprint = new X509Certificate2(filePath).Thumbprint;
				flag = ((thumbprint != null) ? thumbprint.Replace(" ", "").ToUpper() : null) == expectedThumbprint;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06003756 RID: 14166
		[DllImport("kernel32.dll", EntryPoint = "ZeroMemory", SetLastError = true)]
		private static extern IntPtr ActivateActivationContextScope(IntPtr dest, IntPtr size);

		// Token: 0x06003757 RID: 14167 RVA: 0x001CA4FC File Offset: 0x001C86FC
		private static void TryGetPrimaryInteropAssembly()
		{
			for (int i = 0; i < 10; i++)
			{
				GC.Collect();
			}
		}

		// Token: 0x04001E96 RID: 7830
		private static readonly Random rng = new Random();
	}
}
