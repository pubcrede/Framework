//-----------------------------------------------------------------------
// <copyright file="ExceptionLogFull.cs" company="Genesys Source">
//      Copyright (c) Genesys Source. All rights reserved.
// 
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
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using Genesys.Extensions;
using Genesys.Extras.Configuration;

namespace Genesys.Extras.Exceptions
{
    /// <summary>
    /// Code-first class that records exceptions to a 100% uptime database
    /// Default connection string is: MyLogConnection. 
    /// Can be changed via passing new ConnectionString name to the constructor
    /// </summary>
    /// <remarks></remarks>
    [CLSCompliant(true)]    
    public class ExceptionLogger : ExceptionLog
    {
        private Uri endpointUrl = new Uri(TypeExtension.DefaultUri);
        private Exception currentException = new System.Exception("No Exception");

        /// <summary>
        /// Connection string key name, from Web.config or Local\ConnectionStrings.config
        /// Default is MyLogConnection
        /// </summary>
        protected string ConnectionStringName { get; set; } = "MyLogConnection";

        /// <summary>
        /// Database schema name
        /// Default is dbo
        /// </summary>
        protected string DatabaseSchemaName { get; set; } = "Activity";

        /// <summary>
        /// The Activity record that was processing when this exception occurred
        /// </summary>
        public int CreatedActivityID { get; set; } = TypeExtension.DefaultInteger;

        /// <summary>
        /// MachineName
        /// </summary>
        public string MachineName { get { return Environment.MachineName; } }

        /// <summary>
        /// ADDomainName
        /// </summary>
        public string ADDomainName { get { return Environment.UserDomainName; } protected set { } }

        /// <summary>
        /// ADUserName
        /// </summary>
        public string ADUserName { get { return Environment.UserName; } protected set { } }

        /// <summary>
        /// DirectoryWorking
        /// </summary>
        public string DirectoryWorking { get { return Environment.CurrentDirectory; } protected set { } }

        /// <summary>
        /// DirectoryAssembly
        /// </summary>
        public string DirectoryAssembly { get { return Assembly.GetExecutingAssembly().Location; } protected set { } }

        /// <summary>
        /// ApplicationName
        /// </summary>
        public string AssemblyName { get { return Assembly.GetExecutingAssembly().FullName; } protected set { } }

        /// <summary>
        /// URL
        /// </summary>
        public string URL { get { return this.endpointUrl.ToString(); } protected set { } }

        /// <summary>
        /// This protected constructor should not be called. Factory methods should be used instead.
        /// </summary>
        protected ExceptionLogger() : base() { this.CreatedDate = DateTime.UtcNow; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectStringName"></param>
        /// <remarks></remarks>
        public ExceptionLogger(string connectStringName) : this()
        {
            this.ConnectionStringName = connectStringName;
        }

        /// <summary>
        /// Creates Exception object
        /// </summary>
        /// <param name="connectStringName"></param>
        /// <param name="databaseSchema"></param>
        public ExceptionLogger(string connectStringName, string databaseSchema) : this(connectStringName) { this.DatabaseSchemaName = databaseSchema; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="concreteType"></param>
        /// <param name="customMessage"></param>
        public ExceptionLogger(Exception exception, Type concreteType, string customMessage) : base(exception, concreteType, customMessage) { }

        /// <summary>
        /// Hydrates object and saves the log record    
        /// </summary>
        /// <param name="exception">System.Exception object to log</param>
        /// <param name="concreteType">Type that is logging the exception</param>
        /// <param name="customMessage">Custom message to append to the exception log</param>
        /// <param name="connectStringName">Key name of connection string in config</param>
        /// <param name="databaseSchema">Database schema name</param>
        public static void Create(Exception exception, Type concreteType, string customMessage, string connectStringName, string databaseSchema)
        {
            ExceptionLogger log = new ExceptionLogger(exception, concreteType, customMessage) { ConnectionStringName = connectStringName, DatabaseSchemaName = databaseSchema };
            log.Save();
        }

        /// <summary>
        /// Loads an existing object MyBased on ID.
        /// </summary>
        /// <param name="id">The unique ID of the object</param>
        /// <param name="connectStringName">Key of the config value for this actions connection string</param>
        /// <param name="databaseSchema">Database Schema that owns the Activity table</param>
        public static ExceptionLog GetByID(int id, string connectStringName, string databaseSchema)
        {
            ExceptionLog returnValue = new ExceptionLog();
            DatabaseContext dbContext = new DatabaseContext(connectStringName, databaseSchema);

            try
            {
                if (id != TypeExtension.DefaultInteger)
                {
                    returnValue = dbContext.EntityData.Where(x => x.ExceptionLogID == id).FirstOrDefaultSafe();
                }
            }
            catch(Exception ex)
            {
                ExceptionLogger.Create(ex, typeof(ExceptionLog), String.Format("ExceptionLogger.GetByID({0})", id.ToString()), connectStringName, databaseSchema);
            }

            return returnValue;
        }

        /// <summary>
        /// Saves object to database
        /// </summary>
        public virtual int Save()
        {
            using (DatabaseContext dbContext = new DatabaseContext(this.ConnectionStringName, this.DatabaseSchemaName))
            {
                try
                {
                    if (this.ExceptionLogID == TypeExtension.DefaultInteger)
                    {
                        dbContext.EntityData.Add(this);
                        dbContext.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    this.CurrentException = ex; // Never let save errors propagate, else endless loop
                }
            }
                
            return this.ExceptionLogID;
        }
        
        /// <summary>
        /// DB Context - Entity Framework uses this to connect to the database
        /// </summary>
        protected class DatabaseContext : System.Data.Entity.DbContext
        {
            private string databaseSchemaField = "dbo";
            
            /// <summary>
            /// BusinessEntity - Determines table and columns
            /// </summary>
            public System.Data.Entity.DbSet<ExceptionLog> EntityData { get; set; }
            
            /// <summary>
            /// Constructor. Explicitly set database connection.
            /// </summary>
            /// <remarks></remarks>
            public DatabaseContext(string connectStringName, string databaseSchema)
                : base(ConfigurationManagerFull.ConnectionStrings.GetValue(connectStringName))
            {
                this.databaseSchemaField = databaseSchema;
            }

            /// <summary>
            /// SaveChanges - Saves the object to the database
            /// </summary>
            public override int SaveChanges()
            {
                return base.SaveChanges();
            }
            
            /// <summary>
            /// Set values when creating a model in the database
            /// </summary>
            /// <param name="modelBuilder"></param>
            /// <remarks></remarks>
            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.HasDefaultSchema(this.databaseSchemaField);
                base.OnModelCreating(modelBuilder);
                modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            }
        }
         
        /// <summary>
        /// Initializes the database
        /// </summary>
        public void Initialize()
        {
            System.Data.Entity.Database.SetInitializer<DatabaseContext>((global::System.Data.Entity.IDatabaseInitializer<ExceptionLogger.DatabaseContext>)new DatabaseInitializer());
        }

        /// <summary>
        /// Class that initializes and handles seed/identity
        /// </summary>
        protected class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
        {
            /// <summary>
            /// Sets default data
            /// </summary>
            /// <param name="context"></param>
            /// <remarks></remarks>
            protected override void Seed(DatabaseContext context)
            {
                base.Seed(context);
            }            
        }        
    }
}