using System.Timers;

namespace Full_GRASP_And_SOLID;

public class CountdownTimerForRecipe: CountdownTimer
{
    private CountdownTimer countdownTimer;
    private IRecipeContent client;
    private Timer timer;
    
    public CountdownTimerForRecipe(CountdownTimer countdownTimer)
    {
        this.countdownTimer = countdownTimer;
    }

    public void RegisterAdapter(int timeOut, IRecipeContent client)
    {
        this.client = client;
        this.timer = new Timer();
    }
}