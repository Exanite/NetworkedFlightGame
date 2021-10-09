namespace Networking.Client
{
    public class ConnectResult
    {
        public ConnectResult(bool isSuccess, string failReason = Constants.DefaultReason)
        {
            IsSuccess = isSuccess;
            FailReason = failReason;
        }

        public bool IsSuccess { get; }
        public string FailReason { get; }
    }
}