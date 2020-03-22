namespace MijnSauna.Frontend.Phone.Factories.Interfaces
{
    public interface IViewModelFactory
    {
        TViewModel Get<TViewModel>();
    }
}