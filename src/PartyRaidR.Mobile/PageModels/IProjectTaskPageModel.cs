using CommunityToolkit.Mvvm.Input;
using PartyRaidR.Mobile.Models;

namespace PartyRaidR.Mobile.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}