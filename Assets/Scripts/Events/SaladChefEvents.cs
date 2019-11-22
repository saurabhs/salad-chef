namespace SaladChef.Events
{
    public class OnTimeOver : UnityEngine.Events.UnityEvent { }
    public class OnCustomerSeated : UnityEngine.Events.UnityEvent { }
    public class OnOrderPlaced : UnityEngine.Events.UnityEvent<string> { }
    public class OnOrderReceived : UnityEngine.Events.UnityEvent<UnityEngine.GameObject, string, float, float> { }
    public class OnWrongOrderReceived : UnityEngine.Events.UnityEvent { }
    public class OnOrderValidated : UnityEngine.Events.UnityEvent<UnityEngine.GameObject, float, bool> { }
    public class OnMoveComplete : UnityEngine.Events.UnityEvent { }
}