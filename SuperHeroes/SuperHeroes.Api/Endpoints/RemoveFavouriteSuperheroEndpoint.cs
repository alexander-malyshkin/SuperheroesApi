using System.Net;
using FastEndpoints;
using MediatR;
using SuperHeroes.Api.Endpoints.Shared;
using SuperHeroes.Application.Commands.RemoveFavouriteSuperhero;

namespace SuperHeroes.Api.Endpoints;

public class RemoveFavouriteSuperheroEndpoint : EndpointBase<RemoveFavouriteSuperheroRequest, RemoveFavouriteSuperheroResponse>
{
    private const string EndpointSummary = "Remove a superhero from the user's favourites list";
    
    public RemoveFavouriteSuperheroEndpoint(ISender mediator, ILoggerFactory loggerFactory) 
        : base(mediator, loggerFactory, 
            Http.DELETE, 
            EndpointRoutes.RemoveFavouriteSuperhero, 
            EndpointSummary, 
            HttpStatusCode.NoContent)
    {
    }
}
