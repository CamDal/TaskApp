﻿using TaskApp.MVVM.Views;

namespace TaskApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new NavigationPage(new MainView());
	}
}
