using UnityEngine;
using System.Collections;

namespace Wshrzzz.UnityUtil
{
    public class ConsoleBasic : MonoBehaviour
    {
        private static ConsoleBasic s_Instance = null;
        public static ConsoleBasic Instance
        {
            get
            {
                if (s_Instance != null)
                {
                    return s_Instance;
                }
                else
                {
                    s_Instance = FindObjectOfType<ConsoleBasic>();
                    if (s_Instance != null)
                    {
                        return s_Instance;
                    }
                    else
                    {
                        GameObject go = new GameObject();
                        s_Instance = go.AddComponent<ConsoleBasic>();
                        return s_Instance;
                    }
                }
            }
        }
    }
}

