namespace RealtyParser.Trace
{
    public interface ITrace
    {
        ProgressCallback ProgressCallback { get; set; }
        AppendLineCallback AppendLineCallback { get; set; }
        CompliteCallback CompliteCallback { get; set; }
    }
}