using System;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// Die Elementvorlage "Benutzersteuerelement" wird unter https://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace StdOttUwp.Controls
{
    public sealed partial class ColorPicker : UserControl
    {
        public static readonly DependencyProperty RgbSpectrumHeightProperty =
            DependencyProperty.Register("RgbSpectrumHeight", typeof(GridLength), typeof(ColorPicker),
                new PropertyMetadata(new GridLength(1, GridUnitType.Star)));

        public static readonly DependencyProperty BlackWhiteHeightProperty =
            DependencyProperty.Register("BlackWhiteHeight", typeof(GridLength), typeof(ColorPicker),
                new PropertyMetadata(new GridLength(0.1, GridUnitType.Star)));

        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof(Color), typeof(ColorPicker),
                new PropertyMetadata(Colors.Black, OnSelectedColorPropertyChanged));

        private static void OnSelectedColorPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ColorPicker s = (ColorPicker)sender;
            Color value = (Color)e.NewValue;

            s.rectCurrentColor.Fill = new SolidColorBrush(value);

            UpdateValue(ref s.isUpdatingR, () => s.tbxR.Text = value.R.ToString());
            UpdateValue(ref s.isUpdatingG, () => s.tbxG.Text = value.G.ToString());
            UpdateValue(ref s.isUpdatingB, () => s.tbxB.Text = value.B.ToString());
        }

        public static readonly DependencyProperty ViewRgbInputProperty =
            DependencyProperty.Register("ViewRgbInput", typeof(bool), typeof(ColorPicker),
                new PropertyMetadata(true));


        private bool isUpdatingR, isUpdatingG, isUpdatingB;
        private int pointersPressedCount;

        public GridLength RgbSpectrumHeight
        {
            get => (GridLength)GetValue(RgbSpectrumHeightProperty);
            set => SetValue(RgbSpectrumHeightProperty, value);
        }

        public GridLength BlackWhiteHeight
        {
            get => (GridLength)GetValue(BlackWhiteHeightProperty);
            set => SetValue(BlackWhiteHeightProperty, value);
        }

        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        public bool ViewRgbInput
        {
            get => (bool)GetValue(ViewRgbInputProperty);
            set => SetValue(ViewRgbInputProperty, value);
        }

        public ColorPicker()
        {
            this.InitializeComponent();
        }

        private void SetCurrentColor(Point pos)
        {
            if (pos.X < 0 || pos.Y < 0 || pos.X > gidColors.ActualWidth || pos.Y > gidColors.ActualHeight) return;

            Thickness margin = gidPointer.Margin;
            margin.Left = pos.X;
            margin.Top = pos.Y;
            gidPointer.Margin = margin;

            gidPointer.Visibility = Visibility.Visible;

            SelectedColor = GetColor(pos.Y / gidColors.ActualHeight, pos.X / gidColors.ActualWidth);
        }

        private Color GetColor(double value, double brightness)
        {
            byte r, g, b;

            byte raise, fall;

            switch (GetArea(value, out raise, out fall))
            {
                case 0:
                    r = byte.MaxValue;
                    g = raise;
                    b = byte.MinValue;
                    break;

                case 1:
                    r = fall;
                    g = byte.MaxValue;
                    b = byte.MinValue;
                    break;

                case 2:
                    r = byte.MinValue;
                    g = byte.MaxValue;
                    b = raise;
                    break;

                case 3:
                    r = byte.MinValue;
                    g = fall;
                    b = byte.MaxValue;
                    break;

                case 4:
                    r = raise;
                    g = byte.MinValue;
                    b = byte.MaxValue;
                    break;

                case 5:
                    r = byte.MaxValue;
                    g = byte.MinValue;
                    b = fall;
                    break;

                default:
                    r = 127;
                    g = 127;
                    b = 127;
                    break;
            }

            if (brightness < 0.5)
            {
                brightness *= 2;

                r = (byte)(r * brightness);
                g = (byte)(g * brightness);
                b = (byte)(b * brightness);
            }
            else if (brightness > 0.5)
            {
                brightness = (1 - brightness) * 2;

                r = (byte)(byte.MaxValue - (byte.MaxValue - r) * brightness);
                g = (byte)(byte.MaxValue - (byte.MaxValue - g) * brightness);
                b = (byte)(byte.MaxValue - (byte.MaxValue - b) * brightness);
            }

            return Color.FromArgb(byte.MaxValue, r, g, b);
        }

        private int GetArea(double value, out byte startToEnd, out byte endToStart)
        {
            double rgbSpectrumHeight = gidColors.RowDefinitions[0].ActualHeight;
            double blackWhiteHeight = gidColors.RowDefinitions[1].ActualHeight;
            double colorArea = rgbSpectrumHeight / (rgbSpectrumHeight + blackWhiteHeight);

            if (value > colorArea)
            {
                startToEnd = 0;
                endToStart = byte.MaxValue;

                return -1;
            }

            double partSize = colorArea / 6.0;
            int area = (int)Math.Floor(value / partSize);

            startToEnd = (byte)((value - area * partSize) / partSize * byte.MaxValue);
            endToStart = (byte)(((area + 1) * partSize - value) / partSize * byte.MaxValue);

            return area;
        }

        private void GidColors_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (pointersPressedCount > 0) SetCurrentColor(e.GetCurrentPoint(gidColors).Position);
        }

        private void GidColors_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            pointersPressedCount++;

            SetCurrentColor(e.GetCurrentPoint(gidColors).Position);
        }

        private void GidColors_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            pointersPressedCount--;
        }

        private void GidColors_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SetCurrentColor(e.GetPosition(gidColors));
        }

        private void TbxR_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateValue(ref isUpdatingR, () =>
            {
                byte value;

                if (!byte.TryParse(tbxR.Text, out value)) return;

                Color newColor = SelectedColor;
                newColor.R = value;

                SelectedColor = newColor;
            });
        }

        private void TbxG_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateValue(ref isUpdatingG, () =>
            {
                byte value;

                if (!byte.TryParse(tbxG.Text, out value)) return;

                Color newColor = SelectedColor;
                newColor.G = value;

                SelectedColor = newColor;
            });
        }

        private void TbxB_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateValue(ref isUpdatingB, () =>
            {
                byte value;

                if (!byte.TryParse(tbxB.Text, out value)) return;

                Color newColor = SelectedColor;
                newColor.B = value;

                SelectedColor = newColor;
            });
        }

        private static void UpdateValue(ref bool isUpdating, Action seter)
        {
            if (isUpdating) return;

            isUpdating = true;
            seter();
            isUpdating = false;
        }
    }
}
