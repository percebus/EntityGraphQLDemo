# HISTORY

## Packages

1. ~~`HotChocolate.AspNet`~~: [Getting Started](https://chillicream.com/docs/hotchocolate/v15/get-started-with-graphql-in-net-core)
1. `EntityGraphQL`
1. `EntityFrameworkCore`
1. ~~`EntityFrameworkCore.InMemory`~~ `EntityFrameworkCore.SqlServer`
 
## Entity Framework

1. Copied Entities from [Create a data model](https://entitygraphql.github.io/docs/getting-started#create-a-data-model)
1. Copied `DemoContext`
 
## Application

1. Remove `Todo`s
1. Followed steps in [Create the API](https://entitygraphql.github.io/docs/getting-started#create-the-api)
1. Used CoPilot to create Seed Data inside `DbInitializer`
1. Referenced [`src/examples/demo` (`5.7.1`) to fill in the gaps](https://github.com/EntityGraphQL/EntityGraphQL/tree/5.7.1/src/examples/demo)
 
## Controller

1. Added `GraphQLOptions` with `ConfigureSchema` to `Program.cs`, to generate `AppData/schema.graphql` w/ SDL
1. Created `Controlllers/Schema.cs` to expose the `schema.graphql` file as an endpoint
