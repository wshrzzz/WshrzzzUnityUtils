using UnityEngine;
using System.Collections;

namespace Wshrzzz.UnityUtil
{
    public class ConsoleBasic : MonoBehaviour
    {
        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(0, Screen.height - 25, Screen.width, 25));
            s_ConsoleStr = GUILayout.TextField(s_ConsoleStr);
            GUILayout.EndArea();
        }

        void Update()
        {
            if (s_ConsoleStr != "" && s_ConsoleStr.Substring(s_ConsoleStr.Length - 1, 1) == "\n")
            {
                Debug.Log("asdf");
            }
        }

        private static string s_ConsoleStr = "";
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
                    GameObject go = new GameObject();
                    s_Instance = go.AddComponent<ConsoleBasic>();
                    return s_Instance;
                }
            }
        }
    }
}

