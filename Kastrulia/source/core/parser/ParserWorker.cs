using AngleSharp.Html.Parser;
using Kastrulia.source.core.parser.worms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kastrulia.source.core.parser
{
	class ParserWorker<T> where T : class
	{
		IParser<T> parser;
		IParserSettings parserSettings;

		HtmlLoader loader;

		bool isActive;
		public bool IsNetworkError = false;
		private ExampleWorm fuelParser;

		#region Properties

		public IParser<T> Parser {
			get {
				return parser;
			}
			set {
				parser = value;
			}
		}

		public IParserSettings Settings {
			get {
				return parserSettings;
			}
			set {
				parserSettings = value;
				loader = new HtmlLoader(value);
			}
		}

		public bool IsActive {
			get {
				return isActive;
			}
		}

		#endregion

		public event Action<object, T> OnNewData;
		public event Action<object> OnCompleted;

		public ParserWorker(IParser<T> parser)
		{
			this.parser = parser;
		}

		public ParserWorker(IParser<T> parser, IParserSettings parserSettings) : this(parser)
		{
			this.parserSettings = parserSettings;
		}

		public void Start()
		{
			isActive = true;
			Worker();
		}

		public void Abort()
		{
			isActive = false;
		}

		private async void Worker()
		{
			if (!isActive)
			{
				OnCompleted?.Invoke(this);
				return;
			}

			try
			{
				var source = await loader.GetSourceByPageId();
				var domParser = new HtmlParser();
				var document = await domParser.ParseDocumentAsync(source);
				var result = parser.Parse(document);

				OnNewData?.Invoke(this, result);

				IsNetworkError = false;
			}
			catch
			{
				IsNetworkError = true;
			}

			OnCompleted?.Invoke(this);

			isActive = false;
		}


	}
}
