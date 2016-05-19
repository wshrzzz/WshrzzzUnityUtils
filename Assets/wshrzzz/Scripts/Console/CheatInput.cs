using UnityEngine;
using System;
using System.Text;
using System.Collections.Generic;

namespace Wshrzzz.UnityUtils
{
    public class CheatInput : MonoBehaviour
    {
        private static readonly int Cheat_Name_Min_Length = 8;
        private static readonly int Cheat_Name_Max_Length = 30;
        private static readonly string Cheat_Input_Instance_Name = "CheatInput";
        private static readonly string Execute_Cheat_Format = "Execute cheat : {0}";

        public delegate void CheatDelegate();

        private static StringBuilder s_StringBuilder = new StringBuilder();
        private static Dictionary<string, CheatDelegate> s_CheatHandlerDict = new Dictionary<string, CheatDelegate>();

        private static CheatInput s_Instance;
        private static CheatInput Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    GameObject go = new GameObject(Cheat_Input_Instance_Name);
                    go.hideFlags = HideFlags.HideAndDontSave;
                    s_Instance = go.AddComponent<CheatInput>();

                    CheatInput[] cheat = FindObjectsOfType<CheatInput>();
                    for (int i = 0; i < cheat.Length; i++)
                    {
                        if (cheat[i] != s_Instance)
                        {
                            Destroy(cheat[i]);
                        }
                    }
                }
                return s_Instance;
            }
        }

        void Update()
        {
            if (string.IsNullOrEmpty(Input.inputString))
                return;

            HandleInputString();
        }

        private void HandleInputString()
        {
            foreach (char c in Input.inputString)
            {
                if (c == '\b' && s_StringBuilder.Length != 0)
                {
                    s_StringBuilder.Remove(0, 1);
                }
                else if (c != '\r' && c != '\n')
                {
                    s_StringBuilder.Append(c);
                }
                else
                {
                    ExecuteCheat();
                }
            }
        }

        private void ExecuteCheat()
        {
            string command = s_StringBuilder.ToString();
            if (s_CheatHandlerDict.ContainsKey(command))
            {
                Debug.Log(string.Format(Execute_Cheat_Format, command));
                try
                {
                    s_CheatHandlerDict[command]();
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e.ToString());
                }
            }
            s_StringBuilder.Remove(0, s_StringBuilder.Length);
        }

        private bool Add(string cheatName, CheatDelegate handler)
        {
            if (cheatName.Length < Cheat_Name_Min_Length || cheatName.Length > Cheat_Name_Max_Length)
                return false;

            if (s_CheatHandlerDict.ContainsKey(cheatName))
                s_CheatHandlerDict[cheatName] += handler;
            else
                s_CheatHandlerDict[cheatName] = handler;
            return true;
        }

        public static bool AddCheater(string cheatName, CheatDelegate handler)
        {
            return Instance.Add(cheatName, handler);
        }

        private bool Remove(string cheatName, CheatDelegate handler)
        {
            if (!s_CheatHandlerDict.ContainsKey(cheatName))
                return false;

            s_CheatHandlerDict[cheatName] -= handler;
            return true;
        }

        public static bool RemoveCheater(string cheatName, CheatDelegate handler)
        {
            return Instance.Remove(cheatName, handler);
        }
    }
}
