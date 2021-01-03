using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Automation;
using System.Threading;
using System.Collections.ObjectModel;
using SemiAuto.Configurations;
using System.IO;
using SemiAuto.Data;
using SemiAuto.Communicators;
using SemiAuto.CodeGeneration;

namespace SemiAuto
{
    public partial class FormMain : Form
    {
        public Configuration Configuration { get; set; }

        private BindingSource m_Binder = new BindingSource();
        private ObservableCollection<Activity> m_Activities = new ObservableCollection<Activity>();
        private AutomationElement m_MainWindow = null;

        private MasterServiceHandler m_TestService;
        private TestClient m_LocalClient;

        private System.Windows.Forms.Timer m_AutoIdRefreshTimer;
        private int m_TimerTicksLeft = 10;

        public FormMain()
        {
            InitializeComponent();
            this.m_Binder.DataSource = this.m_Activities;
            this.dgvActivities.DataSource = this.m_Binder;
            this.m_Activities.CollectionChanged += M_Activities_CollectionChanged;

            if(!File.Exists(Configuration.DEFAULT_FILE))
            {
                new Configuration().Save(Configuration.DEFAULT_FILE);
            }
            this.Configuration = Configuration.Load(Configuration.DEFAULT_FILE);
        }

        private void OnPropertyChanged(object src, AutomationPropertyChangedEventArgs e)
        {
            AutomationElement el = src as AutomationElement;

            string itemType = el.Current.ClassName;
            string id = el.Current.AutomationId;
            string text = this.GetText(el);
            string name = el.Current.Name;

            if (e.Property == ValuePattern.ValueProperty)
            {
                Activity act = new Activity()
                {
                    Type = Activity.Types.ValueChange,
                    ControlDisplayText = name,
                    ControlType = itemType,
                    WPath = this.GetWPath(this.GetAncestorWalk(el)),
                    Value = e.NewValue
                };
                this.m_Activities.Add(act);
            }
            else if(e.Property == TogglePattern.ToggleStateProperty)
            {
                Activity act = new Activity()
                {
                    Type = Activity.Types.Toggle,
                    ControlDisplayText = text,
                    ControlType = itemType,
                    WPath = this.GetWPath(this.GetAncestorWalk(el)),
                    Value = e.NewValue
                };
                this.m_Activities.Add(act);
            }
            else if(e.Property == SelectionItemPattern.IsSelectedProperty)
            {
                object pattern = null;
                el.TryGetCurrentPattern(SelectionItemPattern.Pattern, out pattern);

                SelectionItemPattern typedPattern = pattern as SelectionItemPattern;
                bool selected = typedPattern.Current.IsSelected;
                AutomationElement container = typedPattern.Current.SelectionContainer;

                Activity act = new Activity()
                {
                    Type = Activity.Types.SelectionChange,
                    ControlDisplayText = this.GetText(container),
                    ControlType = itemType,
                    WPath = this.GetWPath(this.GetAncestorWalk(el)),
                    Value = e.NewValue
                };

            }
        }

        private void OnEvent(object src, AutomationEventArgs e)
        {
            AutomationElement el = src as AutomationElement;

            string itemType = el.Current.ClassName;
            string id  = el.Current.AutomationId;
            string text = this.GetText((src as AutomationElement));

            if (e.EventId == InvokePattern.InvokedEvent)
            {
                Activity act = new Activity()
                {
                    Type = Activity.Types.Click,
                    ControlDisplayText = text,
                    ControlType = itemType,
                    WPath = this.GetWPath(this.GetAncestorWalk(el))
                };
                this.m_Activities.Add(act);
            }
            else if(e.EventId == SelectionPattern.InvalidatedEvent)
            {
                Activity act = new Activity()
                {
                    Type = Activity.Types.SelectionChange,
                    ControlDisplayText = text,
                    ControlType = itemType,
                    WPath = this.GetWPath(this.GetAncestorWalk(el))
                };
                this.m_Activities.Add(act);
            }
        }

        private void OnLoad(object sender, EventArgs e)
        {
            this.cbxWindow.DataSource = WindowHandler.GetOpenWindows().ToList();
            this.cbxWindow.ValueMember = "Value";
            this.cbxWindow.DisplayMember = "Value";

            this.m_TestService = new MasterServiceHandler();
            this.m_TestService.Start();

            this.m_LocalClient = new TestClient();
            this.m_LocalClient.Init();

            this.m_AutoIdRefreshTimer = new System.Windows.Forms.Timer()
            {
                Enabled = true,
                Interval = 1000,
            };
            this.m_AutoIdRefreshTimer.Tick += M_AutoIdRefreshTimer_Tick;
        }

        #region Helpers methods

        private List<AutomationElement> GetAncestorWalk(AutomationElement element)
        {
            List<AutomationElement> tree = new List<AutomationElement>();
            tree.Add(element);

            TreeWalker walker = TreeWalker.RawViewWalker;
            element = tree.First();
            while (true)
            {
                element = walker.GetParent(element);

                if (element != null)
                {
                    tree.Add(element);
                }

                if (element == this.m_MainWindow)
                {
                    break;
                }
            }

            return tree;
        }

        private string GetComplexAutomationId(List<AutomationElement> walk)
        {
            walk.Reverse();
            string auto = String.Join(".", walk.Select(ae => ae.Current.AutomationId).ToArray());

            return auto;
        }

        private string GetWPath(List<AutomationElement> walk)
        {
            walk.Reverse();
            string auto = String.Join(".", walk.Select(ae => ae.Current.AutomationId).ToArray());

            return auto;
        }

        public string GetText(AutomationElement element)
        {
            object patternObj;
            if (element.TryGetCurrentPattern(ValuePattern.Pattern, out patternObj))
            {
                var valuePattern = (ValuePattern)patternObj;
                return valuePattern.Current.Value;
            }
            else if (element.TryGetCurrentPattern(TextPattern.Pattern, out patternObj))
            {
                var textPattern = (TextPattern)patternObj;
                return textPattern.DocumentRange.GetText(-1).TrimEnd('\r'); // often there is an extra '\r' hanging off the end.
            }
            else
            {
                return element.Current.Name;
            }
        }

        public void AttachToWindow(string windowName)
        {
            try
            {
                AutomationElement aeDesktop = null;
                aeDesktop = AutomationElement.RootElement;
                if (aeDesktop == null)
                {
                    throw new Exception("Unable to get Desktop");
                }

                this.m_MainWindow = null;
                int numWaits = 0;
                do
                {
                    this.m_MainWindow = aeDesktop.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, windowName));
                    numWaits++;
                    Thread.Sleep(200);
                }
                while (this.m_MainWindow == null && numWaits < 50);

                if (this.m_MainWindow == null)
                {
                    throw new Exception("Failed to find window");
                }


                AutomationProperty[] captureProperties = new AutomationProperty[]
                {
                    ValuePattern.ValueProperty,
                    TogglePattern.ToggleStateProperty,
                    SelectionPattern.SelectionProperty,
                    SelectionItemPattern.IsSelectedProperty,
                };

                Automation.AddAutomationPropertyChangedEventHandler(this.m_MainWindow, TreeScope.Subtree, this.OnPropertyChanged, captureProperties);
                Automation.AddAutomationEventHandler(SelectionPattern.InvalidatedEvent, this.m_MainWindow, TreeScope.Subtree, this.OnEvent);
                Automation.AddAutomationEventHandler(InvokePattern.InvokedEvent, this.m_MainWindow, TreeScope.Subtree, this.OnEvent);
                Automation.AddAutomationEventHandler(SelectionItemPattern.ElementSelectedEvent, this.m_MainWindow, TreeScope.Subtree, this.OnEvent);

                this.Text += $" (now attached to: ({windowName})";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Event handlers

        private void M_Activities_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.Invoke(new Action(() => this.m_Binder.ResetBindings(false)));
        }

        private void btnAttach_Click(object sender, EventArgs e)
        {
            this.AttachToWindow(this.cbxWindow.SelectedValue.ToString());
        }

        private void M_AutoIdRefreshTimer_Tick(object sender, EventArgs e)
        {
            this.btnGenerateAutoIds.Text = $"Generate ({this.m_TimerTicksLeft})";
            this.m_TimerTicksLeft--;

            if (this.m_TimerTicksLeft == 0)
            {
                this.m_LocalClient.RequestGenerateIds();
                this.m_TimerTicksLeft = 10;
            }
        }

        private void btnAddComment_Click(object sender, EventArgs e)
        {
            this.m_Activities.Add(new Activity()
            {
                Type = Activity.Types.Comment,
                Value = this.tbxCommentText.Text
            });
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.cbxWindow.DataSource = WindowHandler.GetOpenWindows().ToList();
        }

        private void btnGenerateAutoIds_Click(object sender, EventArgs e)
        {
            this.m_LocalClient.RequestGenerateIds();
            this.m_TimerTicksLeft = 10;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettings dlg = new FormSettings();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                dlg.EditedConfiguration.Save(Configuration.DEFAULT_FILE);
                this.Configuration = dlg.EditedConfiguration;
            }
        }

        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IGenerator gen = null;
            if(this.Configuration.Generator.Contains("C#")) //fucking stupid but works for now
            {
                gen = new SemiTestGenerator();
            }
            else
            {
                gen = new ReadableGenerator();
            }

            string result = gen.Generate(this.m_Activities.ToList());
            new FormOutput(result).ShowDialog();
        }

        private void createMacroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(this.dgvActivities.SelectedRows.Count > 0)
            {
                List<Activity> selection = new List<Activity>();
                this.dgvActivities.SelectedRows.Cast<DataGridViewRow>().ToList().ForEach((i) => selection.Add(i.DataBoundItem as Activity));


                FormAddMacro dlg = new FormAddMacro(selection);
                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    this.Configuration.Macros.Add(dlg.EditedItem);
                    this.Configuration.Save(Configuration.DEFAULT_FILE);
                }
            }
        }

        #endregion
    }
}
