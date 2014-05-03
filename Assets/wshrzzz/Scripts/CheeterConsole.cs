using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Wshrzzz.UnityUtil
{
    /// <summary>
    /// Cheeter console. Invisible input and activate cheeter.
    /// </summary>
    public class CheeterConsole : MonoBehaviour
    {
        public delegate void CheetDelegate(string cheeterName);
        public CheetDelegate OnCheet;

        private string CheeterInput { get; set; }

        void Update()
        {
            CheeterInput += Input.inputString;
            if (CheeterInput.Length > 30)
            {
                CheeterInput = CheeterInput.Substring(CheeterInput.Length - 30, 30);
            }
            if (CheeterHash != null)
            {
                for (int cheeterLength = 1; cheeterLength <= LonggestCheeterLength && cheeterLength <= CheeterInput.Length; cheeterLength++ )
                {
                    string str = CheeterInput.Substring(CheeterInput.Length - cheeterLength, cheeterLength);
                    if (CheeterHash.ContainsKey(str))
                    {
                        CheeterInput = "";
                        Debug.Log("On cheet [" +  str + "].");
                        if (OnCheet != null)
                        {
                            OnCheet(str);
                        }
                    }
                }
            }
        }

        private static Hashtable CheeterHash { get; set; }
        private static int LonggestCheeterLength { get; set; }

        /// <summary>
        /// Add a cheeter. If something wrong, it will give some warning.
        /// </summary>
        /// <param name="cheeterName">Cheeter name.</param>
        /// <returns>Is success?</returns>
        public static bool AddCheeter(string cheeterName)
        {
            if (CheeterHash == null)
            {
                CheeterHash = new Hashtable();
            }
            if (cheeterName.Length > 30 || cheeterName.Length < 5)
            {
                cheeterName = cheeterName.Substring(0, 30);
                Debug.LogWarning("Cheeter name is limited in 5-30 chars.");
            }
            if (!CheeterHash.ContainsKey(cheeterName))
            {
                CheeterHash.Add(cheeterName, cheeterName.Length);
                if (cheeterName.Length > LonggestCheeterLength)
                {
                    LonggestCheeterLength = cheeterName.Length;
                }
                Debug.Log("Add cheeter [" + cheeterName + "].");
                return true;
            }
            else
            {
                Debug.Log("Already exist cheeter [" + cheeterName + "].");
                return false;
            }
        }

        /// <summary>
        /// Remove a cheeter. If something wrong, it will give some warning.
        /// </summary>
        /// <param name="cheeterName">Cheeter name.</param>
        /// <returns>Is success?</returns>
        public static bool RemoveCheeter(string cheeterName)
        {
            if (CheeterHash == null)
            {
                CheeterHash = new Hashtable();
                Debug.LogWarning("There isn't a cheeter.");
                return false;
            }
            if (CheeterHash.ContainsKey(cheeterName))
            {
                CheeterHash.Remove(cheeterName);
                Debug.Log("Remove cheeter [" + cheeterName + "].");

                if (cheeterName.Length == LonggestCheeterLength)
                {
                    for (int length = cheeterName.Length; length >= 5; length--)
                    {
                        if (CheeterHash.ContainsValue(length))
                        {
                            LonggestCheeterLength = length;
                            return true;
                        }
                    }
                    LonggestCheeterLength = -1;
                }
                return true;
            }
            else
            {
                Debug.LogWarning("Cheeter [" + cheeterName + "] isn't exist.");
                return false;
            }
        }

        private static CheeterConsole mInstance = null;

        public static CheeterConsole GetInstance()
        {
            if (mInstance != null)
            {
                return mInstance;
            }
            else
            {
                mInstance = FindObjectOfType<CheeterConsole>();
                if (mInstance != null)
                {
                    return mInstance;
                }
                else
                {
                    GameObject go = new GameObject();
                    go.name = "CheeterManager";
                    go.AddComponent<CheeterConsole>();
                    mInstance = go.GetComponent<CheeterConsole>();
                    return mInstance;
                }
            }
        }
    }
}
