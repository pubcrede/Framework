﻿//-----------------------------------------------------------------------
// <copyright file="IBytesID.cs" company="Genesys Source">
//      Copyright (c) Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace Genesys.Foundation.Entity
{
    /// <summary>
    /// Byte array used in all BLOB objects
    /// </summary>
    
    [CLSCompliant(true)]
    public interface IBytesKey : IKey
    {
        /// <summary>
        /// Bytes
        /// </summary>
        byte[] Bytes { get; set; }
    }
}
