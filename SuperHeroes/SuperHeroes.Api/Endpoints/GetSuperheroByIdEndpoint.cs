using System.Net;
using FastEndpoints;
using MediatR;
using SuperHeroes.Api.Endpoints.Shared;
using SuperHeroes.Application.Queries.GetSuperheroById;

namespace SuperHeroes.Api.Endpoints;

/// <summary>
/// Represents the endpoint to get a superhero by id
/// </summary>
public class GetSuperheroByIdEndpoint : EndpointBase<GetSuperheroByIdRequest, GetSuperheroByIdResponse>
{
    private const string GetSuperheroByIdSummary = "Get superhero by id";
    
    public GetSuperheroByIdEndpoint(ISender mediator, ILoggerFactory loggerFactory) 
        : base(mediator, loggerFactory, 
            Http.GET, 
            EndpointRoutes.GetSuperheroesById,
            GetSuperheroByIdSummary, 
            HttpStatusCode.OK)
    {
    }
}
