using System.Net;
using FastEndpoints;
using MediatR;
using SuperHeroes.Api.Endpoints.Shared;
using SuperHeroes.Application.Queries.GetFavouriteSuperheroes;

namespace SuperHeroes.Api.Endpoints;

/// <summary>
/// Represents the endpoint to get favourite superheroes
/// </summary>
public class GetFavouriteSuperheroesEndpoint : EndpointBase<GetFavouriteSuperheroesRequest, GetFavouriteSuperheroesResponse>
{
    private const string GetFavouriteSuperHeroesSummary = "Get favourite superheroes";
    
    public GetFavouriteSuperheroesEndpoint(ISender mediator, ILoggerFactory loggerFactory) 
        : base(mediator, loggerFactory, Http.GET, 
            EndpointRoutes.GetFavouriteSuperheroes, 
            GetFavouriteSuperHeroesSummary, 
            HttpStatusCode.OK)
    {
    }
}
