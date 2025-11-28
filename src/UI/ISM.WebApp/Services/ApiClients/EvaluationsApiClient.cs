using ISM.WebApp.Services.ApiClients.Interfaces;
using ISM.WebApp.Services.ApiClients.Models.Evaluation;

namespace ISM.WebApp.Services.ApiClients;

public class EvaluationsApiClient : ApiClientBase, IEvaluationsApiClient
{
    public EvaluationsApiClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<IEnumerable<EvaluationDto>> GetByIdeaAsync(Guid ideaId, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.GetAsync($"api/ideas/{ideaId}/evaluations", cancellationToken);
        return await ReadAsAsync<IEnumerable<EvaluationDto>>(response) ?? Enumerable.Empty<EvaluationDto>();
    }

    public async Task UpdateAsync(Guid id, EvaluationUpdateRequest request, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.PutAsJsonAsync($"api/evaluations/{id}", request, cancellationToken);
        await EnsureSuccess(response);
    }
}
