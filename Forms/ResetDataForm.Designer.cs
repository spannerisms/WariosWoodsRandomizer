namespace WoodsRandomizer;

partial class ResetDataForm {
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
		TheCancelButton = new Button();
		OkButton = new Button();
		CheckBoxFlowPanel = new FlowLayoutPanel();
		SuspendLayout();
		// 
		// TheCancelButton
		// 
		TheCancelButton.Anchor =  AnchorStyles.Bottom | AnchorStyles.Right;
		TheCancelButton.DialogResult = DialogResult.Cancel;
		TheCancelButton.Location = new Point(340, 282);
		TheCancelButton.Name = "TheCancelButton";
		TheCancelButton.Size = new Size(75, 23);
		TheCancelButton.TabIndex = 1;
		TheCancelButton.Text = "Cancel";
		TheCancelButton.UseVisualStyleBackColor = true;
		// 
		// OkButton
		// 
		OkButton.Anchor =  AnchorStyles.Bottom | AnchorStyles.Right;
		OkButton.DialogResult = DialogResult.OK;
		OkButton.Location = new Point(259, 282);
		OkButton.Name = "OkButton";
		OkButton.Size = new Size(75, 23);
		OkButton.TabIndex = 2;
		OkButton.Text = "OK";
		OkButton.UseVisualStyleBackColor = true;
		// 
		// CheckBoxFlowPanel
		// 
		CheckBoxFlowPanel.FlowDirection = FlowDirection.TopDown;
		CheckBoxFlowPanel.Location = new Point(0, 0);
		CheckBoxFlowPanel.Name = "CheckBoxFlowPanel";
		CheckBoxFlowPanel.Padding = new Padding(3);
		CheckBoxFlowPanel.Size = new Size(418, 278);
		CheckBoxFlowPanel.TabIndex = 3;
		// 
		// ResetDataForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		CancelButton = TheCancelButton;
		ClientSize = new Size(418, 308);
		Controls.Add(CheckBoxFlowPanel);
		Controls.Add(OkButton);
		Controls.Add(TheCancelButton);
		FormBorderStyle = FormBorderStyle.FixedSingle;
		MaximizeBox = false;
		MinimizeBox = false;
		Name = "ResetDataForm";
		Text = "Reset settings";
		ResumeLayout(false);
	}

	#endregion
	private Button TheCancelButton;
	private Button OkButton;
	private FlowLayoutPanel CheckBoxFlowPanel;
}