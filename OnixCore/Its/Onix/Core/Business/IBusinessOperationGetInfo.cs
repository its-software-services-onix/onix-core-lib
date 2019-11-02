namespace Its.Onix.Core.Business
{
	public interface IBusinessOperationGetInfo<T> : IBusinessOperation where T : class
	{
        T Apply(T dat);
    }
}
