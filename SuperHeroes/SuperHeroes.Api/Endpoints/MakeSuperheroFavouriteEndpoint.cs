using System.Net;
using FastEndpoints;
using MediatR;
using SuperHeroes.Api.Endpoints.Shared;
using SuperHeroes.Application.Commands.MakeSuperheroFavourite;

namespace SuperHeroes.Api.Endpoints;

public class MakeSuperheroFavouriteEndpoint : EndpointBase<MakeSuperheroFavouriteRequest, MakeSuperheroFavouriteResponse>
{
    private const string MakeSuperheroFavouriteEndpointSummary = "Make a superhero favourite";
    public MakeSuperheroFavouriteEndpoint(ISender mediator, ILoggerFactory loggerFactory) 
        : base(mediator, loggerFactory, Http.POST, 
            EndpointRoutes.MakeSuperheroFavourite, 
            MakeSuperheroFavouriteEndpointSummary, 
            HttpStatusCode.Created)
    {
    }
}
