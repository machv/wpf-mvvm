﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Mach.Wpf.Mvvm
{
    public class ControlItemDoubleClick : DependencyObject
    {
        public ControlItemDoubleClick()
        {

        }

        public static readonly DependencyProperty ItemsDoubleClickProperty =
            DependencyProperty.RegisterAttached("ItemsDoubleClick",
            typeof(bool), typeof(Binding));

        public static void SetItemsDoubleClick(ItemsControl element, bool value)
        {
            element.SetValue(ItemsDoubleClickProperty, value);

            if (value)
            {
                element.PreviewMouseDoubleClick += new MouseButtonEventHandler(element_PreviewMouseDoubleClick);
            }
        }

        static void element_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ItemsControl control = sender as ItemsControl;

            foreach (InputBinding b in control.InputBindings)
            {
                if (!(b is MouseBinding))
                {
                    continue;
                }

                if (b.Gesture != null
                    && b.Gesture is MouseGesture
                    && ((MouseGesture)b.Gesture).MouseAction == MouseAction.LeftDoubleClick
                    && b.Command.CanExecute(b.CommandParameter))
                {
                    b.Command.Execute(b.CommandParameter);
                    e.Handled = true;
                }
            }
        }

        public static bool GetItemsDoubleClick(ItemsControl element)
        {
            return (bool)element.GetValue(ItemsDoubleClickProperty);
        }
    }
}
