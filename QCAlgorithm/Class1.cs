
using QuantConnect.Algorithm;
using QuantConnect.Data.Market;
using QuantConnect.Securities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace $safeprojectname$
{
	/*
	*   QuantConnect University: Full Basic Template:
	*
	*   The underlying QCAlgorithm class is full of helper methods which enable you to use QuantConnect.
	*   We have explained some of these here, but the full algorithm can be found at:
	*   https://github.com/QuantConnect/QCAlgorithm/blob/master/QuantConnect.Algorithm/QCAlgorithm.cs
	*/
	public class BasicTemplateAlgorithm : QCAlgorithm
	{
		Security SPY;
		
		//Initialize the data and resolution you require for your strategy:
		public override void Initialize()
		{

			//Start and End Date range for the backtest:
			SetStartDate(2013, 1, 1);
			SetEndDate(DateTime.Now.Date.AddDays(-1));

			//Cash allocation
			SetCash(25000);

			//Add as many securities as you like. All the data will be passed into the event handler:
			SPY = AddSecurity(SecurityType.Equity, "SPY", Resolution.Minute);
		}

		//Data Event Handler: New data arrives here. "TradeBars" type is a dictionary of strings so you can access it by symbol.
		public void OnData(TradeBars data)
		{
			// "TradeBars" object holds many "TradeBar" objects: it is a dictionary indexed by the symbol:
			// 
			//  e.g.  data["MSFT"] data["GOOG"]

			if (!Portfolio.HoldStock)
			{
				int quantity = (int)Math.Floor(Portfolio.Cash / data["SPY"].Close);

				//Order function places trades: enter the string symbol and the quantity you want:
				Order(SPY.Symbol, quantity);

				//Debug sends messages to the user console: "Time" is the algorithm time keeper object 
				Debug("Purchased SPY on " + Time.ToShortDateString());

				//You can also use log to send longer messages to a file. You are capped to 10kb
				//Log("This is a longer message send to log.");
			}
		}
	}
}
