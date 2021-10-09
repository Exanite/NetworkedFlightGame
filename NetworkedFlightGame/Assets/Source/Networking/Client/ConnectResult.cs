/// <summary>
///     The result returned by <see cref="UnityClient.ConnectAsync"/>
/// </summary>
public class ConnectResult
{
    /// <summary>
    ///     Creates a new <see cref="ConnectResult"/>
    /// </summary>
    public ConnectResult(bool isSuccess, string failReason = Constants.DefaultReason)
    {
        this.IsSuccess = isSuccess;
        this.FailReason = failReason;
    }

    /// <summary>
    ///     Was the connect a success?
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    ///     The reason the connected failed
    ///     <para/>
    ///     Note: Invalid if <see cref="IsSuccess"/> is
    ///     <see langword="true"/>
    /// </summary>
    public string FailReason { get; }
}