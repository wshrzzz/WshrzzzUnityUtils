using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Wshrzzz.UnityUtils
{
    public class GUILogDisplay : MonoBehaviour
    {
        private static readonly Color Transparent_Color = new Color(0f, 0f, 0f, 0f);

        private static readonly string Debug_Window_Title = "DEBUG";
        private static readonly Vector2 Debug_Window_Position = new Vector2(0f, Screen.height * 0.5f);
        private static readonly Vector2 Debug_Window_Show_Size = new Vector2(Screen.width, Screen.height * 0.5f);
        private static readonly Vector2 Debug_Window_Hide_Size = new Vector2(120, 55f);

        private static readonly Vector2 Control_Bar_Position = new Vector2(20f, 20f);
        private static readonly Vector2 Control_Bar_Size = new Vector2(Debug_Window_Show_Size.x - 40f, 30f);

        private static readonly Vector2 Log_Panel_Position = new Vector2(20f, 55f);
        private static readonly Vector2 Log_Panel_Size = new Vector2(Debug_Window_Show_Size.x - 40f, Debug_Window_Show_Size.y - 45f - Control_Bar_Size.y);

        private static readonly bool Default_Show_Log = true;
        private static readonly bool Default_Auto_Scroll = true;

        public bool ShowWindow = true;

        private GUIWindow m_DebugWindow;
        private ControlBar m_ControlBar;
        private LogPanel m_LogPanel;

        private bool m_ShowLog = Default_Show_Log;
        
        private static GUILogDisplay s_Instance { get; set; }

        #region init
        void Awake()
        {
            if (s_Instance == null)
            {
                s_Instance = this;
            }
            else
            {
                Destroy(this);
            }
            Init();
        }

        void Start()
        {
            CheatInput.AddCheater("showdebug", () => { ShowWindow = true; });
            CheatInput.AddCheater("hidedebug", () => { ShowWindow = false; });
        }

        private void Init()
        {
            m_DebugWindow = new GUIWindow(Debug_Window_Title);
            m_DebugWindow.Position = Debug_Window_Position;
            m_DebugWindow.Size = Debug_Window_Show_Size;

            m_ControlBar = new ControlBar(Control_Bar_Size);
            m_ControlBar.Parent = m_DebugWindow;
            m_ControlBar.Pivot = PivotType.LeftTop;
            m_ControlBar.Position = Control_Bar_Position;
            m_ControlBar.ShowLog = m_ShowLog;
            m_ControlBar.AutoScroll = Default_Auto_Scroll;

            m_LogPanel = new LogPanel(Log_Panel_Size);
            m_LogPanel.Parent = m_DebugWindow;
            m_LogPanel.Pivot = PivotType.LeftTop;
            m_LogPanel.Position = Log_Panel_Position;
            m_LogPanel.AutoScroll = m_ControlBar.AutoScroll;

            m_ControlBar.ShowLogToggledEvent += (s, e) => { m_ShowLog = e.ToggleValue; };
            m_ControlBar.AutoScrollToggledEvent += (s, e) => { m_LogPanel.AutoScroll = e.ToggleValue; };
            m_ControlBar.ClearLogClickedEvent += (s, e) => { m_LogPanel.ClearLog(); };
        } 
        #endregion

        void OnGUI()
        {
            if (!ShowWindow)
                return;
            DrawDebugWindow();
        }

        private void DrawDebugWindow()
        {
            m_DebugWindow.Size = Vector2.Lerp(m_DebugWindow.Size, m_ShowLog ? Debug_Window_Show_Size : Debug_Window_Hide_Size, 0.1f);
            m_DebugWindow.Draw();
        }
        
        #region control bar
        private class ControlBar
        {
            private static readonly float Left_Block_Width = 200f;
            private static readonly string Show_Log_Toggle_Text = "Show Log";
            private static readonly float Show_Log_Toggle_Width = 80f;
            private static readonly string Auto_Scroll_Toggle_Text = "Auto Scroll";
            private static readonly float Auto_Scroll_Toggle_Width = 100f;
            private static readonly string Clear_Log_Button_Text = "Clear Log";
            private static readonly float Clear_Log_Button_Width = 100f;

            public event EventHandler<ToggledEventArgs> ShowLogToggledEvent;
            public event EventHandler<ToggledEventArgs> AutoScrollToggledEvent;
            public event EventHandler<ButtonClickEventArgs> ClearLogClickedEvent;

            private GUIBox m_ControlBar;
            private GUIBox m_ControlBarLeftBlock;
            private GUIToggle m_ShowDebugToggle;
            private GUIToggle m_AutoScrollToggle;
            private GUIBox m_ControlBarRightBlock;
            private GUIButton m_ClearLogButton;

            public bool Enable
            {
                get
                {
                    return m_ControlBar.Enable;
                }
                set
                {
                    m_ControlBar.Enable = value;
                }
            }
            public GUIBase Parent
            {
                get
                {
                    return m_ControlBar.Parent;
                }
                set
                {
                    m_ControlBar.Parent = value;
                }
            }
            public Vector2 Position
            {
                get
                {
                    return m_ControlBar.Position;
                }
                set
                {
                    m_ControlBar.Position = value;
                }
            }
            public PivotType Pivot
            {
                get
                {
                    return m_ControlBar.Pivot;
                }
                set
                {
                    m_ControlBar.Pivot = value;
                }
            }

            public bool ShowLog
            {
                get
                {
                    return m_ShowDebugToggle.ToggleValue;
                }
                set
                {
                    m_ShowDebugToggle.ToggleValue = value;
                }
            }
            public bool AutoScroll
            {
                get
                {
                    return m_AutoScrollToggle.ToggleValue;
                }
                set
                {
                    m_AutoScrollToggle.ToggleValue = value;
                }
            }

            public ControlBar(float width, float height)
            {
                Init(new Vector2(width, height));
            }

            public ControlBar(Vector2 size)
            {
                Init(size);
            }

            private void Init(Vector2 size)
            {
                m_ControlBar = new GUIBox();
                m_ControlBar.Size = size;
                m_ControlBar.Color = Transparent_Color;

                m_ControlBarLeftBlock = new GUIBox();
                m_ControlBarLeftBlock.Parent = m_ControlBar;
                m_ControlBarLeftBlock.Pivot = PivotType.LeftMiddle;
                m_ControlBarLeftBlock.Position = new Vector2(0f, m_ControlBarLeftBlock.Parent.Size.y * 0.5f);
                m_ControlBarLeftBlock.Size = new Vector2(Left_Block_Width, m_ControlBarLeftBlock.Parent.Size.y);
                m_ControlBarLeftBlock.Color = Transparent_Color;

                m_ShowDebugToggle = new GUIToggle(Show_Log_Toggle_Text);
                m_ShowDebugToggle.Parent = m_ControlBarLeftBlock;
                m_ShowDebugToggle.Pivot = PivotType.LeftMiddle;
                m_ShowDebugToggle.Position = new Vector2(0f, m_ShowDebugToggle.Parent.Size.y * 0.5f);
                m_ShowDebugToggle.Size = new Vector2(Show_Log_Toggle_Width, m_ShowDebugToggle.Parent.Size.y);
                m_ShowDebugToggle.ToggleValue = true;
                m_ShowDebugToggle.ToggledEvent += (s, e) => { if (ShowLogToggledEvent != null) ShowLogToggledEvent(s, e); };

                m_AutoScrollToggle = new GUIToggle(Auto_Scroll_Toggle_Text);
                m_AutoScrollToggle.Parent = m_ControlBarLeftBlock;
                m_AutoScrollToggle.Pivot = PivotType.RightMiddle;
                m_AutoScrollToggle.Position = new Vector2(m_AutoScrollToggle.Parent.Size.x, m_AutoScrollToggle.Parent.Size.y * 0.5f);
                m_AutoScrollToggle.Size = new Vector2(Auto_Scroll_Toggle_Width, m_AutoScrollToggle.Parent.Size.y);
                m_AutoScrollToggle.ToggleValue = true;
                m_AutoScrollToggle.ToggledEvent += (s, e) => { if (AutoScrollToggledEvent != null) AutoScrollToggledEvent(s, e); };

                m_ClearLogButton = new GUIButton("Clear Log");
                m_ClearLogButton.Parent = m_ControlBar;
                m_ClearLogButton.Pivot = PivotType.RightMiddle;
                m_ClearLogButton.Position = new Vector2(m_ClearLogButton.Parent.Size.x, m_ClearLogButton.Parent.Size.y * 0.5f);
                m_ClearLogButton.Size = new Vector2(Clear_Log_Button_Width, m_ClearLogButton.Parent.Size.y * 0.8f);
                m_ClearLogButton.ButtonClickEvent += (s, e) => { if (ClearLogClickedEvent != null) ClearLogClickedEvent(s, e); };
            }
        } 
        #endregion

        #region log panel
        private class LogPanel
        {
            private static readonly float Log_Scroll_View_Right_Hold = 30f;
            private static readonly float Log_Scroll_View_Default_Content_Height = 20f;
            private static readonly float Log_Box_Default_Y_Offset = 10f;
            private static readonly float Log_Box_Left_Right_Margin = 20f;
            private static readonly float Log_Box_Spacing_Distance = 5f;

            private GUIBox m_LogPanel;
            private GUIScrollView m_LogScrollView;
            private Vector2 m_Size;

            public bool Enable
            {
                get
                {
                    return m_LogPanel.Enable;
                }
                set
                {
                    m_LogPanel.Enable = value;
                }
            }
            public GUIBase Parent
            {
                get
                {
                    return m_LogPanel.Parent;
                }
                set
                {
                    m_LogPanel.Parent = value;
                }
            }
            public PivotType Pivot
            {
                get
                {
                    return m_LogPanel.Pivot;
                }
                set
                {
                    m_LogPanel.Pivot = value;
                }
            }
            public Vector2 Position
            {
                get
                {
                    return m_LogPanel.Position;
                }
                set
                {
                    m_LogPanel.Position = value;
                }
            }
            public bool AutoScroll { get; set; }
            
            private float m_LogBoxStartY;

            public LogPanel(float width, float height)
            {
                Init(new Vector2(width, height));
            }

            public LogPanel(Vector2 size)
            {
                Init(size);
            }

            private void Init(Vector2 size)
            {
                m_Size = size;
                if (m_LogPanel == null)
                {
                    m_LogPanel = new GUIBox();
                    m_LogPanel.Size = size;
                }

                if (m_LogScrollView != null)
                {
                    m_LogScrollView.Parent = null;
                }
                m_LogScrollView = new GUIScrollView();
                m_LogScrollView.Parent = m_LogPanel;
                m_LogScrollView.Pivot = PivotType.LeftTop;
                m_LogScrollView.Position = Vector2.zero;
                m_LogScrollView.Size = m_LogScrollView.Parent.Size;
                m_LogScrollView.ContentSize = new Vector2(m_LogScrollView.Size.x - Log_Scroll_View_Right_Hold, Log_Scroll_View_Default_Content_Height);

                m_LogBoxStartY = Log_Box_Default_Y_Offset;
            }

            public void AddLog(LogType type, string logStr)
            {
                LogBox logBox = new LogBox(type, logStr, m_LogScrollView.Size.x - Log_Box_Left_Right_Margin * 2);
                logBox.Parent = m_LogScrollView;
                logBox.Pivot = PivotType.LeftTop;
                logBox.Position = new Vector2(10f, m_LogBoxStartY);

                float logBoxSizeYWithSpace = logBox.RealSize.y + Log_Box_Spacing_Distance;
                m_LogBoxStartY += logBoxSizeYWithSpace;
                m_LogScrollView.ContentSize = new Vector2(m_LogScrollView.ContentSize.x, m_LogScrollView.ContentSize.y + logBoxSizeYWithSpace);
                if (AutoScroll) m_LogScrollView.ScrollPosition = m_LogScrollView.ContentSize;
            }

            public void ClearLog()
            {
                Init(m_Size);
            }

            #region log box
            private class LogBox
            {
                private static readonly float Log_Font_Size = 15f;
                private static readonly float Log_Font_Top_Bottom_Margin = 3f;
                private static readonly float Log_Text_Left_Right_Margin = 5f;

                public bool Enable
                {
                    get
                    {
                        return m_LogBox.Enable;
                    }
                    set
                    {
                        m_LogBox.Enable = value;
                    }
                }
                public Vector2 RealSize { get; private set; }
                public GUIBase Parent
                {
                    get
                    {
                        return m_LogBox.Parent;
                    }
                    set
                    {
                        m_LogBox.Parent = value;
                    }
                }
                public Vector2 Position
                {
                    get
                    {
                        return m_LogBox.Position;
                    }
                    set
                    {
                        m_LogBox.Position = value;
                    }
                }
                public PivotType Pivot
                {
                    get
                    {
                        return m_LogBox.Pivot;
                    }
                    set
                    {
                        m_LogBox.Pivot = value;
                    }
                }

                private GUIButton m_LogBox;

                public LogBox(LogType type, string log, float width)
                {
                    Init(type, log, width);
                }

                private void Init(LogType type, string log, float width)
                {
                    float height = log.Split('\n').Length * Log_Font_Size + Log_Font_Top_Bottom_Margin * 2;
                    RealSize = new Vector2(width, height);

                    m_LogBox = new GUIButton();
                    m_LogBox.Size = new Vector2(width, height);
                    switch (type)
                    {
                        case LogType.Log:
                            m_LogBox.Color = Color.white;
                            break;
                        case LogType.LogWarning:
                            m_LogBox.Color = Color.yellow;
                            break;
                        case LogType.LogError:
                            m_LogBox.Color = Color.red;
                            break;
                        default:
                            break;
                    }

                    GUILabel logLabel = new GUILabel(log);
                    logLabel.Parent = m_LogBox;
                    logLabel.Pivot = PivotType.LeftTop;
                    logLabel.Position = new Vector2(Log_Text_Left_Right_Margin, 0f);
                    logLabel.Size = new Vector2(logLabel.Parent.Size.x - Log_Text_Left_Right_Margin * 2, logLabel.Parent.Size.y);
                }
            } 
            #endregion
        }
        #endregion

        private enum LogType
        {
            Log,
            LogWarning,
            LogError,
        }

        public static void Log(object log)
        {
            InternalLog(LogType.Log, log);
        }

        public static void LogWarning(object log)
        {
            InternalLog(LogType.LogWarning, log);
        }

        public static void LogError(object log)
        {
            InternalLog(LogType.LogError, log);
        }

        private static void InternalLog(LogType type, object log)
        {
            string logStr = log.ToString();
            switch (type)
            {
                case LogType.Log:
                    Debug.Log(logStr);
                    break;
                case LogType.LogWarning:
                    Debug.LogWarning(logStr);
                    break;
                case LogType.LogError:
                    Debug.LogError(logStr);
                    break;
                default:
                    break;
            }
            
            if (s_Instance != null)
            {
                s_Instance.m_LogPanel.AddLog(type, logStr);
            }
        }
    }
}