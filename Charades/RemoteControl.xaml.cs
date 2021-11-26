namespace Charades;

public partial class RemoteControl
{
    public RemoteControl(ViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
