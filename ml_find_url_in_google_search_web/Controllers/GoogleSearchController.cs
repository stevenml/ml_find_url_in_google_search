using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ml_find_url_in_google_search_web.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class GoogleSearchController : ControllerBase
	{
		private readonly ILogger<WeatherForecastController> _logger;

		public GoogleSearchController(ILogger<WeatherForecastController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public async Task<List<string>> GetSEOOccurrencesResult()
		{
			return new List<string>();
		}
	}
}
