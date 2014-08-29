// Guids.cs
// MUST match guids.h
using System;

namespace QuantConnect.QCPlugin
{
    static class GuidList
    {
        public const string guidQCPluginPkgString = "93f79150-258a-449e-a380-599b5928a20b";
        public const string guidQCPluginCmdSetString = "e2ba1c58-e0a7-43b3-8974-89170ab0b932";
        public const string guidToolWindowPersistanceString = "119a53f6-a3f1-4931-8cd2-849419698c15";

        public static readonly Guid guidQCPluginCmdSet = new Guid(guidQCPluginCmdSetString);
    };
}