//-----------------------------------------------------------------------
// <copyright file="IWorkerResult.cs" company="Genesys Source">
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
using Genesys.Extras.Collections;

namespace Genesys.Framework.Worker
{
    /// <summary>
    /// Result of any process
    /// </summary>
    [CLSCompliant(true)]
    public interface IWorkerResult
    {
        /// <summary>
        /// CurrentState
        /// </summary>
        WorkerStates CurrentState { get; set; }

        /// <summary>
        /// OnStart
        /// </summary>
        void OnStart();

        /// <summary>
        /// OnSuccess
        /// </summary>
        void OnSuccess();

        /// <summary>
        /// OnFail
        /// </summary>
        /// <param name="errorMessage"></param>
        void OnFail(string errorMessage);

        /// <summary>
        /// FailedRules
        /// </summary>
        KeyValueListString FailedRules { get; set; }

        /// <summary>
        /// Return ID - Primary Key of record
        /// </summary>
        int ReturnID { get; set; }

        /// <summary>
        /// Return Key - Guid Key for record
        /// </summary>
        Guid ReturnKey { get; set; }

        /// <summary>
        /// Serialized data to be returned to caller
        /// </summary>
        string ReturnData { get; set; }
    }
}
