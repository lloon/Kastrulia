using Kastrulia.source.core.models;
using Kastrulia.source.core.parser;
using Kastrulia.source.core.parser.worms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kastrulia.source.core.view_models
{
	public class ExampleInfoModel : BaseViewModel
	{
		public ObservableCollection<ExampleInfo> Items { get; set; }
		public Action LoadItemsCommand { get; set; }

		private ParserWorker<List<ExampleInfo>> parser;

		public bool IsNetworkError = false;

		public event Action OnComplete;
		public event Action OnStart;

		public ExampleInfoModel()
		{
			Title = "Докладна інформація по областях";
			Items = new ObservableCollection<ExampleInfo>();
			LoadItemsCommand = new Action(async () => await ExecuteLoadItemsCommand());

			try
			{
				parser = new ParserWorker<List<ExampleInfo>>(
					new ExampleWorm()
				);
			}
			catch
			{
				IsNetworkError = true;
			}

			parser.OnCompleted += Parser_OnCompleted;
			parser.OnNewData += Parser_OnNewData;
		}

		private void Parser_OnNewData(object arg1, List<ExampleInfo> fuelList)
		{
			SubTitle = fuelList.Last().Title;
			Title = fuelList.Last().Title;

			foreach (ExampleInfo info in fuelList)
			{
				Items.Add(info);
			}
		}

		public void Parser_OnCompleted(object obj)
		{
			IsNetworkError = parser.IsNetworkError;

			OnComplete();
		}

		async Task ExecuteLoadItemsCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				Items.Clear();

				OnStart();

				parser.Settings = new ExampleSettings();
				parser.Start();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
