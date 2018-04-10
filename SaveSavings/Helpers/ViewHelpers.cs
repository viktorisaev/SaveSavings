using Windows.UI.Xaml;

namespace SaveSavings.Helpers
{
    internal class ViewHelpers
    {

        public static Visibility GetVisibility(bool _IsVisible)
        {
            return _IsVisible ? Visibility.Visible : Visibility.Collapsed;
        }

    }
}
