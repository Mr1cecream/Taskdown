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
using Taskdown.Droid;

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
	partial class MainPage : Windows.UI.Xaml.Controls.Page
	{
				private void InitializeComponent()
		{
			var __resourceLocator = new global::System.Uri("file:///C:/Users/guy/dev/cs_school/Taskdown/Taskdown/Taskdown.Shared/MainPage.xaml");
			if(global::Uno.UI.ApplicationHelper.IsLoadableComponent(__resourceLocator))
			{
				global::Windows.UI.Xaml.Application.LoadComponent(this, __resourceLocator);
				return;
			}
			var nameScope = new global::Windows.UI.Xaml.NameScope();
			NameScope.SetNameScope(this, nameScope);
			base.IsParsing = true;
			// Source ..\..\..\..\..\..\..\Taskdown.Shared\MainPage.xaml (Line 1:2)
			base.Content = 
			new global::Windows.UI.Xaml.Controls.Grid
			{
				IsParsing = true,
				// Source ..\..\..\..\..\..\..\Taskdown.Shared\MainPage.xaml (Line 11:6)
				Children = 
				{
					new global::Windows.UI.Xaml.Controls.TextBlock
					{
						IsParsing = true,
						Text = "Hello, world!"/* string/, Hello, world!, TextBlock/Text */,
						Margin = new global::Windows.UI.Xaml.Thickness(20)/* Windows.UI.Xaml.Thickness/, 20, TextBlock/Margin */,
						FontSize = 30d/* double/, 30, TextBlock/FontSize */,
						// Source ..\..\..\..\..\..\..\Taskdown.Shared\MainPage.xaml (Line 12:8)
					}
					.MainPage_6039b808fdf080a0abb2fdf74590c679_XamlApply((MainPage_6039b808fdf080a0abb2fdf74590c679XamlApplyExtensions.XamlApplyHandler0)(c0 => 
					{
						global::Uno.UI.FrameworkElementHelper.SetBaseUri(c0, "file:///C:/Users/guy/dev/cs_school/Taskdown/Taskdown/Taskdown.Shared/MainPage.xaml");
						c0.CreationComplete();
					}
					))
					,
				}
			}
			.MainPage_6039b808fdf080a0abb2fdf74590c679_XamlApply((MainPage_6039b808fdf080a0abb2fdf74590c679XamlApplyExtensions.XamlApplyHandler1)(c1 => 
			{
				global::Uno.UI.FrameworkElementHelper.SetBaseUri(c1, "file:///C:/Users/guy/dev/cs_school/Taskdown/Taskdown/Taskdown.Shared/MainPage.xaml");
				c1.CreationComplete();
			}
			))
			;
			
			this
			.GenericApply((c2 => 
			{
				// Source C:\Users\guy\dev\cs_school\Taskdown\Taskdown\Taskdown.Shared\MainPage.xaml (Line 1:2)
				
				// WARNING Property c2.base does not exist on {http://schemas.microsoft.com/winfx/2006/xaml/presentation}Page, the namespace is http://www.w3.org/XML/1998/namespace. This error was considered irrelevant by the XamlFileGenerator
			}
			))
			.GenericApply((c3 => 
			{
				// Class Taskdown.MainPage
				global::Uno.UI.ResourceResolverSingleton.Instance.ApplyResource(c3, global::Windows.UI.Xaml.Controls.Page.BackgroundProperty, "ApplicationPageBackgroundThemeBrush", isThemeResourceExtension: true, isHotReloadSupported: true, context: global::Taskdown.Droid.GlobalStaticResources.__ParseContext_);
				/* _isTopLevelDictionary:False */
				this._component_0 = c3;
				global::Uno.UI.FrameworkElementHelper.SetBaseUri(c3, "file:///C:/Users/guy/dev/cs_school/Taskdown/Taskdown/Taskdown.Shared/MainPage.xaml");
				c3.CreationComplete();
			}
			))
			;
			OnInitializeCompleted();

			Bindings = new MainPage_Bindings(this);
			Loading += delegate
			{
				Bindings.UpdateResources();
			}
			;
		}
		partial void OnInitializeCompleted();
		private global::Windows.UI.Xaml.Markup.ComponentHolder _component_0_Holder = new global::Windows.UI.Xaml.Markup.ComponentHolder(isWeak: true);
		private global::Windows.UI.Xaml.Controls.Page _component_0
		{
			get
			{
				return (global::Windows.UI.Xaml.Controls.Page)_component_0_Holder.Instance;
			}
			set
			{
				_component_0_Holder.Instance = value;
			}
		}
		private interface IMainPage_Bindings
		{
			void Initialize();
			void Update();
			void UpdateResources();
			void StopTracking();
		}
		#pragma warning disable 0169 //  Suppress unused field warning in case Bindings is not used.
		private IMainPage_Bindings Bindings;
		#pragma warning restore 0169
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
		private class MainPage_Bindings : IMainPage_Bindings
		{
			#if UNO_HAS_UIELEMENT_IMPLICIT_PINNING
			private global::System.WeakReference _ownerReference;
			private MainPage Owner { get => (MainPage)_ownerReference?.Target; set => _ownerReference = new global::System.WeakReference(value); }
			#else
			private MainPage Owner { get; set; }
			#endif
			public MainPage_Bindings(MainPage owner)
			{
				Owner = owner;
			}
			void IMainPage_Bindings.Initialize()
			{
			}
			void IMainPage_Bindings.Update()
			{
				var owner = Owner;
			}
			void IMainPage_Bindings.UpdateResources()
			{
				var owner = Owner;
				owner._component_0.UpdateResourceBindings();
			}
			void IMainPage_Bindings.StopTracking()
			{
			}
		}

	}
}
namespace Taskdown.Droid
{
	static class MainPage_6039b808fdf080a0abb2fdf74590c679XamlApplyExtensions
	{
		public delegate void XamlApplyHandler0(global::Windows.UI.Xaml.Controls.TextBlock instance);
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Windows.UI.Xaml.Controls.TextBlock MainPage_6039b808fdf080a0abb2fdf74590c679_XamlApply(this global::Windows.UI.Xaml.Controls.TextBlock instance, XamlApplyHandler0 handler)
		{
			handler(instance);
			return instance;
		}
		public delegate void XamlApplyHandler1(global::Windows.UI.Xaml.Controls.Grid instance);
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Windows.UI.Xaml.Controls.Grid MainPage_6039b808fdf080a0abb2fdf74590c679_XamlApply(this global::Windows.UI.Xaml.Controls.Grid instance, XamlApplyHandler1 handler)
		{
			handler(instance);
			return instance;
		}
	}
}
