using Godot;
using System;
using System.Collections.Generic;

public partial class SubscriptionHandler : Node
{
	[Export]
	TextureButton subscriptionBtn;
    [Export]
    TextureButton guestTierBtn;
    [Export]
    TextureButton studentTierBtn;
    [Export]
    TextureButton teacherTierBtn;

    [Export] private Button _subscription_panel_Btn;

    //[Export] private PanelContainer _subscription_types_panel;
    //[Export] private VBoxContainer _payment_panel;

    //[Export] private PaymentConfirmation _paymentConfirmation;
    //[Export] private Button _payment_confirm_Btn;

    [Export] private TextureButton _backBtn;

    [Export]
    ColorRect subscriptionPanel;

    [Export]
    Panel profilePanel;

    [Export] PackedScene logInPage;

    [Export] private Control documentationAdder;

    [Export] private NotificationHandler _notificationHandler;

    [Export]
	Texture2D freeTier;
    [Export]
    Texture2D guestTier;
    [Export]
    Texture2D studentTier;
    [Export]
    Texture2D teacherTier;

    private bool isMouseOverPanel = false;

    [ExportCategory("Documentation Disabler")]
    [Export] private DocumentationAbler _documentationAbler;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        //_subscription_types_panel.Visible = true;
        //_payment_panel.Visible = false;

        _subscription_panel_Btn.Visible = false;

        subscriptionBtn.TextureNormal = freeTier;
        subscriptionPanel.Visible = false;

        subscriptionBtn.Connect("pressed", new Callable(this, nameof(OpenSubscription)));

        //_subscription_panel_Btn.Connect("pressed", new Callable(this, nameof(OpenPaymentPanel)));
        //_payment_confirm_Btn.Connect("pressed", new Callable(this, nameof(ConfirmPayment)));
        _backBtn.Connect("pressed", new Callable(this, nameof(OnBackPressed)));

        /*// Connect the mouse_exited signal to close the subscription panel when the mouse leaves it
        subscriptionPanel.Connect("mouse_exited", new Callable(this, nameof(OnMouseExitedPanel)));
        subscriptionPanel.Connect("mouse_entered", new Callable(this, nameof(OnMouseEnteredPanel)));

        // Connect the mouse_exited and mouse_entered signals for the buttons inside the panel
        //guestTierBtn.Connect("mouse_exited", new Callable(this, nameof(OnMouseExitedPanel)));
        guestTierBtn.Connect("mouse_entered", new Callable(this, nameof(OnMouseEnteredPanel)));
        // studentTierBtn.Connect("mouse_exited", new Callable(this, nameof(OnMouseExitedPanel)));
        studentTierBtn.Connect("mouse_entered", new Callable(this, nameof(OnMouseEnteredPanel)));
        // teacherTierBtn.Connect("mouse_exited", new Callable(this, nameof(OnMouseExitedPanel)));
        teacherTierBtn.Connect("mouse_entered", new Callable(this, nameof(OnMouseEnteredPanel)));*/
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (AccountManager.GetUser() != null)
        {
            if (AccountManager.GetUser().Role == "Student")
            {
                _subscription_panel_Btn.Visible = true;
                subscriptionBtn.TextureNormal = studentTier;
                studentTierBtn.Modulate = new Color("5d5d5d");
                teacherTierBtn.Modulate = new Color("ffffff");
                guestTierBtn.Modulate = new Color("ffffff");

                SubscriptionPaymentButtonEdit();

                studentTierBtn.Disabled = true;
                teacherTierBtn.Disabled = false;
                guestTierBtn.Disabled = false;
            }
            else if(AccountManager.GetUser().Role == "Teacher")
            {
                subscriptionBtn.TextureNormal = teacherTier;
                teacherTierBtn.Modulate = new Color("5d5d5d");
                studentTierBtn.Modulate = new Color("ffffff");
                guestTierBtn.Modulate = new Color("ffffff");

                studentTierBtn.Disabled = false;
                teacherTierBtn.Disabled = true;
                guestTierBtn.Disabled = false;
            }
            else
            {
                subscriptionBtn.TextureNormal = guestTier;
                guestTierBtn.Modulate = new Color("5d5d5d");
                studentTierBtn.Modulate = new Color("ffffff");
                teacherTierBtn.Modulate = new Color("ffffff");

                studentTierBtn.Disabled = false;
                teacherTierBtn.Disabled = false;
                guestTierBtn.Disabled = true;
            }
        }
        else
        {
            _subscription_panel_Btn.Visible = false;
            subscriptionBtn.TextureNormal = freeTier;
        }
    }
    private void SubscriptionPaymentButtonEdit()
    {
        if (AccountManager.GetUser().Subscription == "Subscribed" || AccountManager.GetUser().Subscription == "Endorsed")
        {
            if (AccountManager.GetUser().Subscription == "Subscribed")
            {
                _subscription_panel_Btn.Text = "Subscribed";
            }else if (AccountManager.GetUser().Subscription == "Endorsed")
            {
                _subscription_panel_Btn.Text = "Payment sent...";
            }
            _subscription_panel_Btn.Disabled = true;
        }
        else
        {
            _subscription_panel_Btn.Text = "Subscribe";
            _subscription_panel_Btn.Disabled = false;
        }
    }
    private void OpenSubscription()
    {
        _documentationAbler.HideAllDocument();
        if (documentationAdder != null)
        {
            documentationAdder.Visible = false;
        }

        if (AccountManager.GetUser() != null)
        {
            profilePanel.Visible = false;
            if (subscriptionPanel.Visible)
                subscriptionPanel.Visible = false;
            else
                subscriptionPanel.Visible = true;
        }
        else
        {
            Node simultaneous = logInPage.Instantiate();
            GetTree().Root.AddChild(simultaneous);
        }
    }

    private void OnBackPressed()
    {
        /*if (_payment_panel.Visible == true)
        {
            _paymentConfirmation.ResetReceipt();
            _subscription_types_panel.Visible = true;
            _payment_panel.Visible = false;
        }
        else
        {*/
            subscriptionPanel.Visible = false;
        /*}*/
    }

    public void CloseSubscription()
    {
        //_paymentConfirmation.ResetReceipt();
        //_subscription_types_panel.Visible = true;
        //_payment_panel.Visible = false;
        subscriptionPanel.Visible = false;
    }
}
