using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RcloneGui.Core.Models;
using RcloneGui.Core.Services;

namespace RcloneGui.ViewModels;

public partial class AddAccountViewModel : ViewModelBase
{
    private readonly IProviderService _providerService;
    private readonly IRcloneService _rcloneService;

    [ObservableProperty]
    private ObservableCollection<ProviderInfo> _providers = new();

    [ObservableProperty]
    private ProviderInfo? _selectedProvider;

    [ObservableProperty]
    private string _remoteName = string.Empty;

    [ObservableProperty]
    private string _username = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private string _host = string.Empty;

    [ObservableProperty]
    private string _accessKeyId = string.Empty;

    [ObservableProperty]
    private string _secretAccessKey = string.Empty;

    [ObservableProperty]
    private string _url = string.Empty;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    [ObservableProperty]
    private bool _isLoading = false;

    [ObservableProperty]
    private bool _showUsernamePassword = false;

    [ObservableProperty]
    private bool _showHost = false;

    [ObservableProperty]
    private bool _showApiKeys = false;

    [ObservableProperty]
    private bool _showUrl = false;

    [ObservableProperty]
    private bool _requiresBrowser = false;

    public AddAccountViewModel()
    {
        _providerService = new ProviderService();
        _rcloneService = new RcloneService();
        _ = LoadProvidersAsync();
    }

    partial void OnSelectedProviderChanged(ProviderInfo? value)
    {
        UpdateFieldVisibility();
    }

    private async Task LoadProvidersAsync()
    {
        var providers = await _providerService.GetSupportedProvidersAsync();
        Providers = new ObservableCollection<ProviderInfo>(providers.OrderBy(p => p.Name));
    }

    private void UpdateFieldVisibility()
    {
        if (SelectedProvider == null)
        {
            ShowUsernamePassword = false;
            ShowHost = false;
            ShowApiKeys = false;
            ShowUrl = false;
            RequiresBrowser = false;
            return;
        }

        RequiresBrowser = SelectedProvider.RequiresBrowser;

        switch (SelectedProvider.AuthType)
        {
            case AuthenticationType.UsernamePassword:
                ShowUsernamePassword = true;
                ShowHost = SelectedProvider.Type is "ftp" or "sftp";
                ShowUrl = SelectedProvider.Type is "webdav";
                ShowApiKeys = false;
                break;

            case AuthenticationType.ApiKey:
                ShowUsernamePassword = false;
                ShowHost = false;
                ShowUrl = false;
                ShowApiKeys = true;
                break;

            case AuthenticationType.OAuth2:
                ShowUsernamePassword = false;
                ShowHost = false;
                ShowUrl = false;
                ShowApiKeys = false;
                break;

            default:
                ShowUsernamePassword = false;
                ShowHost = false;
                ShowUrl = false;
                ShowApiKeys = false;
                break;
        }
    }

    [RelayCommand]
    private async Task AddAccountAsync()
    {
        if (SelectedProvider == null || string.IsNullOrWhiteSpace(RemoteName))
        {
            StatusMessage = "Please select a provider and enter a remote name.";
            return;
        }

        IsLoading = true;
        StatusMessage = "Adding account...";

        try
        {
            var request = new AuthenticationRequest
            {
                ProviderType = SelectedProvider.Type,
                RemoteName = RemoteName,
                Username = Username,
                Password = Password,
                AdditionalParameters = new Dictionary<string, string>()
            };

            // Add additional parameters based on provider type
            if (!string.IsNullOrWhiteSpace(Host))
                request.AdditionalParameters["host"] = Host;

            if (!string.IsNullOrWhiteSpace(Url))
                request.AdditionalParameters["url"] = Url;

            if (!string.IsNullOrWhiteSpace(AccessKeyId))
                request.AdditionalParameters["access_key_id"] = AccessKeyId;

            if (!string.IsNullOrWhiteSpace(SecretAccessKey))
                request.AdditionalParameters["secret_access_key"] = SecretAccessKey;

            AuthenticationResult result;

            if (SelectedProvider.RequiresBrowser)
            {
                StatusMessage = "Opening browser for authentication...";
                result = await _rcloneService.AuthenticateWithBrowserAsync(request);
            }
            else
            {
                result = await _rcloneService.AuthenticateAsync(request);
            }

            if (result.Success)
            {
                StatusMessage = $"Successfully added {RemoteName}!";
                ClearFields();
            }
            else
            {
                StatusMessage = $"Failed to add account: {result.ErrorMessage}";
            }
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private void ClearFields()
    {
        RemoteName = string.Empty;
        Username = string.Empty;
        Password = string.Empty;
        Host = string.Empty;
        Url = string.Empty;
        AccessKeyId = string.Empty;
        SecretAccessKey = string.Empty;
        StatusMessage = string.Empty;
    }
}
