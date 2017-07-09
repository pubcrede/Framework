﻿//-----------------------------------------------------------------------
// <copyright file="IText.cs" company="Genesys Source">
//      Copyright (c) Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace Genesys.Foundation.Text
{
    /// <summary>
    /// Text interface
    /// </summary>
    [CLSCompliant(true)]
    public interface IText
    {
        /// <summary>
        /// LanguageISO
        /// </summary>
        string LanguageISO { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        string Message { get; set; }
    }
}
