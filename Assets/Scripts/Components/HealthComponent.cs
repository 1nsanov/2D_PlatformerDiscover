using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie; 

        public void ApplyDamage(int dmgValue)
        {
            _health -= dmgValue;
            _onDamage?.Invoke();
            if (_health <= 0)
            {
                _onDie?.Invoke();
            }
        }
    }
}

