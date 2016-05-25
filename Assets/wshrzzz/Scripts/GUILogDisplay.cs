using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Wshrzzz.UnityUtils
{
    public class GUILogDisplay : MonoBehaviour
    {
        private static readonly Vector2 Debug_Window_Position = new Vector2(0f, Screen.height * 0.5f);
        private static readonly Vector2 Debug_Window_Show_Size = new Vector2(Screen.width, Screen.height * 0.5f);

        public bool ShowWindow = true;
        private DebugWindow m_DebugWindow;
        
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
            m_DebugWindow = new DebugWindow();
            m_DebugWindow.ShowLog = ShowWindow;
            m_DebugWindow.Pivot = PivotType.LeftTop;
            m_DebugWindow.Position = Debug_Window_Position;
            m_DebugWindow.PaddingAll = 20f;
            m_DebugWindow.ShowSize = Debug_Window_Show_Size;
        }

        void Start()
        {
            CheatInput.AddCheater("showdebug", () => { ShowWindow = true; });
            CheatInput.AddCheater("hidedebug", () => { ShowWindow = false; });
        }
        #endregion
        void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                LogError("qwer");
            }
        }
        void OnGUI()
        {
            if (!ShowWindow)
                return;
            m_DebugWindow.Draw();
        }

        private class DebugWindow : GUIWindow
        {
            private static readonly string Debug_Window_Title = "DEBUG";
            private static readonly Vector2 Debug_Window_Hide_Size = new Vector2(120f, 60f);

            private static readonly Vector2 Control_Bar_Position = new Vector2(20f, 20f);
            private static readonly float Control_Bar_Height = 20f;
            private static readonly Vector2 Log_Panel_Position = new Vector2(20f, 55f);
            private static float Sub_Control_Margin = 20f;
            private static float Spacing_Distance = 5f;

            private static float Show_Hide_Anim_Speed = 0.2f;

            private static readonly bool Default_Show_Log = true;
            private static readonly bool Default_Auto_Scroll = true;

            public bool ShowLog = true;

            private ControlBar m_ControlBar;
            private LogPanel m_LogPanel;

            private bool m_ShowLog = Default_Show_Log;
            private Vector2 m_ShowSize;
            public Vector2 ShowSize
            {
                get
                {
                    return m_ShowSize;
                }
                set
                {
                    m_ShowSize = value;
                    Size = value;
                }
            }

            public DebugWindow() : base(Debug_Window_Title)
            {
                Init();
            }

            private void Init()
            {
                m_ControlBar = new ControlBar();
                m_ControlBar.Parent = this;
                m_ControlBar.Location = LocationType.Relative;
                m_ControlBar.Margin = MarginType.Top | MarginType.FixedHeight;
                m_ControlBar.Size = new Vector2(0f, Control_Bar_Height);
                m_ControlBar.ShowLog = m_ShowLog;
                m_ControlBar.AutoScroll = Default_Auto_Scroll;

                m_LogPanel = new LogPanel();
                m_LogPanel.Parent = this;
                m_LogPanel.Location = LocationType.Relative;
                m_LogPanel.Margin = MarginType.Top;
                m_LogPanel.MarginTop = 35f;
                m_LogPanel.AutoScroll = m_ControlBar.AutoScroll;

                m_ControlBar.ShowLogToggledEvent += (s, e) => { m_ShowLog = e.ToggleValue; m_LogPanel.Enable = e.ToggleValue; };
                m_ControlBar.AutoScrollToggledEvent += (s, e) => { m_LogPanel.AutoScroll = e.ToggleValue; };
                m_ControlBar.ClearLogClickedEvent += (s, e) => { m_LogPanel.ClearLog(); };
            }

            protected override void GUIDraw()
            {
                if (Vector2.Distance(m_ShowLog ? m_ShowSize : Debug_Window_Hide_Size, DrawingRect.size) > 1f)
                {
                    Size = Vector2.Lerp(DrawingRect.size, m_ShowLog ? m_ShowSize : Debug_Window_Hide_Size, Show_Hide_Anim_Speed);
                }
                base.GUIDraw();
            }

            public void AddLog(LogType type, string log)
            {
                m_LogPanel.AddLog(type, log);
            }
        }
        
        #region control bar
        private class ControlBar : GUIBox
        {
            private static readonly Color Transparent_Color = new Color(0f, 0f, 0f, 0f);
            private static readonly float Left_Block_Width = 200f;
            private static readonly string Show_Log_Toggle_Text = "Show Log";
            private static readonly float Show_Log_Toggle_Width = 80f;
            private static readonly string Auto_Scroll_Toggle_Text = "Auto Scroll";
            private static readonly float Auto_Scroll_Toggle_Width = 100f;
            private static readonly string Clear_Log_Button_Text = "Clear Log";
            private static readonly float Clear_Log_Button_Width = 100f;
            private static readonly float Clear_Log_Button_Height = 20f;

            public event EventHandler<ToggledEventArgs> ShowLogToggledEvent;
            public event EventHandler<ToggledEventArgs> AutoScrollToggledEvent;
            public event EventHandler<ButtonClickEventArgs> ClearLogClickedEvent;

            private GUIBox m_ControlBarLeftBlock;
            private GUIToggle m_ShowDebugToggle;
            private GUIToggle m_AutoScrollToggle;
            private GUIBox m_ControlBarRightBlock;
            private GUIButton m_ClearLogButton;

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

            public ControlBar()
            {
                Init();
            }

            private void Init()
            {
                Color = Transparent_Color;

                m_ControlBarLeftBlock = new GUIBox();
                m_ControlBarLeftBlock.Parent = this;
                m_ControlBarLeftBlock.Location = LocationType.Relative;
                m_ControlBarLeftBlock.Margin = MarginType.Left | MarginType.FixedWidth;
                m_ControlBarLeftBlock.Size = new Vector2(Left_Block_Width, 0f);
                m_ControlBarLeftBlock.Color = Transparent_Color;

                m_ShowDebugToggle = new GUIToggle(Show_Log_Toggle_Text);
                m_ShowDebugToggle.Parent = m_ControlBarLeftBlock;
                m_ShowDebugToggle.Location = LocationType.Relative;
                m_ShowDebugToggle.Margin = MarginType.Left | MarginType.FixedWidth;
                m_ShowDebugToggle.Size = new Vector2(Show_Log_Toggle_Width, 0f);
                m_ShowDebugToggle.ToggleValue = true;
                
                m_AutoScrollToggle = new GUIToggle(Auto_Scroll_Toggle_Text);
                m_AutoScrollToggle.Parent = m_ControlBarLeftBlock;
                m_AutoScrollToggle.Location = LocationType.Relative;
                m_AutoScrollToggle.Margin = MarginType.Right | MarginType.FixedWidth;
                m_AutoScrollToggle.Size = new Vector2(Auto_Scroll_Toggle_Width, 0f);
                m_AutoScrollToggle.ToggleValue = true;
                
                m_ClearLogButton = new GUIButton(Clear_Log_Button_Text);
                m_ClearLogButton.Parent = this;
                m_ClearLogButton.Location = LocationType.Relative;
                m_ClearLogButton.Margin = MarginType.Right | MarginType.FixedWidth | MarginType.FixedHeight;
                m_ClearLogButton.Size = new Vector2(Clear_Log_Button_Width, Clear_Log_Button_Height);

                m_ShowDebugToggle.ToggledEvent += (s, e) => { if (ShowLogToggledEvent != null) ShowLogToggledEvent(s, e); m_AutoScrollToggle.Enable = e.ToggleValue; m_ClearLogButton.Enable = e.ToggleValue; };
                m_AutoScrollToggle.ToggledEvent += (s, e) => { if (AutoScrollToggledEvent != null) AutoScrollToggledEvent(s, e); };
                m_ClearLogButton.ButtonClickEvent += (s, e) => { if (ClearLogClickedEvent != null) ClearLogClickedEvent(s, e); };
            }

            protected override void GUIDraw()
            {
                base.GUIDraw();
            }
        } 
        #endregion

        #region log panel
        private class LogPanel : GUIScrollView
        {
            private static readonly float Log_Scroll_View_Right_Hold = 30f;
            private static readonly float Log_Scroll_View_Content_Top_Bottom_Margin = 10f;
            private static readonly float Log_Box_Spacing_Distance = 5f;
            private static readonly float Log_Item_Pad_In = 10f;

            private Rect m_ContentRect;
            private GUIBox m_Box;

            public bool AutoScroll { get; set; }
            
            private float m_LogBoxStartY;

            public LogPanel()
            {
                Init();
            }

            private void Init()
            {
                m_LogBoxStartY = Log_Scroll_View_Content_Top_Bottom_Margin;

                DetachAllChildren();
                m_Box = new GUIBox();
                m_Box.Parent = this;
                m_Box.Location = LocationType.Relative;
                m_Box.Margin = MarginType.Fit;

                Resize();
            }

            protected override void Resize()
            {
                base.Resize();
                m_ContentRect = new Rect(0f, 0f, DrawingRect.width - Log_Scroll_View_Right_Hold, Mathf.Max((m_LogBoxStartY + Log_Scroll_View_Content_Top_Bottom_Margin), DrawingRect.height));
                ContentDrawingRect = m_ContentRect;
            }

            public void AddLog(LogType type, string logStr)
            {
                LogBox logBox = new LogBox(type, logStr);
                logBox.Parent = m_Box;
                logBox.Location = LocationType.Relative;
                logBox.Margin = MarginType.Left | MarginType.Right | MarginType.Top | MarginType.FixedHeight;
                logBox.MarginLeftRight = Log_Item_Pad_In;
                logBox.MarginTop = m_LogBoxStartY;
                logBox.Size = new Vector2(0f, logBox.RealHeight);

                float logBoxSizeYWithSpace = logBox.RealHeight + Log_Box_Spacing_Distance;
                m_LogBoxStartY += logBoxSizeYWithSpace;

                UpdateRect();

                if (AutoScroll) ScrollPosition = ContentDrawingRect.size;
            }

            public void ClearLog()
            {
                Init();
            }

            #region log box
            private class LogBox : GUIButton
            {
                private static readonly float Log_Font_Size = 15f;
                private static readonly float Log_Font_Top_Bottom_Margin = 3f;
                private static readonly float Log_Text_Left_Right_Margin = 10f;

                private static readonly Color Normal_Log_Color = Color.white;
                private static readonly Color Warning_Log_Color = Color.yellow;
                private static readonly Color Error_Log_Color = Color.red;

                public float RealHeight { get; private set; }
                private string m_Log;

                private GUILabel m_LogLabel;

                public LogBox(LogType type, string log)
                {
                    Init(type, log);
                }

                private void Init(LogType type, string log)
                {
                    m_Log = log;

                    switch (type)
                    {
                        case LogType.Log:
                            Color = Normal_Log_Color;
                            break;
                        case LogType.LogWarning:
                            Color = Warning_Log_Color;
                            break;
                        case LogType.LogError:
                            Color = Error_Log_Color;
                            break;
                        default:
                            break;
                    }

                    m_LogLabel = new GUILabel(log);
                    m_LogLabel.Parent = this;
                    m_LogLabel.Location = LocationType.Relative;
                    m_LogLabel.Margin = MarginType.Left | MarginType.Right;
                    m_LogLabel.MarginLeftRight = Log_Text_Left_Right_Margin;

                    RealHeight = m_Log.Split('\n').Length * Log_Font_Size + Log_Font_Top_Bottom_Margin * 2;
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
                s_Instance.m_DebugWindow.AddLog(type, logStr);
            }
        }
    }
}