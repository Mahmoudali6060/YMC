namespace Origin.YMC.CrossCutting.Framework
{
    /// <summary>
    /// Base contract for abstract factory
    /// </summary>
    public interface IIdentityProviderFactory
    {
        /// <summary>
        /// Create a new IMMSIdentity
        /// </summary>
        /// <returns>
        /// The IMMSIdentity created
        /// </returns>
        IIdentityProvider Create();
    }
}
