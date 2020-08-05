using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ml_find_url_in_google_search_web.Services.Interfaces;

namespace ml_find_url_in_google_search_web.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class GoogleSearchController : ControllerBase
	{
		private readonly ILogger<WeatherForecastController> _logger;
		private readonly IGoogleSearchService _googleSearcheService;

		public GoogleSearchController(
			ILogger<WeatherForecastController> logger,
			IGoogleSearchService googleSearchService)
		{
			_logger = logger;
			_googleSearcheService = googleSearchService;
		}

		[HttpGet]
		[Route("GetSEOOccurrencesResult")]
		public async Task<ActionResult<List<string>>> GetSEOOccurrencesResult(
			[FromQuery] string searchTerm,
			[FromQuery] string occurrenceTerm)
		{
			if (string.IsNullOrWhiteSpace(searchTerm) || string.IsNullOrWhiteSpace(occurrenceTerm))
			{
				return BadRequest("Please specify both terms");
			}

			var occurrencePositions = await _googleSearcheService.FindOccurrences(searchTerm, occurrenceTerm, 100);
			if (occurrencePositions.Count == 0)
			{
				return new List<string>() { "0" };
			}

			return occurrencePositions.Select(x => x.ToString()).ToList();
		}
	}
}
