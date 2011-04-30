﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Coding4Fun.Phone.Controls
{
    public abstract class ColorControl : Control
    {
        public delegate void ColorChangedHandler(object sender, Color color);
        public event ColorChangedHandler ColorChanged;

        #region dependency properties
        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Color), typeof(ColorControl), new PropertyMetadata(null));

        public SolidColorBrush SolidColorBrush
        {
            get { return (SolidColorBrush)GetValue(SolidColorBrushProperty); }
            private set { SetValue(SolidColorBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SolidColorBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SolidColorBrushProperty =
            DependencyProperty.Register("SolidColorBrush", typeof(SolidColorBrush), typeof(ColorControl), new PropertyMetadata(null));
        #endregion

        protected internal void ColorChanging(Color color)
        {
            Color = color;
            SolidColorBrush = new SolidColorBrush(Color);

            if (ColorChanged != null)
                ColorChanged(this, Color);
        }
    }
}
