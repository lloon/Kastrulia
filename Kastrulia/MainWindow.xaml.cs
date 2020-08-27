using Kastrulia.source.core.view_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Kastrulia
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		public ExampleInfoModel viewModel { get; set; }

		bool withSearch = false;

		public MainWindow()
        {
            InitializeComponent();

			viewModel = new ExampleInfoModel();

			main_ItemsControl.DataContext = viewModel;

			viewModel.OnComplete += ViewModel_OnComplete;
			viewModel.OnStart += ViewModel_OnStart;

			OnAppearing();
		}

		private void ViewModel_OnStart()
		{
			meow_Label.Visibility = Visibility.Visible;
		}

		private async void ViewModel_OnComplete()
		{
			meow_Label.Visibility = Visibility.Hidden;

			if (viewModel.IsNetworkError)
			{
				DialogResult dialogResult = MessageBox.Show("Помилка мережі!", "Спробувати завантажити ще раз?", MessageBoxButtons.YesNo);

				if (dialogResult == System.Windows.Forms.DialogResult.Yes)
				{
					OnAppearing();
				}
			}
		}

		protected void OnAppearing()
		{
			if (viewModel.Items.Count == 0)
				viewModel.LoadItemsCommand();
		}

		private void refresh_Button_Click(object sender, RoutedEventArgs e)
		{
			OnAppearing();
		}
	}
}
