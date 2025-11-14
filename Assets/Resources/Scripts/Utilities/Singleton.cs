using UnityEngine;

namespace COL1.Utilities
{

    public class Singleton
    {
        public static T Get<T>() where T : MonoBehaviour
        {
            return Object.FindAnyObjectByType<T>();
        }

        public static void Make<T>(T self) where T : MonoBehaviour
        {
            var list = Object.FindObjectsByType<T>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            if(list.Length > 1) Object.Destroy(self.gameObject);
            else Object.DontDestroyOnLoad(self.gameObject);
        }

    }

}
