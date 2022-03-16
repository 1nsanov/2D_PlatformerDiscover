using UnityEngine;

namespace Scripts.Components
{
    public class ChangeHealthComponent : MonoBehaviour
    {
        [SerializeField] private uint _healthCount;
        [SerializeField] private ModeChangeHealth _mode;

        public void ApplyChange(GameObject target)
        {
            var health = target.GetComponent<HealthComponent>();
            if (health != null)
            {
                var healthCount = (int)(_mode == ModeChangeHealth.Damage ? _healthCount * -1 : _healthCount); 
                health.ApplyChange(healthCount);
            }
        }
    }
    public enum ModeChangeHealth
    {
        Damage,
        Hill
    }
}

