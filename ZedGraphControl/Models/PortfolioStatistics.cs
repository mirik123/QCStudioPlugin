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
using System.Linq;

namespace QuantConnect.Statistics
{
    /// <summary>
    /// The <see cref="PortfolioStatistics"/> class represents a set of statistics calculated from equity and benchmark samples
    /// </summary>
    public class PortfolioStatistics
    {
        /// <summary>
        /// The average rate of return for winning trades
        /// </summary>
        public decimal AverageWinRate { get; set; }

        /// <summary>
        /// The average rate of return for losing trades
        /// </summary>
        public decimal AverageLossRate { get; set; }

        /// <summary>
        /// The ratio of the average win rate to the average loss rate
        /// </summary>
        /// <remarks>If the average loss rate is zero, ProfitLossRatio is set to 0</remarks>
        public decimal ProfitLossRatio { get; set; }

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
        /// The expected value of the rate of return
        /// </summary>
        public decimal Expectancy { get; set; }

        /// <summary>
        /// Annual compounded returns statistic based on the final-starting capital and years.
        /// </summary>
        /// <remarks>Also known as Compound Annual Growth Rate (CAGR)</remarks>
        public decimal CompoundingAnnualReturn { get; set; }

        /// <summary>
        /// Drawdown maximum percentage.
        /// </summary>
        public decimal Drawdown { get; set; }

        /// <summary>
        /// The total net profit percentage.
        /// </summary>
        public decimal TotalNetProfit { get; set; }

        /// <summary>
        /// Sharpe ratio with respect to risk free rate: measures excess of return per unit of risk.
        /// </summary>
        /// <remarks>With risk defined as the algorithm's volatility</remarks>
        public decimal SharpeRatio { get; set; }

        /// <summary>
        /// Algorithm "Alpha" statistic - abnormal returns over the risk free rate and the relationshio (beta) with the benchmark returns.
        /// </summary>
        public decimal Alpha { get; set; }

        /// <summary>
        /// Algorithm "beta" statistic - the covariance between the algorithm and benchmark performance, divided by benchmark's variance
        /// </summary>
        public decimal Beta { get; set; }

        /// <summary>
        /// Annualized standard deviation
        /// </summary>
        public decimal AnnualStandardDeviation { get; set; }

        /// <summary>
        /// Annualized variance statistic calculation using the daily performance variance and trading days per year.
        /// </summary>
        public decimal AnnualVariance { get; set; }

        /// <summary>
        /// Information ratio - risk adjusted return
        /// </summary>
        /// <remarks>(risk = tracking error volatility, a volatility measures that considers the volatility of both algo and benchmark)</remarks>
        public decimal InformationRatio { get; set; }

        /// <summary>
        /// Tracking error volatility (TEV) statistic - a measure of how closely a portfolio follows the index to which it is benchmarked
        /// </summary>
        /// <remarks>If algo = benchmark, TEV = 0</remarks>
        public decimal TrackingError { get; set; }

        /// <summary>
        /// Treynor ratio statistic is a measurement of the returns earned in excess of that which could have been earned on an investment that has no diversifiable risk
        /// </summary>
        public decimal TreynorRatio { get; set; }        
    }
}
