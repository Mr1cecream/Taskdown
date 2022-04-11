// <autogenerated />
#pragma warning disable 618  // Ignore obsolete members warnings
#pragma warning disable 105  // Ignore duplicate namespaces
#pragma warning disable 1591 // Ignore missing XML comment warnings
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Uno.UI;
using Uno.UI.Xaml;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Text;
using Uno.Extensions;
using Uno;
using Uno.UI.Helpers.Xaml;
using Taskdown;

#if __ANDROID__
using _View = Android.Views.View;
#elif __IOS__
using _View = UIKit.UIView;
#elif __MACOS__
using _View = AppKit.NSView;
#elif UNO_REFERENCE_API || NET461
using _View = Windows.UI.Xaml.UIElement;
#endif

namespace Taskdown
{
	[global::System.Diagnostics.CodeAnalysis.SuppressMessage("nventive.Usage", "NV0056", Justification="Generated code")]
	[global::System.Diagnostics.CodeAnalysis.SuppressMessage("nventive.Usage", "NV0058", Justification="Generated code")]
	[global::System.Diagnostics.CodeAnalysis.SuppressMessage("nventive.Usage", "NV1003", Justification="Generated code")]
	[global::System.Diagnostics.CodeAnalysis.SuppressMessage("nventive.Usage", "NV0085", Justification="Generated code")]
	[global::System.Diagnostics.CodeAnalysis.SuppressMessage("nventive.Usage", "NV2001", Justification="Generated code")]
	[global::System.Diagnostics.CodeAnalysis.SuppressMessage("nventive.Usage", "NV2003", Justification="Generated code")]
	[global::System.Diagnostics.CodeAnalysis.SuppressMessage("nventive.Usage", "NV2004", Justification="Generated code")]
	[global::System.Diagnostics.CodeAnalysis.SuppressMessage("nventive.Usage", "NV2005", Justification="Generated code")]
	partial class App : Windows.UI.Xaml.Application
	{
				private void InitializeComponent()
		{
			global::Uno.UI.RemoteControl.RemoteControlClient.Initialize(GetType());
			global::Windows.ApplicationModel.Resources.ResourceLoader.DefaultLanguage = "en-US";
			global::Windows.ApplicationModel.Resources.ResourceLoader.AddLookupAssembly(GetType().Assembly);
			global::Windows.ApplicationModel.Resources.ResourceLoader.AddLookupAssembly(typeof(global::Windows.UI.Xaml.FrameworkElement).Assembly);

			this
			.GenericApply((c0 => 
			{
				// Source C:\Users\guy\dev\cs_school\Taskdown\Taskdown\Taskdown.Shared\App.xaml (Line 1:2)
				
				// WARNING Property c0.base does not exist on {http://schemas.microsoft.com/winfx/2006/xaml/presentation}Application, the namespace is http://www.w3.org/XML/1998/namespace. This error was considered irrelevant by the XamlFileGenerator
			}
			))
			;

			global::Taskdown.GlobalStaticResources.Initialize();
			global::Taskdown.GlobalStaticResources.RegisterResourceDictionariesBySourceLocal();
			global::Uno.UI.DataBinding.BindableMetadata.Provider = new global::Taskdown.BindableMetadataProvider();
			#if __ANDROID__
			global::Uno.Helpers.DrawableHelper.Drawables = typeof(global::Taskdown.Resource.Drawable);
			#endif
			// MergedDictionaries
			Resources.MergedDictionaries.Add(
			new global::Microsoft.UI.Xaml.Controls.XamlControlsResourcesV2()
			{
			}
			);
			// Source ..\..\..\..\..\..\..\Taskdown.Shared\App.xaml (Line 1:2)
		}

	}
}
namespace Taskdown
{
	static class App_191f5e0ab54f5b8ad66769cc05519ec3XamlApplyExtensions
	{
	}
}
