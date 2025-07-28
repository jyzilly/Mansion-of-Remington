using UnityEngine;

public class AnimalBtnTrigger : MonoBehaviour
{
    public delegate void pokeDelegate();
    public pokeDelegate pokeEnteredCallback, pokeExitedCallback;

    private void PokeEntered()
    {
        pokeEnteredCallback.Invoke();
    }

    private void PokeExited()
    {
        pokeExitedCallback.Invoke();
    }
}
