using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Wshrzzz.UnityUtil
{
    public class ConsoleBasic : MonoBehaviour
    {
        private bool m_ShowConsole = false;

        private 

        void Start()
        {
            CheeterConsole.AddCheeter("openmyconsole", () => { m_ShowConsole = true; });
        }

        void OnGUI()
        {
            if (m_ShowConsole)
            {
                GUILayout.BeginArea(new Rect(0, Screen.height - 25, Screen.width, 25));
                GUI.SetNextControlName("ConsoleString");
                s_ConsoleStr = GUILayout.TextField(s_ConsoleStr);
                GUI.FocusControl("ConsoleString");
                GUILayout.EndArea();
                if (Event.current.keyCode == KeyCode.Return && Event.current.type == EventType.KeyUp)
                {
                    DealCommand(s_ConsoleStr);
                }
            }
        }

        void DealCommand(string consoleStr){
            s_ConsoleStr = "";
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

