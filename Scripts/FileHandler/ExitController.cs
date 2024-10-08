using Godot;
using System;
using System.Threading.Tasks;

public partial class ExitController : Node
{
	[Export] private Button exitbtn;
	[Export] private Panel savePanel;
	[Export] private Button yesbtn;
	[Export] private Button nobtn;

    [Export] private Control documentationAdder;

    [Export] private bool saveAvailable = true;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        savePanel.Visible = false;
        exitbtn.Connect("pressed", new Callable(this, nameof(Exit)));
        yesbtn.Connect("pressed", new Callable(this, nameof(Save)));
        nobtn.Connect("pressed", new Callable(this, nameof(NotSave)));
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
	private async void Exit()
	{
        if(documentationAdder != null)
        {
            documentationAdder.Visible = false;
        }
        if (AccountManager.GetUser() != null && saveAvailable && (AccountManager.GetUser().Role == "Student" || AccountManager.GetUser().Role == "Teacher"))
        {
            savePanel.Visible = true;
		}
		else
		{
            await WaitTime();
            GetTree().Quit();
        }
	}
    private async void Save()
    {
        DataToSave.status = "Idle";
        DataToSave.SaveFile();
        DataToSave.ResetDatas();
        await WaitTime();
        GetTree().Quit();
    }
    private void NotSave()
    {
        GetTree().Quit();
    }
    private async Task WaitTime()
    {
        await Task.Delay(1000);
    }
}
