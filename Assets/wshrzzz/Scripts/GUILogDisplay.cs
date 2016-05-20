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
            m_DebugWindow.ShowWindow = ShowWindow;
            m_DebugWindow.Pivot = PivotType.LeftTop;
            m_DebugWindow.Position = Debug_Window_Position;
            m_DebugWindow.Size = Debug_Window_Show_Size;
        }

        void Start()
        {
            CheatInput.AddCheater("showdebug", () => { m_DebugWindow.ShowWindow = true; });
            CheatInput.AddCheater("hidedebug", () => { m_DebugWindow.ShowWindow = false; });
        }
        #endregion
        void Update()
        {
            Log("asdf");
        }
        void OnGUI()
        {
            if (!ShowWindow)
                return;
            m_DebugWindow.Draw();
        }

        private class DebugWindow : GUIBase
        {
            private static readonly string Debug_Window_Title = "DEBUG";
            private static readonly Vector2 Debug_Window_Hide_Size = new Vector2(120, 55f);

            private static readonly Vector2 Control_Bar_Position = new Vector2(20f, 20f);
            private static readonly float Control_Bar_Height = 30f;
            private static readonly Vector2 Log_Panel_Position = new Vector2(20f, 55f);
            private static float Sub_Control_Margin = 20f;
            private static float Spacing_Distance = 5f;

            private static float Show_Hide_Anim_Speed = 0.2f;

            private static readonly bool Default_Show_Log = true;
            private static readonly bool Default_Auto_Scroll = true;

            public bool ShowWindow = true;

            private GUIWindow m_DebugWindow;
            private ControlBar m_ControlBar;
            private LogPanel m_LogPanel;

            private bool m_ShowLog = Default_Show_Log;
            private Vector2 m_ShowSize;

            public DebugWindow()
            {
                Init();
            }

            private void Init()
            {
                m_DebugWindow = new GUIWindow(Debug_Window_Title);
                SetBaseControl(m_DebugWindow);

                m_ControlBar = new ControlBar();
                m_ControlBar.Parent = m_DebugWindow;
                m_ControlBar.Pivot = PivotType.LeftTop;
                m_ControlBar.ShowLog = m_ShowLog;
                m_ControlBar.AutoScroll = Default_Auto_Scroll;

                m_LogPanel = new LogPanel();
                m_LogPanel.Parent = m_DebugWindow;
                m_LogPanel.Pivot = PivotType.LeftTop;
                m_LogPanel.AutoScroll = m_ControlBar.AutoScroll;

                m_ControlBar.ShowLogToggledEvent += (s, e) => { m_ShowLog = e.ToggleValue; };
                m_ControlBar.AutoScrollToggledEvent += (s, e) => { m_LogPanel.AutoScroll = e.ToggleValue; };
                m_ControlBar.ClearLogClickedEvent += (s, e) => { m_LogPanel.ClearLog(); };

                Resize();
            }

            protected override void Resize()
            {
                m_ShowSize = Size;
                m_ControlBar.Position = Control_Bar_Position;
                m_ControlBar.Size = new Vector2(m_ShowSize.x - Sub_Control_Margin * 2, Control_Bar_Height);
                m_LogPanel.Position = Log_Panel_Position;
                m_LogPanel.Size = new Vector2(m_ShowSize.x - Sub_Control_Margin * 2, m_ShowSize.y - Sub_Control_Margin * 2 - Spacing_Distance - m_ControlBar.Size.y);
            }

            protected override void GUIDraw()
            {
                UniqueDraw(() =>
                {
                    m_DebugWindow.Size = Vector2.Lerp(m_DebugWindow.Size, m_ShowLog ? m_ShowSize : Debug_Window_Hide_Size, Show_Hide_Anim_Speed);
                    m_DebugWindow.Draw();
                });
            }

            public void AddLog(LogType type, string log)
            {
                m_LogPanel.AddLog(type, log);
            }
        }
        
        #region control bar
        private class ControlBar : GUIBase
        {
            private static readonly Color Transparent_Color = new Color(0f, 0f, 0f, 0f);
            private static readonly float Left_Block_Width = 200f;
            private static readonly string Show_Log_Toggle_Text = "Show Log";
            private static readonly float Show_Log_Toggle_Width = 80f;
            private static readonly string Auto_Scroll_Toggle_Text = "Auto Scroll";
            private static readonly float Auto_Scroll_Toggle_Width = 100f;
            private static readonly string Clear_Log_Button_Text = "Clear Log";
            private static readonly float Clear_Log_Button_Width = 100f;
            private static readonly float Clear_Log_Button_Height_Percent = 0.7f;

            public event EventHandler<ToggledEventArgs> ShowLogToggledEvent;
            public event EventHandler<ToggledEventArgs> AutoScrollToggledEvent;
            public event EventHandler<ButtonClickEventArgs> ClearLogClickedEvent;

            private GUIBox m_ControlBar;
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
                m_ControlBar = new GUIBox();
                m_ControlBar.Color = Transparent_Color;
                SetBaseControl(m_ControlBar);

                m_ControlBarLeftBlock = new GUIBox();
                m_ControlBarLeftBlock.Parent = m_ControlBar;
                m_ControlBarLeftBlock.Pivot = PivotType.LeftMiddle;
                m_ControlBarLeftBlock.Color = Transparent_Color;

                m_ShowDebugToggle = new GUIToggle(Show_Log_Toggle_Text);
                m_ShowDebugToggle.Parent = m_ControlBarLeftBlock;
                m_ShowDebugToggle.Pivot = PivotType.LeftMiddle;
                m_ShowDebugToggle.ToggleValue = true;
                m_ShowDebugToggle.ToggledEvent += (s, e) => { if (ShowLogToggledEvent != null) ShowLogToggledEvent(s, e); };

                m_AutoScrollToggle = new GUIToggle(Auto_Scroll_Toggle_Text);
                m_AutoScrollToggle.Parent = m_ControlBarLeftBlock;
                m_AutoScrollToggle.Pivot = PivotType.RightMiddle;
                m_AutoScrollToggle.ToggleValue = true;
                m_AutoScrollToggle.ToggledEvent += (s, e) => { if (AutoScrollToggledEvent != null) AutoScrollToggledEvent(s, e); };

                m_ClearLogButton = new GUIButton(Clear_Log_Button_Text);
                m_ClearLogButton.Parent = m_ControlBar;
                m_ClearLogButton.Pivot = PivotType.RightMiddle;
                m_ClearLogButton.ButtonClickEvent += (s, e) => { if (ClearLogClickedEvent != null) ClearLogClickedEvent(s, e); };
                
                Resize();
            }

            protected override void Resize()
            {
                m_ControlBarLeftBlock.Position = new Vector2(0f, m_ControlBarLeftBlock.Parent.Size.y * 0.5f);
                m_ControlBarLeftBlock.Size = new Vector2(Left_Block_Width, m_ControlBarLeftBlock.Parent.Size.y);
                m_ShowDebugToggle.Position = new Vector2(0f, m_ShowDebugToggle.Parent.Size.y * 0.5f);
                m_ShowDebugToggle.Size = new Vector2(Show_Log_Toggle_Width, m_ShowDebugToggle.Parent.Size.y);
                m_AutoScrollToggle.Position = new Vector2(m_AutoScrollToggle.Parent.Size.x, m_AutoScrollToggle.Parent.Size.y * 0.5f);
                m_AutoScrollToggle.Size = new Vector2(Auto_Scroll_Toggle_Width, m_AutoScrollToggle.Parent.Size.y);
                m_ClearLogButton.Position = new Vector2(m_ClearLogButton.Parent.Size.x, m_ClearLogButton.Parent.Size.y * 0.5f);
                m_ClearLogButton.Size = new Vector2(Clear_Log_Button_Width, m_ClearLogButton.Parent.Size.y * Clear_Log_Button_Height_Percent);
            }

            protected override void GUIDraw()
            {
                UniqueDraw(() =>
                {
                    m_ControlBar.Draw();
                });
            }
        } 
        #endregion

        #region log panel
        private class LogPanel : GUIBase
        {
            private static readonly float Log_Scroll_View_Right_Hold = 30f;
            private static readonly float Log_Scroll_View_Default_Content_Height = 20f;
            private static readonly float Log_Box_Default_Y_Offset = 10f;
            private static readonly float Log_Box_Left_Right_Margin = 20f;
            private static readonly float Log_Box_Spacing_Distance = 5f;
            private static readonly float Log_Item_Pad_In = 10f;

            private GUIBox m_LogPanel;
            private GUIScrollView m_LogScrollView;
            private Vector2 m_Size;

            public bool AutoScroll { get; set; }
            
            private float m_LogBoxStartY;

            public LogPanel()
            {
                Init();
            }

            private void Init()
            {
                if (m_LogPanel == null)
                {
                    m_LogPanel = new GUIBox();
                    SetBaseControl(m_LogPanel);
                }

                if (m_LogScrollView != null)
                {
                    m_LogScrollView.Parent = null;
                }
                m_LogScrollView = new GUIScrollView();
                m_LogScrollView.Parent = m_LogPanel;
                m_LogScrollView.Pivot = PivotType.LeftTop;
                
                m_LogBoxStartY = Log_Box_Default_Y_Offset;

                Resize();
            }

            protected override void Resize()
            {
                m_LogScrollView.Position = Vector2.zero;
                m_LogScrollView.Size = m_LogScrollView.Parent.Size;
                m_LogScrollView.ContentSize = new Vector2(m_LogScrollView.Size.x - Log_Scroll_View_Right_Hold, Log_Scroll_View_Default_Content_Height);
            }

            protected override void GUIDraw()
            {
                UniqueDraw(() =>
                {
                    m_LogPanel.Draw();
                });
            }

            public void AddLog(LogType type, string logStr)
            {
                LogBox logBox = new LogBox(type, logStr);
                logBox.Parent = m_LogScrollView;
                logBox.Pivot = PivotType.LeftTop;
                logBox.Position = new Vector2(Log_Item_Pad_In, m_LogBoxStartY);
                logBox.Size = new Vector2(m_LogScrollView.Size.x - Log_Box_Left_Right_Margin * 2, 0f);

                float logBoxSizeYWithSpace = logBox.RealSize.y + Log_Box_Spacing_Distance;
                m_LogBoxStartY += logBoxSizeYWithSpace;
                m_LogScrollView.ContentSize = new Vector2(m_LogScrollView.ContentSize.x, m_LogScrollView.ContentSize.y + logBoxSizeYWithSpace);
                if (AutoScroll) m_LogScrollView.ScrollPosition = m_LogScrollView.ContentSize;
            }

            public void ClearLog()
            {
                Init();
            }

            #region log box
            private class LogBox : GUIBase
            {
                private static readonly float Log_Font_Size = 15f;
                private static readonly float Log_Font_Top_Bottom_Margin = 3f;
                private static readonly float Log_Text_Left_Right_Margin = 5f;

                public Vector2 RealSize { get; private set; }
                private float m_Width;
                private float m_Height;
                private string m_Log;

                private GUIButton m_LogBox;
                private GUILabel m_LogLabel;

                public LogBox(LogType type, string log)
                {
                    Init(type, log);
                }

                private void Init(LogType type, string log)
                {
                    m_Log = log;

                    m_LogBox = new GUIButton();
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
                    SetBaseControl(m_LogBox);

                    m_LogLabel = new GUILabel(log);
                    m_LogLabel.Parent = m_LogBox;
                    m_LogLabel.Pivot = PivotType.LeftTop;

                    Resize();
                }

                protected override void Resize()
                {
                    float height = m_Log.Split('\n').Length * Log_Font_Size + Log_Font_Top_Bottom_Margin * 2;
                    RealSize = new Vector2(Size.x, height);

                    m_LogBox.Size = RealSize;
                    m_LogLabel.Position = new Vector2(Log_Text_Left_Right_Margin, 0f);
                    m_LogLabel.Size = new Vector2(m_LogLabel.Parent.Size.x - Log_Text_Left_Right_Margin * 2, m_LogLabel.Parent.Size.y);
                }

                protected override void GUIDraw()
                {
                    UniqueDraw(() =>
                    {
                        m_LogBox.Draw();
                    });
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