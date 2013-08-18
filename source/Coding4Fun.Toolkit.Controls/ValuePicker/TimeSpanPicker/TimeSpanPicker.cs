﻿using System;
using System.Globalization;
using System.Windows;

using Coding4Fun.Toolkit.Controls.Primitives;

namespace Coding4Fun.Toolkit.Controls
{
    public class TimeSpanPicker : ValuePickerBase<TimeSpan>
    {
        /// <summary>
        /// Initializes a new instance of the TimePicker control.
        /// </summary>
        public TimeSpanPicker()
        {
            DefaultStyleKey = typeof(TimeSpanPicker);

            Value = TimeSpan.FromMinutes(30);

			DialogTitle = ValuePickerResources.TimeSpanPickerTitle;
        }

        protected internal override void UpdateValueString()
        {
            if (Value.HasValue)
            {
                var ts = Value.Value;

                if (!string.IsNullOrEmpty(ValueStringFormat))
                {
                    ValueString = Common.TimeSpanFormat.Format(ts, ValueStringFormat);
                    return;
                }
            }

            ValueString = string.Format(CultureInfo.CurrentCulture, ValueStringFormat ?? ValueStringFormatFallback, Value);
        }

        /// <summary>
        /// Gets or sets the Max Value
        /// </summary>
        public TimeSpan Max
        {
            get { return (TimeSpan)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }

        /// <summary>
        /// Identifies the Max Property
        /// </summary>
        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(
            "Max", typeof(TimeSpan), typeof(ValuePickerBase<TimeSpan>), new PropertyMetadata(TimeSpan.FromHours(24), OnLimitsChanged));

        /// <summary>
        /// Gets or sets the Max Value
        /// </summary>
        public TimeSpan Min
        {
            get { return (TimeSpan)GetValue(MinProperty); }
            set { SetValue(MinProperty, value); }
        }

        /// <summary>
        /// Identifies the Max Property
        /// </summary>
        public static readonly DependencyProperty MinProperty = DependencyProperty.Register(
            "Min", typeof(TimeSpan), typeof(ValuePickerBase<TimeSpan>), new PropertyMetadata(TimeSpan.Zero, OnLimitsChanged));

        private static void OnLimitsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var picker = sender as TimeSpanPicker;

            if (picker != null)
            {
                picker.ValidateBounds();
            }
        }

        private void ValidateBounds()
        {
            if (Min < TimeSpan.Zero)
            {
                Min = TimeSpan.Zero;
            }

            if (Max > TimeSpan.MaxValue)
            {
                Max = TimeSpan.MaxValue;
            }

            if (Max < Min)
            {
                Max = Min;
            }

            if (Value.HasValue)
            {
                Value = TimeSpanExtensions.CheckBound(Value.Value, Min, Max);
            }
            else
            {
                Value = Min;
            }
        }

        /// <summary>
        /// Gets or sets the Value Step
        /// </summary>
        public TimeSpan Step
        {
            get { return (TimeSpan)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }

        /// <summary>
        /// Identifies the Max Property
        /// </summary>
        public static readonly DependencyProperty StepProperty = DependencyProperty.Register(
            "Step", typeof(TimeSpan), typeof(ValuePickerBase<TimeSpan>), new PropertyMetadata(TimeSpan.FromSeconds(1)));

        /// <summary>
        /// Initializes Value, Max, Step when vanigating to the new page
        /// </summary>
        /// <param name="page"></param>
        protected override void NavigateToNewPage(object page)
        {
            var tsPage = page as ITimeSpanPickerPage<TimeSpan>;

            if (tsPage != null)
            {
				tsPage.Minimum = Min;
				tsPage.Maximum = Max;
				tsPage.StepFrequency = Step;
            }

            base.NavigateToNewPage(page);
        }


    }
}
