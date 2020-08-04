using ml_find_url_in_google_search_web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ml_find_url_in_google_search_web.Services
{
	public class GoogleSearchService: IGoogleSearchService
	{
		private readonly HttpClient _httpClient;
		static private string _baseSearchUrl = "https://www.google.com/search?q={0}&num={1}&gws_rd=ssl";

		public GoogleSearchService(
			HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<List<int>> FindOccurrences(string searchTerm, string occurrenceTerm, int numToCheck)
		{
			try
			{
				var httpResponse = await _httpClient.GetAsync(_baseSearchUrl);
				var contents = await httpResponse.Content.ReadAsStringAsync();
				var matches = Regex.Matches(contents, @"(<div class=""kCrYT""><a href=).*?>");
				var foundIndexes = matches.Where(x => x.Value.Contains(occurrenceTerm)).Select(x => x.Index).ToList();

				return foundIndexes;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
