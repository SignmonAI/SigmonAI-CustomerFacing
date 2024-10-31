namespace core.Services.Fixtures
{
    public interface IFixture<TObject>
    {
        static abstract TObject? Instance { get; }
        abstract bool GenerateInDatabase { get; }

        abstract Task PublishInstance(TObject newInstance);
    }
}