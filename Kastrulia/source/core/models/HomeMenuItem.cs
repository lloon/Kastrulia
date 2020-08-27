namespace Kastrulia.source.core.models
{
	public enum MenuItemType
	{
		Mode1,
		Mode2,
		Mode3,
		Mode4
	}
	public class HomeMenuItem
	{
		public MenuItemType Id { get; set; }

		public string Title { get; set; }

		public string SubTitle { get; set; }
	}
}
