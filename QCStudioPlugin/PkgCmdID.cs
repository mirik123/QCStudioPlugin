// PkgCmdID.cs
// MUST match PkgCmdID.h
using System;

namespace QuantConnect.QCPlugin
{
    static class PkgCmdIDList
    {
        public const uint cmdIdQuantConnect = 0x100;
        public const int setCredentials = 0x0500;
        public const int newProject = 0x0600;
        public const int openProject = 0x0700;
        public const int backtest = 0x0800;
        public const int logout = 0x0900;
        public const int delete = 0x1000;
        public const int save = 0x1100;
    };
}