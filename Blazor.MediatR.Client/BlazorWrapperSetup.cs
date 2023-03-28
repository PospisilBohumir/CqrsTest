using System.Text.Json;

namespace Blazor.MediatR.Client;

public record BlazorWrapperSetup(string MediatorPath, JsonSerializerOptions? JsonSerializerOptions=null) : IBlazorWrapperSetup;
