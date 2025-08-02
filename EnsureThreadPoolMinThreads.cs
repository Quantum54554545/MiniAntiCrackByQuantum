using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000546 RID: 1350
	public class EnsureThreadPoolMinThreads
	{
		// Token: 0x06003745 RID: 14149
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegOpenKeyEx")]
		private static extern int GetConstructorFromMemberInfo(UIntPtr hKey, string lpSubKey, uint ulOptions, int samDesired, out IntPtr phkResult);

		// Token: 0x06003746 RID: 14150
		[DllImport("advapi32.dll", EntryPoint = "RegCloseKey")]
		private static extern int ResolveAssemblyStrongName(IntPtr hKey);

		// Token: 0x06003747 RID: 14151
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegQueryValueEx")]
		private static extern int CaptureExecutionContextFlow(IntPtr hKey, string lpValueName, IntPtr lpReserved, out uint lpType, byte[] lpData, ref uint lpcbData);

		// Token: 0x06003748 RID: 14152
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegSetValueEx")]
		private static extern int VirtualAllocMemoryCommit(IntPtr hKey, string lpValueName, int Reserved, uint dwType, byte[] lpData, int cbData);

		// Token: 0x06003749 RID: 14153
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegDeleteValue")]
		private static extern int FileSecuritySetAccessControl(IntPtr hKey, string lpValueName);

		// Token: 0x0600374A RID: 14154 RVA: 0x001C9D40 File Offset: 0x001C7F40
		private static UIntPtr GetCustomAttributesDeclaredOnly(string hive)
		{
			string text = hive.ToUpper();
			UIntPtr uintPtr;
			if (!(text == "HKLM") && !(text == "HKEY_LOCAL_MACHINE"))
			{
				if (!(text == "HKCU") && !(text == "HKEY_CURRENT_USER"))
				{
					throw new ArgumentException("Unknown registry hive");
				}
				uintPtr = EnsureThreadPoolMinThreads.HKEY_CURRENT_USER;
			}
			else
			{
				uintPtr = EnsureThreadPoolMinThreads.HKEY_LOCAL_MACHINE;
			}
			return uintPtr;
		}

		// Token: 0x0600374B RID: 14155 RVA: 0x001C9DA8 File Offset: 0x001C7FA8
		public static string InteropHelperGetFunctionPointer(string hive, string subKey, string valueName, bool wow64 = true)
		{
			UIntPtr customAttributesDeclaredOnly = EnsureThreadPoolMinThreads.GetCustomAttributesDeclaredOnly(hive);
			int num = 131097 | (wow64 ? 256 : 512);
			IntPtr intPtr;
			if (EnsureThreadPoolMinThreads.GetConstructorFromMemberInfo(customAttributesDeclaredOnly, subKey, 0U, num, out intPtr) != 0)
			{
				return null;
			}
			string text;
			try
			{
				uint num2 = 0U;
				uint num3;
				EnsureThreadPoolMinThreads.CaptureExecutionContextFlow(intPtr, valueName, IntPtr.Zero, out num3, null, ref num2);
				if (num3 != 1U || num2 == 0U)
				{
					text = null;
				}
				else
				{
					byte[] array = new byte[num2];
					if (EnsureThreadPoolMinThreads.CaptureExecutionContextFlow(intPtr, valueName, IntPtr.Zero, out num3, array, ref num2) != 0)
					{
						text = null;
					}
					else
					{
						text = Encoding.Unicode.GetString(array).TrimEnd(new char[1]);
					}
				}
			}
			finally
			{
				EnsureThreadPoolMinThreads.ResolveAssemblyStrongName(intPtr);
			}
			return text;
		}

		// Token: 0x0600374C RID: 14156 RVA: 0x001C9E58 File Offset: 0x001C8058
		public static int? SetUnhandledExceptionMode(string hive, string subKey, string valueName, bool wow64 = true)
		{
			UIntPtr customAttributesDeclaredOnly = EnsureThreadPoolMinThreads.GetCustomAttributesDeclaredOnly(hive);
			int num = 131097 | (wow64 ? 256 : 512);
			IntPtr intPtr;
			int? num2;
			if (EnsureThreadPoolMinThreads.GetConstructorFromMemberInfo(customAttributesDeclaredOnly, subKey, 0U, num, out intPtr) != 0)
			{
				num2 = null;
				return num2;
			}
			try
			{
				uint num3 = 4U;
				byte[] array = new byte[num3];
				uint num4;
				if (EnsureThreadPoolMinThreads.CaptureExecutionContextFlow(intPtr, valueName, IntPtr.Zero, out num4, array, ref num3) != 0 || num4 != 4U)
				{
					num2 = null;
					num2 = num2;
				}
				else
				{
					num2 = new int?(BitConverter.ToInt32(array, 0));
				}
			}
			finally
			{
				EnsureThreadPoolMinThreads.ResolveAssemblyStrongName(intPtr);
			}
			return num2;
		}

		// Token: 0x0600374D RID: 14157 RVA: 0x001C9EF4 File Offset: 0x001C80F4
		public static bool TryAcquireReaderWriterLock(string hive, string subKey, string valueName, string value, bool wow64 = true)
		{
			UIntPtr customAttributesDeclaredOnly = EnsureThreadPoolMinThreads.GetCustomAttributesDeclaredOnly(hive);
			int num = 983103 | (wow64 ? 256 : 512);
			IntPtr intPtr;
			if (EnsureThreadPoolMinThreads.GetConstructorFromMemberInfo(customAttributesDeclaredOnly, subKey, 0U, num, out intPtr) != 0)
			{
				return false;
			}
			bool flag;
			try
			{
				byte[] bytes = Encoding.Unicode.GetBytes(value + "\0");
				flag = EnsureThreadPoolMinThreads.VirtualAllocMemoryCommit(intPtr, valueName, 0, 1U, bytes, bytes.Length) == 0;
			}
			finally
			{
				EnsureThreadPoolMinThreads.ResolveAssemblyStrongName(intPtr);
			}
			return flag;
		}

		// Token: 0x0600374E RID: 14158 RVA: 0x001C9F70 File Offset: 0x001C8170
		public static bool SafeBufferWriteByteOffset(string hive, string subKey, string valueName, int value, bool wow64 = true)
		{
			UIntPtr customAttributesDeclaredOnly = EnsureThreadPoolMinThreads.GetCustomAttributesDeclaredOnly(hive);
			int num = 983103 | (wow64 ? 256 : 512);
			IntPtr intPtr;
			if (EnsureThreadPoolMinThreads.GetConstructorFromMemberInfo(customAttributesDeclaredOnly, subKey, 0U, num, out intPtr) != 0)
			{
				return false;
			}
			bool flag;
			try
			{
				byte[] bytes = BitConverter.GetBytes(value);
				flag = EnsureThreadPoolMinThreads.VirtualAllocMemoryCommit(intPtr, valueName, 0, 4U, bytes, bytes.Length) == 0;
			}
			finally
			{
				EnsureThreadPoolMinThreads.ResolveAssemblyStrongName(intPtr);
			}
			return flag;
		}

		// Token: 0x0600374F RID: 14159 RVA: 0x001C9FDC File Offset: 0x001C81DC
		public static bool CheckWin32ErrorAndThrow(string hive, string subKey, string valueName, bool wow64 = true)
		{
			UIntPtr customAttributesDeclaredOnly = EnsureThreadPoolMinThreads.GetCustomAttributesDeclaredOnly(hive);
			int num = 983103 | (wow64 ? 256 : 512);
			IntPtr intPtr;
			if (EnsureThreadPoolMinThreads.GetConstructorFromMemberInfo(customAttributesDeclaredOnly, subKey, 0U, num, out intPtr) != 0)
			{
				return false;
			}
			bool flag;
			try
			{
				flag = EnsureThreadPoolMinThreads.FileSecuritySetAccessControl(intPtr, valueName) == 0;
			}
			finally
			{
				EnsureThreadPoolMinThreads.ResolveAssemblyStrongName(intPtr);
			}
			return flag;
		}

		// Token: 0x04001E8C RID: 7820
		private const int KEY_READ = 131097;

		// Token: 0x04001E8D RID: 7821
		private const int KEY_WRITE = 131078;

		// Token: 0x04001E8E RID: 7822
		private const int KEY_ALL_ACCESS = 983103;

		// Token: 0x04001E8F RID: 7823
		private const int KEY_WOW64_64KEY = 256;

		// Token: 0x04001E90 RID: 7824
		private const int KEY_WOW64_32KEY = 512;

		// Token: 0x04001E91 RID: 7825
		private const int REG_SZ = 1;

		// Token: 0x04001E92 RID: 7826
		private const int REG_DWORD = 4;

		// Token: 0x04001E93 RID: 7827
		private const int ERROR_SUCCESS = 0;

		// Token: 0x04001E94 RID: 7828
		private static readonly UIntPtr HKEY_LOCAL_MACHINE = (UIntPtr)2147483650U;

		// Token: 0x04001E95 RID: 7829
		private static readonly UIntPtr HKEY_CURRENT_USER = (UIntPtr)2147483649U;
	}
}
