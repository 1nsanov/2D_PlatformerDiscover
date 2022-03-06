using UnityEngine;
using UnityEngine.InputSystem;

public class HeroInputReader : MonoBehaviour
{
    [SerializeField] private HeroMove _heroMove;
    private HeroInputAction _inputAction;

    private void Awake()
    {
        _inputAction = new HeroInputAction();
        _inputAction.Hero.HorizontalMovement.performed += OnHorizontalMovement;
        _inputAction.Hero.HorizontalMovement.canceled += OnHorizontalMovement;
        _inputAction.Hero.Fire.performed += OnFire;
    }
    private void OnEnable()
    {
        _inputAction.Enable();
    }

    private void OnHorizontalMovement(InputAction.CallbackContext context)
    {
       var direction = context.ReadValue<Vector2>();
        _heroMove.SetDirection(direction);
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            _heroMove.Fire();
        }
    }
}
