namespace core.Services.Fixtures
{
    public interface IFixture<TObject>
    {
        abstract TObject? DefaultInstance { get; }
        abstract bool GenerateInDatabase { get; }

        abstract Task ApplyDefault(TObject newInstance);
    }
}