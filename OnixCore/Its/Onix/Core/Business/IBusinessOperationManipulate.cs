namespace Its.Onix.Core.Business
{
	public interface IBusinessOperationManipulate<in T> : IBusinessOperation where T : class
	{
        int Apply(T dat);
    }
}
