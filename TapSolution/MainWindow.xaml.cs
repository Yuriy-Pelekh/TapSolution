using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TapSolution
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    public void T(double to)
    {
      // Create a duration of 2 seconds. 
      var duration = new Duration(TimeSpan.FromSeconds(5));

      // Create two DoubleAnimations and set their properties. 
      var myDoubleAnimationBottom = new DoubleAnimationUsingKeyFrames {Duration = duration};
      myDoubleAnimationBottom.KeyFrames.Add(new LinearDoubleKeyFrame {KeyTime = KeyTime.FromPercent(0.25), Value = to/3});
      myDoubleAnimationBottom.KeyFrames.Add(new LinearDoubleKeyFrame {KeyTime = KeyTime.FromPercent(1.0), Value = to});

      var myDoubleAnimationTop = new DoubleAnimationUsingKeyFrames { Duration = duration };
      myDoubleAnimationTop.KeyFrames.Add(new LinearDoubleKeyFrame { KeyTime = KeyTime.FromPercent(0.4), Value = to/5 });
      myDoubleAnimationTop.KeyFrames.Add(new LinearDoubleKeyFrame { KeyTime = KeyTime.FromPercent(0.8), Value = to/2 });
      myDoubleAnimationTop.KeyFrames.Add(new LinearDoubleKeyFrame { KeyTime = KeyTime.FromPercent(1.0), Value = to });

      var sb = new Storyboard { Duration = duration };

      sb.Children.Add(myDoubleAnimationBottom);
      sb.Children.Add(myDoubleAnimationTop);

      Storyboard.SetTarget(myDoubleAnimationBottom, line);
      Storyboard.SetTarget(myDoubleAnimationTop, line);

      // Set the attached properties of Canvas.Left and Canvas.Top 
      // to be the target properties of the two respective DoubleAnimations 
      Storyboard.SetTargetProperty(myDoubleAnimationTop, new PropertyPath("(X1)"));  // <--- "Best correlation has illegal arguments" 
      Storyboard.SetTargetProperty(myDoubleAnimationBottom, new PropertyPath("(X2)")); // <--- These two lines don't work: 

      sb.Begin();
    }

    private void ButtonRunClick(object sender, RoutedEventArgs e)
    {

      var maxWidth = canvas.ActualWidth - 10;
      T(maxWidth);
    }
  }
}
