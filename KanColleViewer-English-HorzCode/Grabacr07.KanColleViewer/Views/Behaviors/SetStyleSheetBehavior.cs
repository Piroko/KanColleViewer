﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Navigation;
using Grabacr07.Desktop.Metro;
using Grabacr07.KanColleViewer.Properties;
using mshtml;

namespace Grabacr07.KanColleViewer.Views.Behaviors
{
	internal class SetStyleSheetBehavior : Behavior<WebBrowser>
	{
		protected override void OnAttached()
		{
			base.OnAttached();
			this.AssociatedObject.LoadCompleted += this.AssociatedObjectOnNavigated;
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.AssociatedObject.LoadCompleted -= this.AssociatedObjectOnNavigated;
		}

		private void AssociatedObjectOnNavigated(object sender, NavigationEventArgs navigationEventArgs)
		{
			this.SetStyleSheet();
		}


		private void SetStyleSheet()
		{
			var document = this.AssociatedObject.Document as HTMLDocument;
			if (document == null) return;

			var gameFrame = document.getElementById("game_frame");
			if (gameFrame == null) return;

			var target = gameFrame.document as HTMLDocument;
			if (target != null)
			{
				target.createStyleSheet().cssText = Properties.Settings.Default.OverrideStyleSheet;

				var dpi = this.AssociatedObject.GetSystemDpi();

				this.AssociatedObject.Width = 800 / dpi.ScaleX;
				this.AssociatedObject.Height = 480 / dpi.ScaleY;
			}
		}
	}
}
