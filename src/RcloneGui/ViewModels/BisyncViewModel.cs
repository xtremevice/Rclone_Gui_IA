using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RcloneGui.Core.Models;
using RcloneGui.Core.Services;

namespace RcloneGui.ViewModels;

public partial class BisyncViewModel : ViewModelBase
{
    private readonly IRcloneService _rcloneService;

    [ObservableProperty]
    private ObservableCollection<BisyncOperation> _bisyncOperations = new();

    [ObservableProperty]
    private ObservableCollection<RemoteAccount> _availableRemotes = new();

    [ObservableProperty]
    private BisyncOperation? _selectedOperation;

    [ObservableProperty]
    private string _operationName = string.Empty;

    [ObservableProperty]
    private RemoteAccount? _sourceRemote;

    [ObservableProperty]
    private string _sourcePath = string.Empty;

    [ObservableProperty]
    private RemoteAccount? _destinationRemote;

    [ObservableProperty]
    private string _destinationPath = string.Empty;

    [ObservableProperty]
    private bool _resync = false;

    [ObservableProperty]
    private bool _checkAccess = true;

    [ObservableProperty]
    private bool _createEmptySrcDirs = false;

    [ObservableProperty]
    private bool _removeEmptyDirs = false;

    [ObservableProperty]
    private bool _dryRun = false;

    [ObservableProperty]
    private bool _force = false;

    [ObservableProperty]
    private int _maxDelete = 50;

    [ObservableProperty]
    private string _conflictResolve = "none";

    [ObservableProperty]
    private string _compareFlag = "size,modtime";

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    [ObservableProperty]
    private bool _isLoading = false;

    [ObservableProperty]
    private string _generatedCommand = string.Empty;

    [ObservableProperty]
    private bool _showMacCommand = true;

    [ObservableProperty]
    private ObservableCollection<string> _sourcePaths = new();

    [ObservableProperty]
    private ObservableCollection<string> _destinationPaths = new();

    public ObservableCollection<string> ConflictResolveOptions { get; } = new()
    {
        "none",
        "newer",
        "older",
        "larger",
        "smaller",
        "path1",
        "path2"
    };

    public BisyncViewModel()
    {
        _rcloneService = new RcloneService();
        _ = LoadRemotesAsync();
        _ = LoadBisyncOperationsAsync();
    }

    [RelayCommand]
    private async Task LoadRemotesAsync()
    {
        IsLoading = true;
        StatusMessage = "Loading remotes...";

        try
        {
            var remotes = await _rcloneService.GetConfiguredRemotesAsync();
            AvailableRemotes = new ObservableCollection<RemoteAccount>(remotes.OrderBy(r => r.Name));
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
    private async Task LoadBisyncOperationsAsync()
    {
        // TODO: Load saved bisync operations from storage
        // For now, just clear the list
        BisyncOperations.Clear();
        await Task.CompletedTask;
    }

    [RelayCommand]
    private async Task LoadSourcePathsAsync()
    {
        if (SourceRemote == null) return;

        IsLoading = true;
        try
        {
            var paths = await _rcloneService.ListRemotePathsAsync(SourceRemote.Name, SourcePath);
            SourcePaths = new ObservableCollection<string>(paths);
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error loading paths: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task LoadDestinationPathsAsync()
    {
        if (DestinationRemote == null) return;

        IsLoading = true;
        try
        {
            var paths = await _rcloneService.ListRemotePathsAsync(DestinationRemote.Name, DestinationPath);
            DestinationPaths = new ObservableCollection<string>(paths);
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error loading paths: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private void GenerateCommand()
    {
        if (SourceRemote == null || DestinationRemote == null)
        {
            StatusMessage = "Please select both source and destination remotes";
            return;
        }

        var operation = CreateOperationFromForm();
        GeneratedCommand = _rcloneService.GenerateBisyncCommand(operation, ShowMacCommand);
        StatusMessage = "Command generated successfully";
    }

    [RelayCommand]
    private async Task RunBisyncAsync()
    {
        if (SourceRemote == null || DestinationRemote == null)
        {
            StatusMessage = "Please select both source and destination remotes";
            return;
        }

        IsLoading = true;
        StatusMessage = "Running bisync operation...";

        try
        {
            var operation = CreateOperationFromForm();
            var result = await _rcloneService.RunBisyncAsync(operation);

            if (result.Success)
            {
                StatusMessage = $"Bisync completed successfully! Transferred {result.FilesTransferred} files in {result.Duration.TotalSeconds:F1}s";
            }
            else
            {
                StatusMessage = $"Bisync failed: {result.ErrorOutput}";
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
    private void SaveOperation()
    {
        if (string.IsNullOrWhiteSpace(OperationName) || SourceRemote == null || DestinationRemote == null)
        {
            StatusMessage = "Please fill in all required fields";
            return;
        }

        var operation = CreateOperationFromForm();
        operation.Name = OperationName;

        BisyncOperations.Add(operation);
        StatusMessage = $"Operation '{OperationName}' saved successfully";

        // TODO: Persist to storage
        ClearForm();
    }

    [RelayCommand]
    private void DeleteOperation()
    {
        if (SelectedOperation == null)
        {
            StatusMessage = "Please select an operation to delete";
            return;
        }

        BisyncOperations.Remove(SelectedOperation);
        StatusMessage = $"Operation deleted successfully";
        SelectedOperation = null;
    }

    [RelayCommand]
    private void LoadOperation()
    {
        if (SelectedOperation == null)
        {
            StatusMessage = "Please select an operation to load";
            return;
        }

        OperationName = SelectedOperation.Name;
        SourceRemote = AvailableRemotes.FirstOrDefault(r => r.Name == SelectedOperation.SourceRemote);
        SourcePath = SelectedOperation.SourcePath;
        DestinationRemote = AvailableRemotes.FirstOrDefault(r => r.Name == SelectedOperation.DestinationRemote);
        DestinationPath = SelectedOperation.DestinationPath;

        var options = SelectedOperation.Options;
        Resync = options.Resync;
        CheckAccess = options.CheckAccess;
        CreateEmptySrcDirs = options.CreateEmptySrcDirs;
        RemoveEmptyDirs = options.RemoveEmptyDirs;
        DryRun = options.DryRun;
        Force = options.Force;
        MaxDelete = options.MaxDelete;
        ConflictResolve = options.ConflictResolve;
        CompareFlag = options.CompareFlag;

        StatusMessage = $"Loaded operation '{SelectedOperation.Name}'";
    }

    [RelayCommand]
    private void ClearForm()
    {
        OperationName = string.Empty;
        SourceRemote = null;
        SourcePath = string.Empty;
        DestinationRemote = null;
        DestinationPath = string.Empty;
        Resync = false;
        CheckAccess = true;
        CreateEmptySrcDirs = false;
        RemoveEmptyDirs = false;
        DryRun = false;
        Force = false;
        MaxDelete = 50;
        ConflictResolve = "none";
        CompareFlag = "size,modtime";
        GeneratedCommand = string.Empty;
        StatusMessage = "Form cleared";
    }

    private BisyncOperation CreateOperationFromForm()
    {
        return new BisyncOperation
        {
            Name = OperationName,
            SourceRemote = SourceRemote?.Name ?? string.Empty,
            SourcePath = SourcePath,
            DestinationRemote = DestinationRemote?.Name ?? string.Empty,
            DestinationPath = DestinationPath,
            Options = new BisyncOptions
            {
                Resync = Resync,
                CheckAccess = CheckAccess,
                CreateEmptySrcDirs = CreateEmptySrcDirs,
                RemoveEmptyDirs = RemoveEmptyDirs,
                DryRun = DryRun,
                Force = Force,
                MaxDelete = MaxDelete,
                ConflictResolve = ConflictResolve,
                CompareFlag = CompareFlag
            }
        };
    }
}
