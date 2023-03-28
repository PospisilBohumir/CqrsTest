using System.Net.Http.Json;
using System.Text.Json;
using MediatR;

namespace Blazor.MediatR.Client;

internal class BlazorWrapperRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly HttpClient _http;
    private readonly IBlazorWrapperSetup _setup;

    public BlazorWrapperRequestHandler(HttpClient http, IBlazorWrapperSetup setup)
    {
        _http = http;
        _setup = setup;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var messageContract = new MessageContract(JsonSerializer.Serialize(request, _setup.JsonSerializerOptions), 
            request.GetType().AssemblyQualifiedName!);
        
        var result = await _http.PostAsJsonAsync(_setup.MediatorPath, messageContract, cancellationToken);
        return await result.Content.ReadFromJsonAsync<TResponse>(_setup.JsonSerializerOptions, cancellationToken);
    }
}