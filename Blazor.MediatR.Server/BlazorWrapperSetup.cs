using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Blazor.MediatR.Server;

public record BlazorWrapperSetup(PathString MediatorPath, JsonSerializerOptions? JsonSerializerOptions = null);