namespace WoodsRandomizer;

partial class HelpForm {
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	/// Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing) {
		if (disposing && (components != null)) {
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Windows Form Designer generated code

	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent() {
		treeView1 = new TreeView();
		webBrowser1 = new WebBrowser();
		SuspendLayout();
		// 
		// treeView1
		// 
		treeView1.Dock = DockStyle.Left;
		treeView1.Location = new Point(0, 0);
		treeView1.Name = "treeView1";
		treeView1.Size = new Size(128, 441);
		treeView1.TabIndex = 0;
		treeView1.AfterSelect += TreeView1_AfterSelect_1;
		// 
		// webBrowser1
		// 
		webBrowser1.AllowWebBrowserDrop = false;
		webBrowser1.Dock = DockStyle.Fill;
		webBrowser1.Location = new Point(128, 0);
		webBrowser1.Name = "webBrowser1";
		webBrowser1.Size = new Size(356, 441);
		webBrowser1.TabIndex = 0;
		webBrowser1.WebBrowserShortcutsEnabled = false;
		// 
		// HelpForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(484, 441);
		Controls.Add(webBrowser1);
		Controls.Add(treeView1);
		MaximizeBox = false;
		MaximumSize = new Size(500, 800);
		MinimumSize = new Size(500, 256);
		Name = "HelpForm";
		Text = "Help!";
		Load += HelpForm_Load;
		ResumeLayout(false);

	}

	#endregion

	private TreeView treeView1;
	private WebBrowser webBrowser1;
}