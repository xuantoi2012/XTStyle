using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace XTStyle.Controls
{
    /// <summary>
    /// Progress step item
    /// </summary>
    public class ProgressStep
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsCurrent { get; set; }
    }

    /// <summary>
    /// A step-by-step progress tracker control (stepper)
    /// </summary>
    public class ProgressTracker : ItemsControl
    {
        static ProgressTracker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressTracker), new FrameworkPropertyMetadata(typeof(ProgressTracker)));
        }

        public ProgressTracker()
        {
            Steps = new ObservableCollection<ProgressStep>();
            ItemsSource = Steps;
        }

        /// <summary>
        /// Gets the progress steps
        /// </summary>
        public new ObservableCollection<ProgressStep> Steps
        {
            get { return (ObservableCollection<ProgressStep>)GetValue(StepsProperty); }
            private set { SetValue(StepsProperty, value); }
        }

        public new static readonly DependencyProperty StepsProperty =
            DependencyProperty.Register("Steps", typeof(ObservableCollection<ProgressStep>), typeof(ProgressTracker));

        /// <summary>
        /// Gets or sets the current step index (0-based)
        /// </summary>
        public int CurrentStep
        {
            get { return (int)GetValue(CurrentStepProperty); }
            set { SetValue(CurrentStepProperty, value); }
        }

        public static readonly DependencyProperty CurrentStepProperty =
            DependencyProperty.Register("CurrentStep", typeof(int), typeof(ProgressTracker),
                new PropertyMetadata(0, OnCurrentStepChanged));

        /// <summary>
        /// Gets or sets the orientation of the tracker
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(ProgressTracker),
                new PropertyMetadata(Orientation.Horizontal));

        private static void OnCurrentStepChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var tracker = (ProgressTracker)d;
            tracker.UpdateSteps();
        }

        private void UpdateSteps()
        {
            for (int i = 0; i < Steps.Count; i++)
            {
                Steps[i].IsCompleted = i < CurrentStep;
                Steps[i].IsCurrent = i == CurrentStep;
            }
        }
    }
}
