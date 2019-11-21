namespace SaladChef.Events
{
    public class OnWaitTimeOver : UnityEngine.Events.UnityEvent { }
    public class OnOrderReceived : UnityEngine.Events.UnityEvent<float, float> { }
}