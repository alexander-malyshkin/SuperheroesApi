This is an ASP.NET Core 8 Web API solution that allows a user to search for a Superhero, 
returning information on the Superhero and allowing a user to store favourite Superhero's. 
The user is able to get a list of favourites back from the Restful API.

## Architectural choices:
- The endpoints in Api layer are implemented as Minimal API via FastEndpoints library,
- The Application layer is implemented with CQRS approach via MediatR library,
- The projects dependencies are organized with Clean architecture in mind.

## Scalability factors:
- Asynchronous calls to the used network and database resources,
- Caching data from the external API,
- Light-weight endpoints (no MVC infrastructure),
- (not implemented) pooling for DbContext,
- (not implemented) caching for the repository,
- (not implemented) caching for the incoming http requests.

The following features are implemented:

## Queries:
- **Get a superhero by Id**
  Fetches a superhero from an external API or Redis cache and returns him/her with a populated property 'Favourite'.

- **Search for superheroes by name**
  Fetches an array of superheroes from the external API or Redis cache and returns them with a populated property 'Favourite'.

- **Getting all favourite superheroes**
  Fetches an array of favourite superheroes from the app database, looks up each of them in the external API or Redis cache and returns them with a populated property 'Favourite'.

## Commands:

- **Adding a superhero to the favourites list**
    Adds a superhero to the favourites list of the user (persisting this to the database).
    
- **Removing a superhero from the favourites list**
    Removes a superhero from the favourites list of the user (persisting this to the database).
    
### Caching and resilience
Caching is implemented on top of the superhero provider and resilience are implemented as decorators on top of a faulty movies provider
