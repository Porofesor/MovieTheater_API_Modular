using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace UserEndPoints
{
    public static class UserEndpoints
    {
        private const string Tag = "User Management";

        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder endpoints)
        {
            // Register Account Endpoint
            endpoints.MapPost("/api/users/RegisterAccountAsync", async (RegisterAccountAsync.Request request, RegisterAccountAsync useCase) =>
            {
                var result = await useCase.Handle(request);
                return result.Success ? Results.Ok(result) : Results.BadRequest(result.Errors);
            }).WithTags(Tag);

            // Login Endpoint
            endpoints.MapPost("/api/users/Login", async (Login.Request request, Login useCase) =>
            {
                var result = await useCase.Handle(request);
                return result.Success ? Results.Ok(result) : Results.BadRequest(result.Errors);
            }).WithTags(Tag);

            // Verify Email Endpoint
            endpoints.MapGet("/api/users/verify-email", async (Guid token, VerifyEmail useCase) =>
            {
                bool success = await useCase.Handle(token);
                return success ? Results.Ok() : Results.BadRequest("Verification token expired.");
            }).WithTags(Tag).WithName("VerifyEmail");

            // Get User by ID Endpoint
            endpoints.MapGet("/api/users/{id:guid}", async (Guid id, GetUser useCase) =>
            {
                var user = await useCase.Handle(id);
                return user is not null ? Results.Ok(user) : Results.NotFound("User not found.");
            }).WithTags(Tag).RequireAuthorization();

            return endpoints;
        }
    }
}