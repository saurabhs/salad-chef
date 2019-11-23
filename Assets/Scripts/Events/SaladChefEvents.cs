namespace SaladChef.Events
{
    public class OnTimeOver : UnityEngine.Events.UnityEvent { }
    public class OnCustomerSeated : UnityEngine.Events.UnityEvent { }
    public class OnOrderPlaced : UnityEngine.Events.UnityEvent<string> { }
    public class OnOrderReceived : UnityEngine.Events.UnityEvent<UnityEngine.GameObject, string, float, float> { }
    public class OnWrongOrderReceived : UnityEngine.Events.UnityEvent<UnityEngine.GameObject> { }
    public class OnOrderValidated : UnityEngine.Events.UnityEvent<UnityEngine.GameObject, float, bool> { }
    public class OnMoveComplete : UnityEngine.Events.UnityEvent { }
    public class OnVegetablePicked : UnityEngine.Events.UnityEvent<UnityEngine.GameObject> { }

    public class OnMoveToTable : UnityEngine.Events.UnityEvent<int> { }
    public class OnMoveToKitchen : UnityEngine.Events.UnityEvent { }
    public class OnChopVegetableStart : UnityEngine.Events.UnityEvent { }
    public class OnServeToCustomer : UnityEngine.Events.UnityEvent<int> { }
    public class OnDiscardVegetable : UnityEngine.Events.UnityEvent { }

    public class OnVegetableChoppedComplete : UnityEngine.Events.UnityEvent<Core.Vegetable> { }
}