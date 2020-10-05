using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChallengeING.Models;
using ChallengeING.Models.Interfaces;
using ChallengeING.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChallengeING.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountRepository _repository;

        public TransactionsController(ILogger<AccountController> logger, IAccountRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Returns a report over the previous month transactions by the expenses category.
        /// </summary>
        /// <param name="id">The account id.</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetTransactionsForAccount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<ActionResult<List<AccountTransactionReport>>> GetTransactionsForAccount(Guid id)
        {
            return await _repository.GetMonthlyReportForAccount(id);
        }
    }
}
