using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Linq;
using System.Threading.Tasks;
using RcloneGui.Core.Services;

namespace RcloneGui.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly IRcloneService _rcloneService;

    [ObservableProperty]
    private ViewModelBase _currentPage;

    [ObservableProperty]
    private string _rcloneVersion = "Checking...";

    public AddAccountViewModel AddAccountViewModel { get; }
    public RemotesViewModel RemotesViewModel { get; }

    public MainWindowViewModel()
    {
        _rcloneService = new RcloneService();
        AddAccountViewModel = new AddAccountViewModel();
        RemotesViewModel = new RemotesViewModel();
        
        _currentPage = RemotesViewModel;
        
        _ = LoadRcloneVersionAsync();
    }

    private async Task LoadRcloneVersionAsync()
    {
        try
        {
            var version = await _rcloneService.GetRcloneVersionAsync();
            var firstLine = version.Split('\n').FirstOrDefault() ?? version;
            RcloneVersion = firstLine;
        }
        catch
        {
            RcloneVersion = "Rclone not found - please install rclone";
        }
    }

    [RelayCommand]
    private void ShowRemotes()
    {
        CurrentPage = RemotesViewModel;
    }

    [RelayCommand]
    private void ShowAddAccount()
    {
        CurrentPage = AddAccountViewModel;
    }
}
