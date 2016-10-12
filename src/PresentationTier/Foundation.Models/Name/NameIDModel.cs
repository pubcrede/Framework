//-----------------------------------------------------------------------
// <copyright file="NameIDModel.cs" company="Genesys Source">
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
using Genesys.Foundation.Entity;

namespace Genesys.Foundation.Name
{
	/// <summary>
	/// Common object across models and business entity
	/// </summary>
	/// <remarks></remarks>
	[CLSCompliant(true)]
	public class NameIDModel : ModelEntity<NameIDModel>, INameID
	{
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; } = TypeExtension.DefaultString;
		
		/// <summary>
		/// Constructor
		/// </summary>
		public NameIDModel() : base()
		{
		}
	}
}
