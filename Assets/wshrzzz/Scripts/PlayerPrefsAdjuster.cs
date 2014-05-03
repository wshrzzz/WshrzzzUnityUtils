using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Wshrzzz.UnityUtil
{
    /// <summary>
    /// Set PlayerPrefs and get info display on GUI with PlayerPrefsAdjuster.
    /// </summary>
    public class PlayerPrefsAdjuster : MonoBehaviour
    {
        private List<PrefsListItem> MyChangeList { get; set; }
        private List<PrefsListItem> MyRemoveList { get; set; }

        void Update()
        {
            foreach (var item in MyChangeList)
            {
                item.ShowCountDown -= Time.deltaTime;
                if (item.ShowCountDown <= 0f)
                {
                    MyRemoveList.Add(item);
                }
            }
            foreach (var item in MyRemoveList)
            {
                MyChangeList.Remove(item);
            }
            MyRemoveList.Clear();
        }

        void OnGUI()
        {
            foreach (var item in MyChangeList)
            {
                GUILayout.Box(item.PrefsName + " : " + item.Value);
            }
        }

        private static PlayerPrefsAdjuster mInstance = null;

        public static PlayerPrefsAdjuster GetInstance()
        {
            if (mInstance == null)
            {
                GameObject go = new GameObject();
                go.name = "PlayerPrefsAdjuster";
                go.AddComponent<PlayerPrefsAdjuster>();
                mInstance = go.GetComponent<PlayerPrefsAdjuster>();
                mInstance.MyChangeList = new List<PrefsListItem>();
                mInstance.MyRemoveList = new List<PrefsListItem>();
            }
            return mInstance;
        }

        public static void ChangeSetting(string prefsName, int value)
        {
            GetInstance();
            PlayerPrefs.SetInt(prefsName, value);
            foreach (var item in mInstance.MyChangeList)
            {
                if (item.PrefsName == prefsName)
                {
                    item.Value = value.ToString();
                    item.ShowCountDown = 2.5f;
                    return;
                }
            }
            PrefsListItem newItem = new PrefsListItem() { PrefsName = prefsName, Value = value.ToString(), ShowCountDown = 2.5f };
            mInstance.MyChangeList.Add(newItem);
        }

        public static void ChangeSetting(string prefsName, float value)
        {
            GetInstance();
            PlayerPrefs.SetFloat(prefsName, value);
            foreach (var item in mInstance.MyChangeList)
            {
                if (item.PrefsName == prefsName)
                {
                    item.Value = value.ToString("F3");
                    item.ShowCountDown = 2.5f;
                    return;
                }
            }
            PrefsListItem newItem = new PrefsListItem() { PrefsName = prefsName, Value = value.ToString("F3"), ShowCountDown = 2.5f };
            mInstance.MyChangeList.Add(newItem);
        }

        public static void ChangeSetting(string prefsName, string value)
        {
            GetInstance();
            PlayerPrefs.SetString(prefsName, value);
            foreach (var item in mInstance.MyChangeList)
            {
                if (item.PrefsName == prefsName)
                {
                    item.Value = value;
                    item.ShowCountDown = 2.5f;
                    return;
                }
            }
            PrefsListItem newItem = new PrefsListItem() { PrefsName = prefsName, Value = value, ShowCountDown = 2.5f };
            mInstance.MyChangeList.Add(newItem);
        }

        public class PrefsListItem
        {
            public string PrefsName { get; set; }
            public string Value { get; set; }
            public float ShowCountDown { get; set; }
        }
    }
}