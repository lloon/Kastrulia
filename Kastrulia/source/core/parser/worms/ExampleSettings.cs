using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kastrulia.source.core.parser.worms
{
	class ExampleSettings : IParserSettings
	{
		public string BaseUrl { get; set; } = "https://index.minfin.com.ua";

		public string Prefix { get; set; } = "ua/markets/fuel/detail";
	}
}
