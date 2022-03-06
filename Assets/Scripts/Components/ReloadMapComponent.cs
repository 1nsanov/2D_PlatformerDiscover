using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Components
{
    public class ReloadMapComponent : MonoBehaviour
    {
        public void ReloadMap()
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
            DestroyObjectComponent.TotalSumCoins = 0;
        }
    }
}

