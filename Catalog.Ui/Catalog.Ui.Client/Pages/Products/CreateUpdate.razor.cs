using Catalog.UI.Shared.Common;
using Catalog.UI.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Catalog.Ui.Client.Pages.Products;

public partial class CreateUpdate : ComponentBase
{
    [Inject]
    public required IApiService ApiService { get; set; }

    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required ILogger<CreateUpdate> Logger { get; set; }

    [Inject]
    public required IJSRuntime JsRuntime { get; set; }

    public CreateUpdateVM ProductModel { get; set; } = new();

    public IEnumerable<CategoryProductVM> Categories { get; set; } = new List<CategoryProductVM>();

    private bool IsSaving { get; set; }
    private string? StatusMessage { get; set; }
    private bool IsError { get; set; }
    private bool ShowPayload { get; set; }
    private string PriceInputId { get; } = "product-price-input";

    [Parameter]
    public int Id { get; set; }

    public async Task HandleSaveAsync()
    {
        IsSaving = true;
        StatusMessage = null;
        IsError = false;

        // Garantir que o campo Price perca o foco para forçar o binding do valor mais recente
        try
        {
            await JsRuntime.InvokeVoidAsync("eval", $"document.getElementById('{PriceInputId}')?.blur()");
        }
        catch { }

        if (Id > 0)
        {
            // log payload antes do update
            try
            {
                var payload = JsonSerializer.Serialize(ProductModel);
                Logger.LogInformation("Updating product id {Id} with payload: {Payload}", Id, payload);
                // também log no console do navegador para garantir visibilidade
                try { await JsRuntime.InvokeVoidAsync("console.log", "Update payload:", payload); } catch { }
            }
            catch { }

            var updateResponse = await ApiService.UpdateAsync($"Product/{Id}", ProductModel);
            try
            {
                var respContent = await updateResponse.Content.ReadAsStringAsync();
                Logger.LogInformation("Update response status {Status} content: {Content}", updateResponse.StatusCode, respContent);

                if (updateResponse.IsSuccessStatusCode)
                {
                    // tentar desserializar o wrapper Result<T>
                    try
                    {
                        var result = JsonSerializer.Deserialize<Result<CreateUpdateVM>>(respContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        if (result != null && result.IsSuccess)
                        {
                            StatusMessage = "Produto atualizado com sucesso.";
                            IsError = false;
                        }
                        else
                        {
                            StatusMessage = result?.Error ?? "Falha ao atualizar o produto.";
                            IsError = true;
                        }
                    }
                    catch
                    {
                        StatusMessage = "Produto atualizado.";
                        IsError = false;
                    }
                }
                else
                {
                    StatusMessage = !string.IsNullOrWhiteSpace(respContent) ? respContent : $"Erro: {updateResponse.StatusCode}";
                    IsError = true;
                }
            }
            catch
            {
                StatusMessage = "Erro ao processar resposta do servidor.";
                IsError = true;
            }
        }
        else
        {
            try
            {
                var payload = JsonSerializer.Serialize(ProductModel);
                Logger.LogInformation("Creating product with payload: {Payload}", payload);
                try { await JsRuntime.InvokeVoidAsync("console.log", "Post payload:", payload); } catch { }
            }
            catch { }

            var postResult = await ApiService.PostAsync<Result<CreateUpdateVM>>("Product", ProductModel);
            try
            {
                Logger.LogInformation("Post returned: {@Result}", postResult);
            }
            catch { }

            if (postResult != null && postResult.IsSuccess)
            {
                StatusMessage = "Produto criado com sucesso.";
                IsError = false;
            }
            else
            {
                StatusMessage = postResult?.Error ?? "Falha ao criar produto.";
                IsError = true;
            }
        }

        IsSaving = false;

        // se sucesso, aguardar um curto período para exibir a mensagem e depois navegar
        if (!IsError)
        {
            await Task.Delay(1000);
            NavigationManager.NavigateTo("/products");
        }
    }

    public async Task OnSaveClicked()
    {
        try
        {
            // show immediate feedback in UI
            StatusMessage = "Enviando...";
            IsError = false;
            StateHasChanged();

            try { await JsRuntime.InvokeVoidAsync("console.log", "Save clicked"); } catch { }

            await HandleSaveAsync();
        }
        catch (Exception ex)
        {
            try { await JsRuntime.InvokeVoidAsync("console.error", "Save error:", ex.Message); } catch { }
            StatusMessage = "Erro interno: " + ex.Message;
            IsError = true;
            IsSaving = false;
            StateHasChanged();
        }
    }

    protected override async Task OnParametersSetAsync()
    {
        if (Id > 0)
        {
            try
            {
                var result = await ApiService.GetAsync<Result<CreateUpdateVM>>($"Product/{Id}");
                if (result != null && result.IsSuccess)
                {
                    ProductModel = result.Value ?? new CreateUpdateVM();
                }
                else
                {
                    ProductModel = new CreateUpdateVM();
                }
            }
            catch
            {
                ProductModel = new CreateUpdateVM();
            }
        }
    }

    protected override async Task OnInitializedAsync()
    {
        await LoadCategoriesAsync();
    }

    private async Task LoadCategoriesAsync()
    {
        try
        {
            var result = await ApiService.GetAsync<Result<IEnumerable<CategoryProductVM>>>("Categories");
            if (result != null && result.IsSuccess)
            {
                Categories = result.Value ?? new List<CategoryProductVM>();
            }
            else
            {
                Categories = new List<CategoryProductVM>();
            }
        }
        catch
        {
            Categories = new List<CategoryProductVM>();
        }
    }

    public string GetDebugPayload()
    {
        try
        {
            return JsonSerializer.Serialize(ProductModel, new JsonSerializerOptions { WriteIndented = true });
        }
        catch
        {
            return string.Empty;
        }
    }
}