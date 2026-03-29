using System.Diagnostics;
using System.Xml;

namespace WoodsRandomizer;

public partial class UpdateAnnouncer : Form {
	public int? Compare { get; private set; } = null;

	public static Version? ThisVersion { get; private set; }
	public static Version? AvailableVersion { get; private set; }


	static UpdateAnnouncer() {
		UpdateVersions();
	}

	public UpdateAnnouncer() {
		InitializeComponent();
		CheckForUpdates();
		ReleaseLink.VisitedLinkColor = ReleaseLink.LinkColor;
	}

	public static void UpdateVersions() {
		if (ThisVersion is null) {
			using var thisStream = Program.GetResourceStream("version.xml");
			ThisVersion = MakeVersion(thisStream);
		}

		if (AvailableVersion is null) {
			const string updateurl = "https://raw.githubusercontent.com/spannerisms/WariosWoodsRandomizer/main/version.xml";

			using var client = new HttpClient();
			client.Timeout = new(0, 0, 2);
			try {
				Stream? xmlgit = client.GetStreamAsync(updateurl).GetAwaiter().GetResult();
				AvailableVersion = MakeVersion(xmlgit);
			} catch {
				AvailableVersion = null;
			}
		}
	}

	private static Version? MakeVersion(Stream? stream) {
		if (stream is null) return null;

		Version? ret = null;

		try {
			XmlDocument verxml = new();
			verxml.Load(stream);
			var vnode = verxml.SelectSingleNode("//version");

			if (vnode?.Attributes is not XmlAttributeCollection attrs) return null;

			// get each version number; if any fail, give up
			if (!int.TryParse(attrs["major"]?.Value, out int maj)) return null;
			if (!int.TryParse(attrs["minor"]?.Value, out int min)) return null;
			if (!int.TryParse(attrs["build"]?.Value, out int bld)) return null;

			ret = new(maj, min, bld);
		} catch {
			ret = null;
		}

		return ret;
	}

	private const string UnknownVersion = "?.?.?";
	internal static string VersionString => ThisVersion?.ToString() ?? UnknownVersion;


	private void ReleaseLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
		Process.Start("explorer.exe", "https://github.com/spannerisms/WariosWoodsRandomizer/releases/latest");
	}
	
	public DialogResult ShowAndCheckForUpdates() {
		CheckForUpdates();
		return ShowDialog();
	}


	private void CheckForUpdates() {
		if (Compare is not null) {
			return;
		}

		UpdateVersions();

		CurrentVersionLabel.Text = ThisVersion?.ToString() ?? UnknownVersion;
		AvailableVersionLabel.Text = AvailableVersion?.ToString() ?? UnknownVersion;

		if (ThisVersion is null || AvailableVersion is null) {
			Compare = null;
			MainMessageLabel.Text = (ThisVersion, AvailableVersion) switch {
				(null, null) => "Could not determine the current version or available version.",
				(null, _) => "Could not determine the current version.",
				(_, null) => "Could not determine available version.",
				(_, _) => "????????????????????"
			};

			return;
		}

		Compare = ThisVersion.CompareTo(AvailableVersion);

		MainMessageLabel.Text = Compare switch {
			< 0 => "A new version is available.",
			  0 => "Your application is up to date.",
			> 0 => "It appears you have an unreleased version.",
		};
	}

	private record AppVersion(int Major, int Minor, int Build) {
		public override string ToString() => $"v{Major}.{Minor}.{Build}";

		public int CompareTo(AppVersion? other) {
			if (other is not AppVersion other2) {
				return -999;
			}

			int test = Major - other2.Major;

			if (test is not 0) {
				return test;
			}

			test = Minor - other2.Minor;

			if (test is not 0) {
				return test;
			}

			return Build - other2.Build;
		}
	}
}
