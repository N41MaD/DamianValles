﻿namespace WeightLifting.Library.Logger
{
    public interface ILoggerService
    {
        void Info(string message);
        void Warn(string message);
        void Error(string message);
    }
}
