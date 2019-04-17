using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patcha.InvestmentWallet.Api.Requests;
using Patcha.InvestmentWallet.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class CompaniesController : Controller
    {
        #region Fields
        private readonly IMediator _mediator;
        #endregion

        #region Constructor
        public CompaniesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Actions
        // GET api/companies
        [HttpGet]
        public async Task<IEnumerable<InvestmentCompany>> Get()
        {
            return await _mediator.Send(new GetCollectionRequest<InvestmentCompany>());
        }

        // GET api/companies/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<InvestmentCompany>> Get(string id)
        {
            InvestmentCompany company = await _mediator.Send(new GetSingleRequest<InvestmentCompany>(id));
            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        // POST api/companies
        [HttpPost]
        public async Task<IActionResult> Post(InvestmentCompany company)
        {
            //if (await _mediator.Send(new CheckExistsRequest<InvestmentCompany>(company.Name)))
            //{
            //    return StatusCode((int)HttpStatusCode.Conflict);
            //}

            company = await _mediator.Send(new CreateRequest<InvestmentCompany>(company));

            return CreatedAtAction(nameof(Get), new { company.Id }, company);
        }
        #endregion
    }
}
