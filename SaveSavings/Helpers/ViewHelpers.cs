using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace SaveSavings.Helpers
{
    internal class ViewHelpers
    {

        public static Visibility GetVisibility(bool _IsVisible)
        {
            return _IsVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        public static Brush GetColorByAmount(int amount)
        {
            // TODO: create and reference single 
            return amount < 0 ? ViewHelpers.NegativeAmountBrush : ViewHelpers.PositiveAmountBrush;
        }

        private static Brush PositiveAmountBrush = new SolidColorBrush(Colors.Green);
        private static Brush NegativeAmountBrush = new SolidColorBrush(Colors.Red);

    }
}
