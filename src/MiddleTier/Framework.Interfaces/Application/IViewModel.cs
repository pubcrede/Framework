//-----------------------------------------------------------------------
// <copyright file="IViewModel.cs" company="Genesys Source">
//      Copyright (c) Genesys Source. All rights reserved.
//      Licensed to the Apache Software Foundation (ASF) under one or more 
//      contributor license agreements.  See the NOTICE file distributed with 
//      this work for additional information regarding copyright ownership.
//      The ASF licenses this file to You under the Apache License, Version 2.0 
//      (the 'License'); you may not use this file except in compliance with 
//      the License.  You may obtain a copy of the License at 
//       
//        http://www.apache.org/licenses/LICENSE-2.0 
//       
//       Unless required by applicable law or agreed to in writing, software  
//       distributed under the License is distributed on an 'AS IS' BASIS, 
//       WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  
//       See the License for the specific language governing permissions and  
//       limitations under the License. 
// </copyright>
//-----------------------------------------------------------------------
using Genesys.Extras.Net;
using System.ComponentModel;

namespace Genesys.Framework.Application
{
    /// <summary>
    /// Interface to enforce ViewModel division of responsibilities
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public interface IViewModel<TModel> : INotifyPropertyChanged
    {
        /// <summary>
        /// Configuration data
        ///  Data must be constructed in the application tier
        /// </summary>
        IApplication MyApplication { get; }

        /// <summary>
        /// Model data
        /// </summary>
        TModel MyModel { get; }

        /// <summary>
        /// Sender of main Http Verbs
        /// </summary>
        HttpVerbSender Sender { get; }
    }
}
