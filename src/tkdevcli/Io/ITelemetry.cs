namespace tkdevcli.Io
{
    internal interface ITelemetry
    {
        void Event(TelemetryEvent evt);
    }
}
