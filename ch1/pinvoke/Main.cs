using System;
using System.Runtime.InteropServices;

namespace ch1_pinvoke
{
	class MainClass
	{
		// DLLImport for Win32NT native call
		[DllImport("user32", CharSet=CharSet.Auto)]
		static extern int MessageBox(IntPtr hWnd, 
			String text, String caption, int options);

		// DLLImport for C `printf` statement
		[DllImport("libc")]
		static extern void printf(string message);

		static void Main(string[] args)
		{
			// Grab OS and have branching native calls for Win32NT or other (*nix)
			OperatingSystem os = Environment.OSVersion;

			if (os.Platform == PlatformID.Win32Windows 
				|| os.Platform == PlatformID.Win32NT) {
				// If Windows, use native call to have a popup window appear
				MessageBox (IntPtr.Zero, "Hello world!", "Hello world!", 0);
			} else {
				// Print to console
				printf ("Hello world!\n");
			}
		}
	}
}