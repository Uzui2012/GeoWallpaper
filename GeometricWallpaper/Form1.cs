using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeometricWallpaper
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			pBox.ImageLocation = "";
			Wallpaper.Set(new System.Uri(""), Wallpaper.Style.Centered);
		}

		private void GenerateImage()
		{
			Random random = new Random();
			int numTri = Convert.ToInt16(numTriangles.Value);
			int[][] vectors = new int[5][];
			for(int i = 0; i < numTri; i++){
				
			}

		}
	}

	

	public sealed class Wallpaper
	{
		Wallpaper() { }

		const int SPI_SETDESKWALLPAPER = 20;
		const int SPIF_UPDATEINIFILE = 0x01;
		const int SPIF_SENDWININICHANGE = 0x02;

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

		public enum Style : int
		{
			Tiled,
			Centered,
			Stretched
		}

		public static void Set(Uri uri, Style style)
		{
			System.IO.Stream s = new System.Net.WebClient().OpenRead(uri.ToString());

			System.Drawing.Image img = System.Drawing.Image.FromStream(s);
			string tempPath = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
			img.Save(tempPath, System.Drawing.Imaging.ImageFormat.Bmp);

			RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
			if (style == Style.Stretched)
			{
				key.SetValue(@"WallpaperStyle", 2.ToString());
				key.SetValue(@"TileWallpaper", 0.ToString());
			}

			if (style == Style.Centered)
			{
				key.SetValue(@"WallpaperStyle", 1.ToString());
				key.SetValue(@"TileWallpaper", 0.ToString());
			}

			if (style == Style.Tiled)
			{
				key.SetValue(@"WallpaperStyle", 1.ToString());
				key.SetValue(@"TileWallpaper", 1.ToString());
			}

			SystemParametersInfo(SPI_SETDESKWALLPAPER,
				0,
				tempPath,
				SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
		}
	}
}

