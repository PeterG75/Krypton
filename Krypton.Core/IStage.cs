namespace Krypton.Core
{
    public interface IStage
    {
        string Name { get; }
        void Run(DevirtualizationCtx Ctx);
    }
}