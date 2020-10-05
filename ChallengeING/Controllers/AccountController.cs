using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChallengeING.Models.Interfaces;
using ChallengeING.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChallengeING.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountRepository _repository;

        public AccountController(ILogger<AccountController> logger, IAccountRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Returns the account associated with the passed id.
        /// </summary>
        /// <param name="id">Identification GUID</param>
        /// <returns>Account</returns>
        [HttpGet("{id}", Name = "GetAccountById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<ActionResult<AccountDTO>> GetAccountById(Guid id)
        {
            return await _repository.GetAccount(id);
        }

        /// <summary>
        /// Returns all accounts.
        /// </summary>
        /// <returns>List of accounts</returns>
        [HttpGet("accounts", Name = "GetAccounts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<ActionResult<GetAccountResponse>> GetAccounts()
        {
            return await _repository.GetAllAcounts();
        }

    }
}
