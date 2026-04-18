using Catalog.UI.Shared.Common;
using Catalog.UI.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Catalog.Ui.Client.Pages.Products;

public partial class List : ComponentBase
{
    [Inject]
    public required IApiService ApiService { get; set; }

    [Inject]
    public required IJSRuntime JsRuntime { get; set; }

    [Inject]
    public required ILogger<List> Logger { get; set; }
    private IEnumerable<ListVM> Products { get; set; } = Enumerable.Empty<ListVM>();
    private bool IsLoading { get; set; }
    private string? ErrorMessage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await LoadProductsAsync();
    }

    private async Task LoadProductsAsync()
    {
        IsLoading = true;
        ErrorMessage = null;
        try
        {
            var result = await ApiService.GetAsync<Result<IEnumerable<ListVM>>>("Product");

            if (result != null && result.IsSuccess)
            {
                Products = result.Value ?? Enumerable.Empty<ListVM>();
            }
            else
            {
                Products = Enumerable.Empty<ListVM>();
                ErrorMessage = result?.Error ?? "Erro ao carregar produtos.";
            }
        }
        catch (Exception ex)
        {
            Products = Enumerable.Empty<ListVM>();
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsLoading = false;
            StateHasChanged();
        }
    }

    private async Task DeleteProductAsync(int id)
    {
        // confirmação no navegador
        try
        {
            var confirm = await JsRuntime.InvokeAsync<bool>("confirm", $"Confirma exclusão do produto {id}?");
            if (!confirm) return;

            IsLoading = true;
            ErrorMessage = null;

            var response = await ApiService.DeleteAsync($"Product/{id}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                Logger.LogInformation("Produto {Id} removido com sucesso. Response: {Content}", id, content);
                await LoadProductsAsync();
            }
            else
            {
                ErrorMessage = !string.IsNullOrWhiteSpace(content) ? content : $"Erro ao deletar produto: {response.StatusCode}";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsLoading = false;
            StateHasChanged();
        }
    }
}
