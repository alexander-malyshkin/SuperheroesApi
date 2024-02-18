using System.Net;
using FastEndpoints;
using MediatR;
using SuperHeroes.Api.Endpoints.Shared;
using SuperHeroes.Application.Queries.GetSuperheroesByName;

namespace SuperHeroes.Api.Endpoints;

/// <summary>
/// Represents the endpoint to search superheroes by name
/// </summary>
public class GetSuperHeroesByNameEndpoint : EndpointBase<GetSuperheroesByNameRequest, GetSuperheroesByNameResponse>
{
    private const string GetSuperheroesByNameSummary = "Get superheroes by name";
    
    public GetSuperHeroesByNameEndpoint(ISender mediator, ILoggerFactory loggerFactory)
        : base(mediator, loggerFactory, Http.GET, EndpointRoutes.GetSuperheroesByName, GetSuperheroesByNameSummary, HttpStatusCode.OK)
    {
    }
}
