using UnityEngine;

//Note: An Abstract class means that the class cannot be instantiated, but can be inherited

public abstract class Coin : MonoBehaviour
{
    //Note: Protected means that the variable can only be accessed by the class itself and its subclasses
    //Note: A protected function can only be accessed by the class itself and its subclasses

    [SerializeField] private SpriteRenderer spriteRenderer;
    protected int coinValue;
    protected bool alreadyCollected = false;

    //Note: Abstract means that the method must be implemented by the subclass
    public abstract int Collect();

    public void SetValue(int value)
    {
        coinValue = value;
    }

    protected void Show(bool show)
    {
        spriteRenderer.enabled = show;
    }
}
