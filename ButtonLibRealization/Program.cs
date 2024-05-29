using ButtonLib;

internal class Program
{
    public static void ButtonAction(ButtonEventArgs e)
    {

    }
    private static void Main(string[] args)
    {
        //Creating a button and assign a virtual key with the name
        ButtonManager.CreateButton(0x01,"LeftMouseButton");
        //Getting Button from button container in manager
        BaseButton LeftMouseButton = ButtonManager.GetButtonByName("LeftMouseButton");


        //Assign actions
        LeftMouseButton.ButtonDown += ButtonAction;
        LeftMouseButton.ButtonPressed += ButtonAction;
        LeftMouseButton.ButtonUp += ButtonAction;


        //Creating Cancellation Token for exiting button scanner
        CancellationTokenSource cts = new CancellationTokenSource();

        //Creating button event arguments entity
        ButtonEventArgs e = new ButtonEventArgs();
        //Runing button scaner with arguments
        LeftMouseButton.ButtonScanner(cts, e);

        //Running scanner without arguments
        LeftMouseButton.ButtonScanner(cts);


        //Exit the scanner
        Thread.Sleep(60000);
        cts.Cancel();
    }
}