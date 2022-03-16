using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _healthLimit;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onHill;
        [SerializeField] private UnityEvent _onDie; 

        private int _healthPull;
        private void Awake()
        {
            _healthPull = _healthLimit;
        }
        public void ApplyChange(int healthValue)
        {
            _healthPull += healthValue;
            if (_healthPull > _healthLimit)
            {
                _healthPull = _healthLimit;
            }
            else
            {
                if (healthValue < 0)
                {
                    _onDamage?.Invoke();
                    if (_healthPull <= 0)
                    {
                        _onDie?.Invoke();
                    }
                }
                else if (healthValue > 0)
                {
                    _onHill?.Invoke();
                }
            }
            Debug.Log($"Health: {_healthPull}");
        }
    }
}

