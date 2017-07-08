﻿//-----------------------------------------------------------------------
// <copyright file="ReadOnlyViewModel.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Genesys.Foundation.Pages;
using Genesys.Foundation.Entity;
using Genesys.Foundation.Operation;
using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Genesys.Foundation.Application
{
    /// <summary>
    /// ViewModel holds model and is responsible for server calls, navigation, etc.
    /// </summary>
    public class WpfViewModel<TModel> : CrudViewModel<TModel>, ICrudAsync<TModel, int> where TModel : ModelEntity<TModel>, new()
    {
        /// <summary>
        /// Currently running application
        /// </summary>
        public override IApplication MyApplication { get { return (WpfApplication)System.Windows.Application.Current; } protected set { } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="webServiceControllerName">Web API support endpoint's controller to use as path</param>
        public WpfViewModel(string webServiceControllerName) 
            : base(webServiceControllerName)
        { }
        
        /// <summary>
        /// Navigates to any screen
        /// </summary>
        /// <param name="destinationUri">Destination Uri, absolute or relative</param>
        /// <param name="dataToPass">Screen data</param>
        public void Navigate(string destinationUri, object dataToPass = null)
        {
            Navigate(new Uri(destinationUri, UriKind.RelativeOrAbsolute), dataToPass);
        }

        /// <summary>
        /// Navigates to any screen
        /// </summary>
        /// <param name="destinationUri">Destination Uri, absolute or relative</param>
        /// <param name="dataToPass">Screen data</param>
        public void Navigate(Uri destinationUri, object dataToPass = null)
        {
            var navService = NavigationService.GetNavigationService(WpfApplication.Current);
            this.Navigate(destinationUri, dataToPass, navService);
        }

        /// <summary>
        /// Navigates to any screen
        /// </summary>
        /// <param name="destinationUri">Destination Uri, absolute or relative</param>
        /// <param name="dataToPass">Screen data</param>
        /// <param name="navService">Navigation Service object to be used to perform the .Navigate() call</param>
        public virtual void Navigate(Uri destinationUri, object dataToPass, NavigationService navService)
        {
            var newComponent = System.Windows.Application.LoadComponent(destinationUri);

            if (newComponent is ReadOnlyPage)
            {
                navService.LoadCompleted += new LoadCompletedEventHandler(((ReadOnlyPage)newComponent).NavigationService_LoadCompleted);
            }
            navService.Navigate(((Page)newComponent), dataToPass);
        }
    }
}