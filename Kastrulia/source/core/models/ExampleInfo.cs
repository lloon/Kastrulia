using System.Collections.ObjectModel;

namespace Kastrulia.source.core.models
{
	public class ExampleInfo
	{
		public string Title { get; set; }
		public string Region { get; set; }
		public ObservableCollection<Operator> Operators { get; set; }
	}
}
