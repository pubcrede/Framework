﻿//-----------------------------------------------------------------------
// <copyright file="OkCancel.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using Genesys.Framework.Worker;

namespace Genesys.Framework.UserControls
{
    /// <summary>
    /// OK and cancel buttons
    /// </summary>
    public sealed partial class OkCancel : ReadOnlyControl
    {
        /// <summary>
        /// OnOKClickedEventHandler
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event arguments</param>
        public delegate void OnOKClickedEventHandler(object sender, RoutedEventArgs e);

        /// <summary>
        /// OnOKClickedEventHandler
        /// </summary>
        public event OnOKClickedEventHandler OnOKClicked;

        /// <summary>
        /// OnCancelClickedEventHandler
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event arguments</param>
        public delegate void OnCancelClickedEventHandler(object sender, RoutedEventArgs e);

        /// <summary>
        /// OnCancelClickedEventHandler
        /// </summary>
        public event OnCancelClickedEventHandler OnCancelClicked;

        /// <summary>
        /// Shows/hides the map
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        public Visibility VisibilityButtons
        {
            get
            {
                return ButtonOK.Visibility;
            }
            set
            {
                ButtonOKControl.Visibility = value;
                ButtonCancelControl.Visibility = value;
                // Only show progress if processing
                if (value == System.Windows.Visibility.Collapsed)
                {
                    ProgressProcessing.Visibility = value;
                }
            }
        }

        /// <summary>
        /// HorizontalAlignment
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        public new HorizontalAlignment HorizontalAlignment
        {
            get
            {
                return StackButtons.HorizontalAlignment;
            }
            set
            {
                StackButtons.HorizontalAlignment = value;
            }
        }

        /// <summary>
        /// Orientation
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        public Orientation Orientation
        {
            get
            {
                return StackButtons.Orientation;
            }
            set
            {
                StackButtons.Orientation = value;
            }
        }

        /// <summary>
        /// Progress text
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        public string TextProcessingMessage
        {
            get
            {
                return ProgressProcessing.TextProcessingMessage;
            }
            set
            {
                ProgressProcessing.TextProcessingMessage = value;
            }
        }

        /// <summary>
        /// Progress text
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        public string TextResultMessage
        {
            get
            {
                return ProgressProcessing.TextResultMessage;
            }
            set
            {
                ProgressProcessing.TextResultMessage = value;
            }
        }

        /// <summary>
        /// OK Button
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        public Button ButtonOK
        {
            get
            {
                return ButtonOKControl;
            }
            set
            {
                ButtonOKControl = value;
            }
        }

        /// <summary>
        /// OK Button content
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        public object ButtonOKContent
        {
            get
            {
                return ButtonOKControl.Content;
            }
            set
            {
                ButtonOKControl.Content = value;
            }
        }

        /// <summary>
        /// Cancel Button
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        public Button ButtonCancel
        {
            get
            {
                return ButtonCancelControl;
            }
            set
            {
                ButtonCancelControl = value;
            }
        }

        /// <summary>
        /// OK Button content
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        public object ButtonCancelContent
        {
            get
            {
                return ButtonCancelControl.Content;
            }
            set
            {
                ButtonCancelControl.Content = value;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public OkCancel()
        {
            InitializeComponent();
            ButtonOKControl.Click += OK_Click;
            ButtonCancelControl.Click += Cancel_Click;
        }

        /// <summary>
        /// Binds controls to the data 
        /// </summary>
        /// <param name="modelData"></param>
        protected override void BindModelData(object modelData)
        {
        }

        /// <summary>
        /// Partial and controls have been loaded
        /// </summary>
        /// <param name="sender">Sender of this event call</param>
        /// <param name="e">Event arguments</param>
        protected override void Partial_Loaded(object sender, EventArgs e)
        {
            base.Partial_Loaded(sender, e);
        }

        /// <summary>
        /// Ok button
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event arguments</param>
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            OnOKClicked?.Invoke(sender, e);
        }

        /// <summary>
        /// Cancel Button
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event arguments</param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            OnCancelClicked?.Invoke(sender, e);
        }

        /// <summary>
        /// Starts the processing
        /// </summary>
        public void StartProcessing()
        {
            StackButtons.Visibility = Visibility.Collapsed;
            ProgressProcessing.StartProcessing();
        }

        /// <summary>
        /// Starts the processing
        /// </summary>
        public void StartProcessing(string processingMessage)
        {
            StackButtons.Visibility = Visibility.Collapsed;
            ProgressProcessing.StartProcessing(processingMessage);
        }

        /// <summary>
        /// Cancels processing with no message and no display of processing results.
        /// </summary>
        public void CancelProcessing()
        {
            ProgressProcessing.CancelProcessing();
            StackButtons.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Stops processing, and supplies WorkerResult data
        /// </summary>
        /// <param name="results">WorkerResult class with results of the processing.</param>
        public void StopProcessing(WorkerResult results)
        {
            ProgressProcessing.StopProcessing(results);
            StackButtons.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Stops processing, and supplies WorkerResult data
        /// </summary>
        /// <param name="results">WorkerResult class with results of the processing.</param>
        /// <param name="successMessage">Message to display if success</param>
        public void StopProcessing(WorkerResult results, string successMessage)
        {
            ProgressProcessing.StopProcessing(results, successMessage);
            StackButtons.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Validate this control
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            return true;
        }
    }
}