﻿//-----------------------------------------------------------------------
// <copyright file="ReadOnlyControl.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Genesys.Extensions;
using Genesys.Extras.Collections;
using Genesys.Framework.Application;
using Genesys.Framework.Pages;
using Genesys.Framework.Validation;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;

namespace Genesys.Framework.UserControls
{
    /// <summary>
    /// Common UI functions
    /// </summary>
    public abstract class ReadOnlyControl : UserControl
    {
        /// <summary>
        /// Currently running application
        /// </summary>
        public WpfApplication MyApplication { get { return (WpfApplication)System.Windows.Application.Current; } }

        /// <summary>
        /// Holds model data
        /// </summary>
        protected object MyViewModel { get; set; } = null;

        /// <summary>
        /// Uri to currently active frame/page
        /// </summary>
        public Uri CurrentPage { get { return MyApplication.CurrentPage; } }

        /// <summary>
        /// Throws Exception if any UI elements overrun their text max length
        /// </summary>
        public bool ThrowExceptionOnTextOverrun { get; set; } = TypeExtension.DefaultBoolean;

        /// <summary>
        /// Constructor
        /// </summary>
        public ReadOnlyControl() : base()
        {
#if (DEBUG)
            ThrowExceptionOnTextOverrun = true;
#endif
            Loaded += Partial_Loaded;
            SizeChanged += Partial_SizeChanged;
        }

        /// <summary>
        /// Page_Load event handler
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event arguments</param>
        protected virtual void Partial_Loaded(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Page_SizeChanged event handler
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event arguments</param>
        protected void Partial_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        /// <summary>
        /// CanGoBack if backward path exists, not at root or invalid path
        /// </summary>
        protected bool CanGoBack { get { return MyApplication.RootFrame.CanGoBack; } }

        /// <summary>
        /// Navigates back one screen
        /// </summary>
        public virtual void GoBack() { MyApplication.Navigate(MyApplication.HomePage); }

        /// <summary>
        /// Navigates to any screen
        /// </summary>
        /// <param name="destinationUri">Destination Uri, absolute or relative</param>
        /// <param name="dataToPass">Screen data</param>
        public virtual void Navigate(string destinationUri, object dataToPass = null)
        {
            Navigate(new Uri(destinationUri, UriKind.RelativeOrAbsolute), dataToPass);
        }

        /// <summary>
        /// Navigates to any screen
        /// </summary>
        /// <param name="destinationUri">Destination Uri, absolute or relative</param>
        /// <param name="dataToPass">Screen data</param>
        public virtual void Navigate(Uri destinationUri, object dataToPass = null)
        {
            var navService = NavigationService.GetNavigationService(this);
            var newComponent = System.Windows.Application.LoadComponent(destinationUri);

            if (newComponent is ReadOnlyPage)
            {
                navService.LoadCompleted += new LoadCompletedEventHandler(((ReadOnlyPage)newComponent).NavigationService_LoadCompleted);
            }
            navService.Navigate(((Page)newComponent), dataToPass);
        }

        /// <summary>
        /// Binds all model data to the screen controls
        /// </summary>
        /// <param name="modelData">Data to bind to controls</param>
        protected abstract void BindModelData(object modelData);

        /// <summary>
        /// Binds a string to a Image
        /// </summary>
        /// <param name="item">Item to bind</param>
        /// <param name="bindingProperty">String name of property holding the data</param>
        public void SetBinding(ref Image item, string bindingProperty)
        {
            item.SetBinding(Image.SourceProperty, new Binding() { Path = new PropertyPath(bindingProperty), Mode = BindingMode.OneWay });
        }


        /// <summary>
        /// Binds a string to a TextBlock
        /// </summary>
        /// <param name="item">Item to bind</param>
        /// <param name="initialValue">Initial value or selection</param>
        /// <param name="bindingProperty">String name of property holding the data</param>
        public void SetBinding(ref TextBlock item, string initialValue, string bindingProperty)
        {
            item.SetBinding(TextBlock.TextProperty, new Binding() { Path = new PropertyPath(bindingProperty), Mode = BindingMode.OneWay });
        }

        /// <summary>
        /// Binds a string to a TextBox
        /// </summary>
        /// <param name="item">Item to bind</param>
        /// <param name="initialValue">Initial value or selection</param>
        /// <param name="bindingProperty">String name of property holding the data</param>
        public void SetBinding(ref TextBox item, string initialValue, string bindingProperty)
        {
            item.SetBinding(TextBox.TextProperty, new Binding() { Path = new PropertyPath(bindingProperty), Mode = BindingMode.TwoWay });
        }

        /// <summary>
        /// Binds a string to a TextBox
        /// </summary>
        /// <param name="item">Item to bind</param>
        /// <param name="initialValue">Initial value or selection</param>
        /// <param name="bindingProperty">String name of property holding the data</param>
        public void SetBinding(ref TextBox item, DateTime initialValue, string bindingProperty)
        {
            item.SetBinding(TextBox.TextProperty, new Binding() { Path = new PropertyPath(bindingProperty), Mode = BindingMode.TwoWay });
        }

        /// <summary>
        /// Binds a string to a PasswordBox
        /// </summary>
        /// <param name="item">Item to bind</param>
        /// <param name="initialValue">Initial value or selection</param>
        /// <param name="bindingProperty">String name of property holding the data</param>
        public void SetBinding(ref PasswordBox item, string initialValue, string bindingProperty)
        {
            item.SetBinding(PasswordBox.PasswordCharProperty, new Binding() { Path = new PropertyPath(bindingProperty), Mode = BindingMode.TwoWay });
        }

        /// <summary>
        /// Binds a string to a DatePicker
        /// </summary>
        /// <param name="item">Item to bind</param>
        /// <param name="initialValue">Initial value or selection</param>
        /// <param name="bindingProperty">String name of property holding the data</param>
        public void SetBinding(ref DatePicker item, DateTime initialValue, string bindingProperty)
        {
            item.SetBinding(DatePicker.TextProperty, new Binding() { Path = new PropertyPath(bindingProperty), Mode = BindingMode.TwoWay });
        }

        /// <summary>
        /// Binds a standard key-value pair to a ComboBox
        /// </summary>
        /// <param name="item">Item to bind</param>
        /// <param name="collection">List of elements to bind</param>
        /// <param name="selectedKey">Default item to select, by key</param>
        /// <param name="bindingProperty">String name of property holding the data</param>
        public void SetBinding(ref ComboBox item, List<KeyValuePair<int, string>> collection, int selectedKey, string bindingProperty)
        {
            item.ItemsSource = collection;
            item.DisplayMemberPath = "Value";
            item.SelectedValuePath = "Key";
            item.SetBinding(ComboBox.SelectedValueProperty, new Binding() { Path = new PropertyPath(bindingProperty), Mode = BindingMode.TwoWay });
            // Handle for no state
            if (collection.Count == 1)
            {
                selectedKey = TypeExtension.DefaultInteger;
            }
            item.SelectedValue = selectedKey;
        }

        /// <summary>
        /// Binds a standard key-value pair to a ComboBox
        /// </summary>
        /// <param name="item">Item to bind</param>
        /// <param name="collection">List of elements to bind</param>
        /// <param name="selectedKey">Default item to select, by key</param>
        /// <param name="bindingProperty">String name of property holding the data</param>
        public void SetBinding(ref ComboBox item, List<KeyValuePair<Guid, string>> collection, int selectedKey, string bindingProperty)
        {
            item.ItemsSource = collection;
            item.DisplayMemberPath = "Value";
            item.SelectedValuePath = "Key";
            item.SetBinding(ComboBox.SelectedValueProperty, new Binding() { Path = new PropertyPath(bindingProperty), Mode = BindingMode.TwoWay });
            // Handle for no state
            if (collection.Count == 1)
            {
                selectedKey = TypeExtension.DefaultInteger;
            }
            item.SelectedValue = selectedKey;
        }

        /// <summary>
        /// Binds a standard key-value pair to a ComboBox
        /// </summary>
        /// <param name="item">Item to bind</param>
        /// <param name="collection">List of elements to bind</param>
        /// <param name="selectedKey">Default item to select, by key</param>
        /// <param name="bindingProperty">String name of property holding the data</param>
        public void SetBinding(ref ComboBox item, KeyValueListString collection, string selectedKey, string bindingProperty)
        {
            item.ItemsSource = collection;
            item.DisplayMemberPath = "Value";
            item.SelectedValuePath = "Key";
            item.SetBinding(ComboBox.SelectedValueProperty, new Binding() { Path = new PropertyPath(bindingProperty), Mode = BindingMode.TwoWay });
            // Handle for no state
            if (collection.Count == 1) { selectedKey = collection.FirstOrDefaultSafe().Key; }
            item.SelectedValue = selectedKey;
        }

        /// <summary>
        /// Validates text length, etc.
        /// </summary>
        public abstract bool Validate();

        /// <summary>
        /// Validates Text Message
        /// </summary>
        /// <param name="uiControl">Control holding original text</param>
        /// <param name="textMessage">Text to validate length</param>
        public bool ValidateTextLength(Control uiControl, string textMessage)
        {
            Validator<Control> controlValidator = new Validator<Control>();
            TextBlock testControl = new TextBlock() { Text = textMessage };
            controlValidator.BusinessRules.Add(new ValidationRule<Control>("ActualWidth", item => item.ActualHeight <= testControl.ActualHeight));
            return controlValidator.IsValid(uiControl);
        }

        /// <summary>
        /// Workflow beginning. No custom to return.
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event arguments</param>
        public delegate void SendBeginEventHandler(object sender, EventArgs e);
    }
}