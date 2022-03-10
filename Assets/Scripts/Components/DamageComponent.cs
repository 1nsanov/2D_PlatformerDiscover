using UnityEngine;

namespace Scripts.Components
{
    public class DamageComponent : MonoBehaviour
    {
        [SerializeField] private int _damage;

        public void ApplyDamage(GameObject target)
        {
            var health = target.GetComponent<HealthComponent>();
            if (health != null)
            {
                health.ApplyDamage(_damage);
            }
        }
    }
}

