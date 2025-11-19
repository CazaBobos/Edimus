using Companies.Core.Features.CreateCompany;
using Companies.Core.Features.GetCompanies;
using Companies.Core.Features.GetCompany;
using Companies.Core.Features.RemoveCompany;
using Companies.Core.Features.RestoreCompany;
using Companies.Core.Features.UpdateCompany;
using Companies.Input;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Abstractions;
using Shared.Infrastructure.Extensions;

namespace Companies.Controllers;

[ApiController]
[Authorize]
[Route("api/companies")]
public class CompaniesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CompaniesController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Finds a company by its Id
    /// </summary>
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> FindOne(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetCompanyRequest
        {
            Id = id
        }, cancellationToken);

        return Ok(response.Company);
    }

    /// <summary>
    /// Finds all companies that match the given name
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> FindMany([FromQuery] GetCompaniesInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetCompaniesRequest
        {
            Limit = input.Limit,
            Page = input.Page,
            Name = input.Name,
            Acronym = input.Acronym,
            Enabled = input.Enabled
        }, cancellationToken);

        HttpContext.Response.Headers.TryAdd(Pagination.Count, $"{response.Count}");
        HttpContext.Response.Headers.TryAdd(Pagination.Page, $"{response.Page}");
        HttpContext.Response.Headers.TryAdd(Pagination.Limit, $"{response.Limit}");

        return Ok(response.Companies);
    }

    /// <summary>
    /// Creates a new company
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCompanyInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CreateCompanyRequest
        {
            Name = input.Name,
            Slogan = input.Slogan,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response.Id);
    }

    /// <summary>
    /// Updates a company's name
    /// </summary>
    [HttpPut("{companyId}")]
    public async Task<IActionResult> Update(int companyId, [FromBody] CreateCompanyInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateCompanyRequest
        {
            Id = companyId,
            Name = input.Name,
            Slogan = input.Slogan,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Disables a company
    /// </summary>
    [HttpDelete("{companyId}")]
    public async Task<IActionResult> Remove(int companyId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new RemoveCompanyRequest
        {
            Id = companyId,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Restores a company
    /// </summary>
    [HttpPatch("{companyId}")]
    public async Task<IActionResult> Restore(int companyId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new RestoreCompanyRequest
        {
            Id = companyId,
            User = User.GetUser(),
        }, cancellationToken);

        return Ok(response);
    }
}