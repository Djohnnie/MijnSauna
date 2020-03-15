using MijnSauna.Frontend.Phone.Services.Design;

namespace MijnSauna.Frontend.Phone.ViewModels.Design
{
    public class MainPageDesignViewModel : MainPageViewModel
    {
        public MainPageDesignViewModel()
            : base(new StatusBarDesignService())
        {

        }
    }
}