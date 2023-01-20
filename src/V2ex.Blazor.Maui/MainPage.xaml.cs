using Microsoft.AspNetCore.Components.WebView.Maui;
using Volo.Abp.DependencyInjection;

namespace V2ex;

public partial class MainPage : ContentPage, ISingletonDependency
{
	public MainPage()
	{
		InitializeComponent();
    }

    public BlazorWebView WebView
    {
        get => _webView;
    }
}
