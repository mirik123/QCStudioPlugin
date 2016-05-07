/*
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

using System;
using System.Collections.Generic;

namespace QuantConnect.Orders
{
    /// <summary>
    /// Order struct for placing new trade
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Order ID.
        /// </summary>
        public int Id;

        /// <summary>
        /// Order id to process before processing this order.
        /// </summary>
        public int ContingentId;

        /// <summary>
        /// Brokerage Id for this order for when the brokerage splits orders into multiple pieces
        /// </summary>
        public List<string> BrokerId;

        /// <summary>
        /// Symbol of the Asset
        /// </summary>
        public Symbol Symbol;

        /// <summary>
        /// Price of the Order.
        /// </summary>
        public decimal Price;

        /// <summary>
        /// Currency for the order price
        /// </summary>
        public string PriceCurrency;

        /// <summary>
        /// Time the order was created.
        /// </summary>
        public DateTime Time;

        /// <summary>
        /// Number of shares to execute.
        /// </summary>
        public int Quantity;

        /// <summary>
        /// Order Type
        /// </summary>
        public OrderType Type;

        /// <summary>
        /// Status of the Order
        /// </summary>
        public OrderStatus Status;

        /// <summary>
        /// Order duration - GTC or Day. Day not supported in backtests.
        /// </summary>
        public OrderDuration Duration;

        /// <summary>
        /// Tag the order with some custom data
        /// </summary>
        public string Tag;

        /// <summary>
        /// The symbol's security type
        /// </summary>
        public SecurityType SecurityType { get { return Symbol.ID.SecurityType; } }

        /// <summary>
        /// Order Direction Property based off Quantity.
        /// </summary>
        public OrderDirection Direction
        {
            get
            {
                if (Quantity > 0)
                {
                    return OrderDirection.Buy;
                }
                if (Quantity < 0)
                {
                    return OrderDirection.Sell;
                }
                return OrderDirection.Hold;
            }
        }

        /// <summary>
        /// Get the absolute quantity for this order
        /// </summary>
        public decimal AbsoluteQuantity
        {
            get { return Math.Abs(Quantity); }
        }

        /// <summary>
        /// Gets the executed value of this order. If the order has not yet filled,
        /// then this will return zero.
        /// </summary>
        public decimal Value
        {
            get { return Quantity * Price; }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.Format("OrderId: {0} {1} {2} order for {3} unit{4} of {5}", Id, Status, Type, Quantity, Quantity == 1 ? "" : "s", Symbol);
        }

        /// <summary>
        /// Order Expiry on a specific UTC time.
        /// </summary>
        public DateTime DurationValue;
    }
}
