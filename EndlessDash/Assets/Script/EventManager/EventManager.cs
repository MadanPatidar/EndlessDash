using UnityEngine.Events;
public static class EventManager
{
    public static event UnityAction<int> OnAddActions;
    public static void RaiseAddActions(int actions) => OnAddActions?.Invoke(actions);
    //----
    public static event UnityAction<int> OnCoinCollection;
    public static void RaiseCoinCollection(int coin) => OnCoinCollection?.Invoke(coin);
    //----
    public static event UnityAction<int> OnTakeDamage;
    public static void RaiseTakeDamage(int damage) => OnTakeDamage?.Invoke(damage);

    //init/reset the ingame
    public static event UnityAction OnInit;
    public static void RaiseInit() => OnInit?.Invoke();
    //--
    public static event UnityAction<bool> OnAppPause;
    public static void RaiseAppPause(bool pause) => OnAppPause?.Invoke(pause);
}