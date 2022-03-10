using UnityEngine;
using UnityEngine.InputSystem;

public class HeroInputReader : MonoBehaviour
{
    [SerializeField] private HeroPlayer _hero;
    private HeroInputAction _inputAction;

    private void Awake()
    {
        _inputAction = new HeroInputAction();
    }
    private void OnEnable()
    {
        _inputAction.Enable();
    }

    public void OnMovement(InputAction.CallbackContext ctx)
    {
       var direction = ctx.ReadValue<Vector2>();
        _hero.SetDirection(direction);
    }

    public void OnFire(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            _hero.Fire();
        }
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            _hero.Interact();
        }
    }
}
