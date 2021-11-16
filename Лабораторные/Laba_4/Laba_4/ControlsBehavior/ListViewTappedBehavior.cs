using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Laba_4.ControlsBehavior
{
	public class ListViewTappedBehavior : BindableObject
	{
		public static readonly BindableProperty CommandProperty =
			BindableProperty.CreateAttached(
				nameof(Command),
				typeof(ICommand),
				typeof(ListViewTappedBehavior),
				null,
				propertyChanged: OnCommandChanged);

		public ICommand Command
		{
			get => (ICommand)GetValue(CommandProperty);
			set => SetValue(CommandProperty, value);
		}

		static void OnCommandChanged(BindableObject view, object oldValue, object newValue)
		{
			var entry = view as ListView;
			if (entry == null)
				return;

			entry.ItemTapped += (sender, e) =>
			{
				var command = (newValue as ICommand);
				if (command == null)
					return;

				if (command.CanExecute(e.Item))
				{
					command.Execute(e.Item);
				}

			};
		}
	}
}
