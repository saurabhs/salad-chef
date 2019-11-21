namespace SaladChef.Events
{
    public class OnTimeOver : UnityEngine.Events.UnityEvent { }
    public class OnOrderReceived : UnityEngine.Events.UnityEvent<float, float> { }
}