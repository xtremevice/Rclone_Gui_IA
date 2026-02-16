using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RcloneGui.Core.Models;
using RcloneGui.Core.Services;

namespace RcloneGui.ViewModels;

public partial class RemotesViewModel : ViewModelBase
{
    private readonly IRcloneService _rcloneService;

    [ObservableProperty]
    private ObservableCollection<RemoteAccount> _remotes = new();

    [ObservableProperty]
    private RemoteAccount? _selectedRemote;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    [ObservableProperty]
    private bool _isLoading = false;

    public RemotesViewModel()
    {
        _rcloneService = new RcloneService();
        _ = LoadRemotesAsync();
    }

    [RelayCommand]
    private async Task LoadRemotesAsync()
    {
        IsLoading = true;
        StatusMessage = "Loading remotes...";

        try
        {
            var remotes = await _rcloneService.GetConfiguredRemotesAsync();
            Remotes = new ObservableCollection<RemoteAccount>(remotes.OrderBy(r => r.Name));
            StatusMessage = $"Loaded {remotes.Count} remote(s)";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error loading remotes: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task DeleteRemoteAsync()
    {
        if (SelectedRemote == null)
        {
            StatusMessage = "Please select a remote to delete.";
            return;
        }

        IsLoading = true;
        StatusMessage = $"Deleting {SelectedRemote.Name}...";

        try
        {
            var success = await _rcloneService.DeleteRemoteAsync(SelectedRemote.Name);
            if (success)
            {
                StatusMessage = $"Successfully deleted {SelectedRemote.Name}";
                await LoadRemotesAsync();
            }
            else
            {
                StatusMessage = $"Failed to delete {SelectedRemote.Name}";
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
    private async Task TestRemoteAsync()
    {
        if (SelectedRemote == null)
        {
            StatusMessage = "Please select a remote to test.";
            return;
        }

        IsLoading = true;
        StatusMessage = $"Testing {SelectedRemote.Name}...";

        try
        {
            var success = await _rcloneService.TestRemoteAsync(SelectedRemote.Name);
            StatusMessage = success 
                ? $"{SelectedRemote.Name} is working correctly!" 
                : $"{SelectedRemote.Name} test failed.";
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
}
