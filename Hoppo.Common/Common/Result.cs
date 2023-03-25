namespace Hoppo.Common.Common
{
    public class Result
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }

    public class GetOneResult<TEntity> : Result where TEntity : class, new()
    {
        public TEntity Data { get; set; }
    }

    public class GetManyResult<TEntity> : Result where TEntity : class, new()
    {
        public IEnumerable<TEntity> Data { get; set; }
    }
}
