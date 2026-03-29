using System.ComponentModel;

namespace WoodsRandomizer;

[DefaultProperty(nameof(Value))]
[DefaultEvent(nameof(ValueChanged))]
[DefaultBindingProperty(nameof(Value))]
internal class BetterUpDown : NumericUpDown {
	public BetterUpDown() : base() { }

	public new decimal Value {
		get => base.Value;
		set => base.Value = decimal.Clamp(decimal.Round(value, DecimalPlaces, MidpointRounding.ToZero), Minimum, Maximum);
	}

	protected override void OnKeyDown(KeyEventArgs e) {
		base.OnKeyDown(e);
		if (e.KeyCode is Keys.Enter) {
			e.SuppressKeyPress = true;
		}

	}

	protected override void OnMouseWheel(MouseEventArgs e) {
		if (e is HandledMouseEventArgs f) {
			f.Handled = true;
		}

		if (e.Delta > 0) {
			UpButton();
		} else if (e.Delta < 0) {
			DownButton();
		}

		base.OnMouseWheel(e);
	}
}
