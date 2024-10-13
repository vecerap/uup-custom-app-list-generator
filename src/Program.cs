using System.Text;
using System.Text.RegularExpressions;

namespace WinStoreAppBuilder
{
	public static class Program
	{

		private const string TEXT_FILE = "CustomAppsList.txt";
		private const string CONFIG_FILE = "ConvertConfig.ini";
		private const string ORIGINAL_PREFIX = "original-";

		private static string[] _apps = new string[]
		{
			"WindowsStore",
			"StorePurchaseApp",
			"SecHealthUI",
			"DesktopAppInstaller",
			"Windows\\.Photos",
			"WindowsCamera",
			"WindowsNotepad",
			"Microsoft\\.Paint",
			"Microsoft\\.WindowsTerminal",
			"MicrosoftWindows\\.Client\\.WebExperience",
			"Microsoft\\.WindowsCalculator",
			"Microsoft\\.ScreenSketch",
			"windowscommunicationsapps",
			"XboxSpeechToTextOverlay",
			"XboxGameOverlay",
			"XboxIdentityProvider",
			"PowerAutomateDesktop",
			"QuickAssist",
			"ApplicationCompatibilityEnhancements",
			"MicrosoftWindows\\.CrossDevice",
			"Microsoft\\.ZuneMusic",
			"Microsoft\\.YourPhone",
			"Microsoft\\.GamingApp",
			"Microsoft\\.XboxGamingOverlay",
			"Microsoft\\.Xbox\\.TCUI",
			"Microsoft\\.WebMediaExtensions",
			"Microsoft\\.RawImageExtension",
			"Microsoft\\.HEIFImageExtension",
			"Microsoft\\.HEVCVideoExtension",
			"Microsoft\\.VP9VideoExtensions",
			"Microsoft\\.WebpImageExtension",
			"Microsoft\\.DolbyAudioExtensions",
			"Microsoft\\.AVCEncoderVideoExtension",
			"Microsoft\\.MPEG2VideoExtension"

		};

		public static void Main()
		{

			if (!File.Exists(TEXT_FILE))
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Custom app list not found.");
				Console.ResetColor();
				return;
			}

			if (File.Exists(ORIGINAL_PREFIX + TEXT_FILE))
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Original file was already modified");
				Console.ResetColor();
				return;
			}

			var lines = File.ReadAllLines(TEXT_FILE);

			var regex = BuildRegex();

			var list = new List<string>();

			foreach (var line in lines)
			{
				var match = Regex.Match(line, regex);

				if (match.Success)
				{
					string newLine = line.Replace("# ", string.Empty);
					list.Add(newLine);
				}
				else
				{
					list.Add(line);
				}
			}
			Console.ForegroundColor = ConsoleColor.Green;
			File.Move(TEXT_FILE, ORIGINAL_PREFIX + TEXT_FILE);
			File.WriteAllLines(TEXT_FILE, list, Encoding.UTF8);
			Console.WriteLine("Custom app list generated. Original file was kept...");
			EnableCustomList();
			Console.WriteLine("Custom list enabled in config");
			Console.ResetColor();
		}

		private static string BuildRegex()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("(");

			string apps = string.Join("|", _apps);

			sb.Append(apps);

			sb.Append(")");

			return sb.ToString();
		}

		private static void EnableCustomList()
		{
			var config = File.ReadAllText(CONFIG_FILE);

			var newConfig = config.Replace("CustomList   =0", "CustomList   =1");

			File.WriteAllText(CONFIG_FILE, newConfig, Encoding.UTF8);
		}
	}
}
