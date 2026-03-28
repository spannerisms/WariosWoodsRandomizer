using System.Xml;

namespace WoodsRandomizer;
public partial class HelpForm : Form {
	private readonly Font normalfont;
	private readonly Font selectedfont;

	public HelpForm() {
		InitializeComponent();

		normalfont = Font;
		selectedfont = new(Font, FontStyle.Underline);
	}


	private TreeNode? displayedNode = null;

	private const string helpcss = """
<style>
dd, p { font-size: 9pt; }
dt { font-weight: bold; font-size: 9pt; }
dd { margin-bottom: 4px; margin-block-start: 1px; margin-block-end: 20em; }
h2 { font-weight: bold; font-size: 15pt; }
</style>
""";

	private void TreeView1_AfterSelect_1(object sender, TreeViewEventArgs e) {
		TreeNode n = treeView1.SelectedNode;

		if (n == displayedNode) {
			return;
		}




		if (n.Tag is string s) {
			webBrowser1.DocumentText = $"<!doctype html>\r\n<html lang=\"en-US\"><head>{helpcss}</head><body>{s}</body></html>";

			n.NodeFont = selectedfont;

			if (displayedNode is not null) {
				displayedNode.NodeFont = normalfont;
			}

			displayedNode = n;

		}
	}

	private void HelpForm_Load(object sender, EventArgs e) {
		using var v = Program.GetResourceStream("help.xml");

		if (v is null) {
			webBrowser1.DocumentText = $"Unable to load resource: help.xml";
			return;
		}

		SuspendLayout();

		DateTime xmlstart = DateTime.Now;

		var thisXml = new XmlDocument();
		thisXml.Load(v);

		var nodes = thisXml.SelectNodes("//categories/category");

		if (nodes is not null) {
			foreach (XmlNode catnode in nodes) {
				if (catnode.Attributes?["name"]?.Value is not string catname) {
					continue;
				}

				TreeNode newCatNode = new(catname);
				treeView1.Nodes.Add(newCatNode);

				foreach (XmlNode groupnode in catnode.ChildNodes) {
					var groupattrs = groupnode.Attributes;

					if (groupattrs?["name"]?.Value is not string groupname) {
						continue;
					}

					TreeNode newGroupNode = new(groupname);

					StringBuilder nodeDoc = new();
					nodeDoc.Append($"<h2>{groupname}</h2>");

					var groupdesc = groupnode.SelectNodes("description");

					if (groupdesc is not null) {
						foreach (XmlNode gdescnode in groupdesc) {
							if (!string.IsNullOrWhiteSpace(gdescnode.InnerText)) {
								nodeDoc.Append($"<p>{Reformat(gdescnode.InnerText)}</p>");
							}
						}
					}

					var groupitems = groupnode.SelectNodes("item");

					if (groupitems is not null) {
						nodeDoc.Append($"<dl>");

						foreach (XmlNode itemnode in groupnode!.ChildNodes) {
							var itemattrs = itemnode.Attributes;

							if (itemattrs?["name"]?.Value is not string itemname) {
								continue;
							}

							nodeDoc.Append($"<dt>{Reformat(itemname)}</dt>");

							if (!string.IsNullOrWhiteSpace(itemnode.InnerText)) {
								nodeDoc.Append($"<dd>{Reformat(itemnode.InnerText)}</dd>");
							}
						}

						nodeDoc.Append("</dl>");
					}

					newGroupNode.Tag = nodeDoc.ToString();
					newCatNode.Nodes.Add(newGroupNode);
				}
			}

			static string Reformat(string s) {
				StringBuilder rp = new(s);
				rp.Replace("{{", "<code>").Replace("}}", "</code>")
					.Replace("{SPLAT}", SPLAT)

					.Replace('{', '<').Replace('}', '>'); // this must be last

				return rp.ToString();
			}
		}

		TimeSpan diff = DateTime.Now - xmlstart;
		webBrowser1.DocumentText = $"Help page XML parsed in {diff.TotalSeconds:F3}s";

		ResumeLayout();
	}

	protected override void OnFormClosing(FormClosingEventArgs e) {
		if (e.CloseReason == CloseReason.UserClosing) {
			Hide();
			e.Cancel = true;
		}
	}
}
