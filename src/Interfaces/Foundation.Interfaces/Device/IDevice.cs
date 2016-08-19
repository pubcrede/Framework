//-----------------------------------------------------------------------
// <copyright file="IDevice.cs" company="Genesys Source">
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
using System;

namespace Genesys.Foundation.Device
{
    /// <summary>
    /// Device
    /// </summary>    
    [CLSCompliant(true)]
    public interface IDevice : IDeviceUUID
    {
        /// <summary>
        /// Model
        /// </summary>
        string Model { get; set; }

        /// <summary>
        /// Manufacturer
        /// </summary>
        string Manufacturer { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// OSName
        /// </summary>
        string OSName { get; set; }
    }
}