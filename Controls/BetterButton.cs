using System.ComponentModel;

namespace WoodsRandomizer;

[DefaultProperty(nameof(Text))]
[DefaultEvent(nameof(Click))]
[DefaultBindingProperty(nameof(Text))]
internal class BetterButton : Button {
	public BetterButton() : base() {
		SetStyle(ControlStyles.Selectable, false);
		TabStop = false;
	}
}
