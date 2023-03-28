using System.Text.Json;

namespace Blazor.MediatR.Client;

public interface IBlazorWrapperSetup
{
    string MediatorPath { get; }
    JsonSerializerOptions JsonSerializerOptions { get; }
}