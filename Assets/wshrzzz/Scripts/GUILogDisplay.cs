using UnityEngine;
using System.Collections;

namespace Wshrzzz.UnityUtil
{
    /// <summary>
    /// Print the log on Unity GUI.
    /// It can be used sometimes where can't easily see system log.
    /// </summary>
    public class GUILogDisplay : MonoBehaviour
    {
        private Vector2 mScrollV2 = new Vector2(0f, 0f);
        private Vector2 mTempV2 = new Vector2(0f, 1000f);
        private bool mIsAutoScroll = true;
        private float mWinWidth;
        private float mWinHeight;
        private Rect mMyDebugWindow0;
        private bool mShowWin = true;

        /// <summary>
        /// Whether show debug log window.
        /// </summary>
        public bool showLog = true;

        void Awake()
        {
            isWork = true;
        }

        void Start()
        {
            mWinWidth = Screen.width;
            mWinHeight = Screen.height * 0.4f;
            mMyDebugWindow0 = new Rect(0f, Screen.height * 0.6f, mWinWidth, mWinHeight);
        }

        void OnGUI()
        {
            if (showLog)
            {
                if (mShowWin)
                {
                    mMyDebugWindow0.width = mWinWidth;
                    mMyDebugWindow0.height = mWinHeight;
                }
                else
	            {
                    mMyDebugWindow0.width = 0f;
                    mMyDebugWindow0.height = 0f;
	            }
                mMyDebugWindow0 = GUILayout.Window(1226, mMyDebugWindow0, DisplayMyLog, "DEBUG");
            }
        }

        /// <summary>
        /// My window function.
        /// </summary>
        void DisplayMyLog(int winID)
        {
            GUILayout.BeginHorizontal("Label");
            mShowWin = GUILayout.Toggle(mShowWin, "Show Debug", GUILayout.MinWidth(Screen.width * 0.1f), GUILayout.ExpandWidth(false));
            if (mShowWin)
            {
                mIsAutoScroll = GUILayout.Toggle(mIsAutoScroll, "Auto Scroll", GUILayout.MinWidth(Screen.width * 0.1f), GUILayout.ExpandWidth(false));
                if (GUILayout.Button("Clean Log", GUILayout.ExpandWidth(false)))
                {
                    logInfo = "";
                }
            }
            GUILayout.EndHorizontal();

            if (mShowWin)
            {
                GUILayout.BeginHorizontal("Label");
                GUILayout.Label("Width", GUILayout.ExpandWidth(false));
                mWinWidth = GUILayout.HorizontalSlider(mWinWidth, Screen.width * 0.5f, Screen.width, GUILayout.MinWidth(Screen.width * 0.2f), GUILayout.ExpandWidth(false));
                GUILayout.Label("Height", GUILayout.ExpandWidth(false));
                mWinHeight = GUILayout.HorizontalSlider(mWinHeight, Screen.height * 0.3f, Screen.height, GUILayout.MinWidth(Screen.width * 0.2f), GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();

                if (mIsAutoScroll)
                {
                    mScrollV2 += mTempV2;
                }
                mScrollV2 = GUILayout.BeginScrollView(mScrollV2);
                GUILayout.Box(logInfo);
                GUILayout.EndScrollView();
            }

            GUI.DragWindow(new Rect(0f, 0f, 10000f, 10000f));
        }

        private static string logInfo = "";
        private static int logCount = 0;
        private static int maxLogNumber = 400;
        private static bool isWork = false;
        
        /// <summary>
        /// Use this static method to print log on GUI window.
        /// </summary>
        /// <param name="log">Log to print.</param>
        public static void PrintLog(string log)
        {
            if (isWork)
            {
                logCount++;
                if (logCount > maxLogNumber || logInfo.Length > 10000)
                {
                    logInfo = logInfo.Substring(logInfo.IndexOf("\n") + 1);
                }
                logInfo += log + "\n";
            }
        }
    }
}