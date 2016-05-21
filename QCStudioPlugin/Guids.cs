// Guids.cs
// MUST match guids.h
using System;

namespace QuantConnect.QCStudioPlugin
{
    static class GuidList
    {
        public const string guidQCStudioPluginPkgString = "c061f3cd-9fd7-4e6f-af9f-f925e05a4aab";
        public const string guidQCStudioPluginCmdSetString = "fd160a50-c872-44f1-a621-45ef6a900972";
        public const string guidToolWindowPersistanceString = "5c3c4aae-8a85-47dc-919b-f840dbb485a4";

        public static readonly Guid guidQCStudioPluginCmdSet = new Guid(guidQCStudioPluginCmdSetString);
    };
}