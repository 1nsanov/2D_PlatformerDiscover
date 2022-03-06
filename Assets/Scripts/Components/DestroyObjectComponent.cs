using UnityEngine;


namespace Scripts.Components
{
    public class DestroyObjectComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToDestroy;
        public static int TotalSumCoins = 0;
        public void DestroyObj()
        {
            if (_objectToDestroy.tag == "Coin_1")
            {
                TotalSumCoins += 1;
            }
            else if (_objectToDestroy.tag == "Coin_10")
            {
                TotalSumCoins += 10;
            }
            Debug.Log($"Total Coin: {TotalSumCoins}");
            Destroy(_objectToDestroy);
        }
    }
}

