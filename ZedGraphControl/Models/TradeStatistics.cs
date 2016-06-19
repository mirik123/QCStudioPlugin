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

namespace QuantConnect.Statistics
{
    /// <summary>
    /// The <see cref="TradeStatistics"/> class represents a set of statistics calculated from a list of closed trades
    /// </summary>
    public class TradeStatistics
    {
        /// <summary>
        /// The entry date/time of the first trade
        /// </summary>
        public DateTime? StartDateTime { get; set; }

        /// <summary>
        /// The exit date/time of the last trade
        /// </summary>
        public DateTime? EndDateTime { get; set; }

        /// <summary>
        /// The total number of trades
        /// </summary>
        public int TotalNumberOfTrades { get; set; }

        /// <summary>
        /// The total number of winning trades
        /// </summary>
        public int NumberOfWinningTrades { get; set; }

        /// <summary>
        /// The total number of losing trades
        /// </summary>
        public int NumberOfLosingTrades { get; set; }

        /// <summary>
        /// The total profit/loss for all trades (as symbol currency)
        /// </summary>
        public decimal TotalProfitLoss { get; set; }

        /// <summary>
        /// The total profit for all winning trades (as symbol currency)
        /// </summary>
        public decimal TotalProfit { get; set; }

        /// <summary>
        /// The total loss for all losing trades (as symbol currency)
        /// </summary>
        public decimal TotalLoss { get; set; }

        /// <summary>
        /// The largest profit in a single trade (as symbol currency)
        /// </summary>
        public decimal LargestProfit { get; set; }

        /// <summary>
        /// The largest loss in a single trade (as symbol currency)
        /// </summary>
        public decimal LargestLoss { get; set; }

        /// <summary>
        /// The average profit/loss (a.k.a. Expectancy or Average Trade) for all trades (as symbol currency)
        /// </summary>
        public decimal AverageProfitLoss { get; set; }

        /// <summary>
        /// The average profit for all winning trades (as symbol currency)
        /// </summary>
        public decimal AverageProfit { get; set; }

        /// <summary>
        /// The average loss for all winning trades (as symbol currency)
        /// </summary>
        public decimal AverageLoss { get; set; }

        /// <summary>
        /// The average duration for all trades
        /// </summary>
        public TimeSpan AverageTradeDuration { get; set; }

        /// <summary>
        /// The average duration for all winning trades
        /// </summary>
        public TimeSpan AverageWinningTradeDuration { get; set; }

        /// <summary>
        /// The average duration for all losing trades
        /// </summary>
        public TimeSpan AverageLosingTradeDuration { get; set; }

        /// <summary>
        /// The maximum number of consecutive winning trades
        /// </summary>
        public int MaxConsecutiveWinningTrades { get; set; }

        /// <summary>
        /// The maximum number of consecutive losing trades
        /// </summary>
        public int MaxConsecutiveLosingTrades { get; set; }

        /// <summary>
        /// The ratio of the average profit per trade to the average loss per trade
        /// </summary>
        /// <remarks>If the average loss is zero, ProfitLossRatio is set to 0</remarks>
        public decimal ProfitLossRatio { get; set; }

        /// <summary>
        /// The ratio of the number of winning trades to the number of losing trades
        /// </summary>
        /// <remarks>If the total number of trades is zero, WinLossRatio is set to zero</remarks>
        /// <remarks>If the number of losing trades is zero and the number of winning trades is nonzero, WinLossRatio is set to 10</remarks>
        public decimal WinLossRatio { get; set; }

        /// <summary>
        /// The ratio of the number of winning trades to the total number of trades
        /// </summary>
        /// <remarks>If the total number of trades is zero, WinRate is set to zero</remarks>
        public decimal WinRate { get; set; }

        /// <summary>
        /// The ratio of the number of losing trades to the total number of trades
        /// </summary>
        /// <remarks>If the total number of trades is zero, LossRate is set to zero</remarks>
        public decimal LossRate { get; set; }

        /// <summary>
        /// The average Maximum Adverse Excursion for all trades
        /// </summary>
        public decimal AverageMAE { get; set; }

        /// <summary>
        /// The average Maximum Favorable Excursion for all trades
        /// </summary>
        public decimal AverageMFE { get; set; }

        /// <summary>
        /// The largest Maximum Adverse Excursion in a single trade (as symbol currency)
        /// </summary>
        public decimal LargestMAE { get; set; }

        /// <summary>
        /// The largest Maximum Favorable Excursion in a single trade (as symbol currency)
        /// </summary>
        public decimal LargestMFE { get; set; }

        /// <summary>
        /// The maximum closed-trade drawdown for all trades (as symbol currency)
        /// </summary>
        /// <remarks>The calculation only takes into account the profit/loss of each trade</remarks>
        public decimal MaximumClosedTradeDrawdown { get; set; }

        /// <summary>
        /// The maximum intra-trade drawdown for all trades (as symbol currency)
        /// </summary>
        /// <remarks>The calculation takes into account MAE and MFE of each trade</remarks>
        public decimal MaximumIntraTradeDrawdown { get; set; }

        /// <summary>
        /// The standard deviation of the profits/losses for all trades (as symbol currency)
        /// </summary>
        public decimal ProfitLossStandardDeviation { get; set; }

        /// <summary>
        /// The downside deviation of the profits/losses for all trades (as symbol currency)
        /// </summary>
        /// <remarks>This metric only considers deviations of losing trades</remarks>
        public decimal ProfitLossDownsideDeviation { get; set; }

        /// <summary>
        /// The ratio of the total profit to the total loss
        /// </summary>
        /// <remarks>If the total profit is zero, ProfitFactor is set to zero</remarks>
        /// <remarks>if the total loss is zero and the total profit is nonzero, ProfitFactor is set to 10</remarks>
        public decimal ProfitFactor { get; set; }

        /// <summary>
        /// The ratio of the average profit/loss to the standard deviation
        /// </summary>
        public decimal SharpeRatio { get; set; }

        /// <summary>
        /// The ratio of the average profit/loss to the downside deviation
        /// </summary>
        public decimal SortinoRatio { get; set; }

        /// <summary>
        /// The ratio of the total profit/loss to the maximum closed trade drawdown
        /// </summary>
        /// <remarks>If the total profit/loss is zero, ProfitToMaxDrawdownRatio is set to zero</remarks>
        /// <remarks>if the drawdown is zero and the total profit is nonzero, ProfitToMaxDrawdownRatio is set to 10</remarks>
        public decimal ProfitToMaxDrawdownRatio { get; set; }

        /// <summary>
        /// The maximum amount of profit given back by a single trade before exit (as symbol currency)
        /// </summary>
        public decimal MaximumEndTradeDrawdown { get; set; }

        /// <summary>
        /// The average amount of profit given back by all trades before exit (as symbol currency)
        /// </summary>
        public decimal AverageEndTradeDrawdown { get; set; }

        /// <summary>
        /// The maximum amount of time to recover from a drawdown (longest time between new equity highs or peaks)
        /// </summary>
        public TimeSpan MaximumDrawdownDuration { get; set; }

        /// <summary>
        /// The sum of fees for all trades
        /// </summary>
        public decimal TotalFees { get; set; }       
    }
}
