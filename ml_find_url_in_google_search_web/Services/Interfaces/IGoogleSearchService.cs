using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ml_find_url_in_google_search_web.Services.Interfaces
{
	public interface IGoogleSearchService
	{
		Task<List<int>> FindOccurrences(string searchTerm, string occurrenceTerm, int numToCheck);
	}
}
