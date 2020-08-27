using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kastrulia.source.core.models;
using System.Collections.ObjectModel;

namespace Kastrulia.source.core.parser.worms
{
	class ExampleWorm : IParser<List<ExampleInfo>>
	{
		public List<ExampleInfo> Parse(IHtmlDocument document)
		{
			var List = new List<ExampleInfo>();
			var tbody = document.QuerySelectorAll("tr").Where(item => item.ParentElement != null && item.ParentElement.TagName == "TBODY");
			var caption = document.QuerySelector("caption");

			string title = caption.TextContent;

			string lastRegion = "";
			string newRegion = "";

			foreach (var tr in tbody)
			{
				var th = tr.QuerySelectorAll("th");
				if (th.Count() == 1)
				{
					newRegion = th[0].TextContent;
				}
				else
				{
					if (newRegion != "")
					{
						var operatop = new Operator();

						var td = tr.QuerySelectorAll("td");

						if (td.Count() == 7)
						{
							operatop.Name = td[0].TextContent;

							operatop.Fuel = new Fuel
							{
								A95P = td[2].TextContent,
								A95 = td[3].TextContent,
								A92 = td[4].TextContent,
								DP = td[5].TextContent,
								GAS = td[6].TextContent
							};

							if (newRegion != lastRegion)
							{
								var info = new ExampleInfo();
								info.Title = title;
								info.Region = newRegion;
								info.Operators = new ObservableCollection<Operator>();

								List.Add(info);
							}
							else
							{
								if (List.Count > 0)
								{
									List.Last().Operators.Add(operatop);
								}
							}

							lastRegion = newRegion;
						}
					}
				}
			}

			return List;
		}
	}
}
