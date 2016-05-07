# QC.RestAPI v1 Beta.

QCRestAPI is a C# wrapper project containing all the models and information required to create a C# program tapping into the QuantConnect API. It uses basic HTTP Auth for now and requires posting requests over HTTPS.

It has three endpoints:

* Projects - ( create / read / update / delete )
* Compiler - ( create )
* Backtests - ( create / read )

## DEPENDENCIES
To avoid duplicating code we made it reuse the same types as QuantConnect base class library "Common". You need to add this into your project to get it to build. https://github.com/QuantConnect/QCAlgorithm

## FURTHER READING 
See full documentation on the REST API:
https://www.quantconnect.com/api
