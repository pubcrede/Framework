﻿//-----------------------------------------------------------------------
// <copyright file="WpfApplication.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using Genesys.Extensions;
using Genesys.Extras.Configuration;
using Genesys.Extras.Net;
using System.Windows;
using System.Linq;
using System.Windows.Navigation;

namespace Genesys.Framework.Application
{
    /// <summary>
    /// Global application information
    /// </summary>
    public abstract class WpfApplication : System.Windows.Application, IWpfApplication
    {
        /// <summary>
        /// Persistent ConfigurationManager class, automatically loaded with this project .config files
        /// </summary>
        public IConfigurationManager ConfigurationManager { get; set; } = new ConfigurationManagerFull();

        /// <summary>
        /// MyWebService
        /// </summary>
        public Uri MyWebService { get { return new Uri(ConfigurationManager.AppSettingValue("MyWebService"), UriKind.RelativeOrAbsolute); } }

        /// <summary>
        /// Entry point Screen (Typically login screen)
        /// </summary>
        public abstract new Uri StartupUri { get; }

        /// <summary>
        /// Home dashboard screen
        /// </summary>
        public abstract Uri HomePage { get; }

        /// <summary>
        /// Error screen
        /// </summary>
        public abstract Uri ErrorPage { get; }

        /// <summary>
        /// Returns currently active window
        /// </summary>
        public static new Window Current { get { return System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive); } }

        /// <summary>
        /// Error screen
        /// </summary>
        public Uri CurrentPage { get { return ((NavigationWindow)WpfApplication.Current).CurrentSource; } }

        /// <summary>
        /// Constructor
        /// </summary>
        public WpfApplication() : base()
        {            
            OnObjectInitialize();
        }

        /// <summary>
        /// Loads config data
        /// </summary>
        /// <returns></returns>
        public async Task LoadDataAsync()
        {
            await Task.Delay(1);
            ConfigurationManager = new ConfigurationManagerFull();
        }

        /// <summary>
        /// Wakes up any sleeping processes, and MyWebService chain
        /// </summary>
        /// <returns></returns>
        public virtual async Task WakeServicesAsync()
        {
            if (MyWebService.ToString() == TypeExtension.DefaultString)
            {
                HttpRequestGetString Request = new HttpRequestGetString(MyWebService.ToString());
                Request.ThrowExceptionWithEmptyReponse = false;
                await Request.SendAsync();
            }
        }

        /// <summary>
        /// Gets the root frame of the application
        /// </summary>
        /// <returns></returns>
        public Frame RootFrame
        {
            get
            {
                Frame returnValue = new Frame();

                if (Current.Content is Frame)
                {
                    returnValue = (Frame)Current.Content;
                }
                return returnValue;
            }
        }

        /// <summary>
        /// Can this screen go back
        /// </summary>
        public bool CanGoBack { get { return RootFrame.CanGoBack; } }

        /// <summary>
        /// Can this screen go forward
        /// </summary>
        public bool CanGoForward { get { return RootFrame.CanGoForward; } }

        /// <summary>
        /// Navigates back to previous screen
        /// </summary>
        public void GoBack() { RootFrame.GoBack(); }

        /// <summary>
        /// Navigates forward to next screen
        /// </summary>
        public void GoForward() { RootFrame.GoForward(); }

        /// <summary>
        /// Navigates to a page via Uri.
        /// Typically in WPF apps
        /// </summary>
        /// <param name="destinationPageUrl">Destination page Uri</param>
        public bool Navigate(Uri destinationPageUrl) { return RootFrame.Navigate(destinationPageUrl); }

        /// <summary>
        /// Navigates to a page via Uri.
        /// Typically in WPF apps
        /// </summary>
        /// <param name="destinationPageUrl">Destination page Uri</param>
        /// <param name="dataToPass">Data to be passed to the destination page</param>
        public bool Navigate<TModel>(Uri destinationPageUrl, TModel dataToPass) { return RootFrame.Navigate(destinationPageUrl, dataToPass); }

        /// <summary>
        /// New model to load
        /// </summary>
        public event ObjectInitializeEventHandler ObjectInitialize;

        /// <summary>
        /// OnObjectInitialize()
        /// </summary>
        protected async void OnObjectInitialize()
        {
            await this.LoadDataAsync();
            await this.WakeServicesAsync();
            ObjectInitialize?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Workflow beginning. No custom to return.
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event arguments</param>
        public delegate void ObjectInitializeEventHandler(object sender, EventArgs e);        
        }
}