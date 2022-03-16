using UnityEngine;


namespace Scripts.Components
{
    public class DestroyObjectComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToDestroy;
        public void DestroyObj()
        {
            Destroy(_objectToDestroy);
        }
    }
}

