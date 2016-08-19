//-----------------------------------------------------------------------
// <copyright file="DatabaseSchemaAttribute.cs" company="Genesys Source">
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
using Genesys.Extensions;

namespace Genesys.Foundation.Data
{
    /// <summary>
    /// Class attribute decoration that holds the DatabaseSchemaName 
    /// Name is the key used to lookup connection string from config file.
    /// </summary>    
    [AttributeUsage(AttributeTargets.All)]
    public class DatabaseSchemaAttribute : Attribute, IAttributeValue<string>
    {
        /// <summary>
        /// Name supplied by attribute. 
        /// Default is MyDataConnection
        /// </summary>
        public string Value { get; set; } = "dbo";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="databaseSchemaValue">Database schema name</param>
        public DatabaseSchemaAttribute(string databaseSchemaValue)
        {
            this.Value = databaseSchemaValue;
        }
    }
}