﻿/*
 * QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals.
 * Lean Algorithmic Trading Engine v2.0. Copyright 2014 QuantConnect Corporation.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace QuantConnect
{

    /// <summary>
    /// Type of tradable security / underlying asset
    /// </summary>
    public enum SecurityType
    {
        /// <summary>
        /// Base class for all security types:
        /// </summary>
        Base,

        /// <summary>
        /// US Equity Security
        /// </summary>
        Equity,

        /// <summary>
        /// Option Security Type
        /// </summary>
        Option,

        /// <summary>
        /// Commodity Security Type
        /// </summary>
        Commodity,

        /// <summary>
        /// FOREX Security
        /// </summary>
        Forex,

        /// <summary>
        /// Future Security Type
        /// </summary>
        Future,

        /// <summary>
        /// Contract For a Difference Security Type.
        /// </summary>
        Cfd
    }

    /// <summary>
    /// Specifies the different types of options
    /// </summary>
    public enum OptionRight
    {
        /// <summary>
        /// A call option, the right to buy at the strike price
        /// </summary>
        Call,

        /// <summary>
        /// A put option, the right to sell at the strike price
        /// </summary>
        Put
    }

    /// <summary>
    /// Specifies the style of an option
    /// </summary>
    public enum OptionStyle
    {
        /// <summary>
        /// American style options are able to be exercised at any time on or before the expiration date
        /// </summary>
        American,

        /// <summary>
        /// European style options are able to be exercised on the expiration date only.
        /// </summary>
        European
    }
}
