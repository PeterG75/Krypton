using System.Collections.Generic;
using Krypton.Core;

namespace Krypton.Pipeline
{
    public class Devirtualizer
    {
        public Devirtualizer(DevirtualizationCtx Ctx)
        {
            this.Ctx = Ctx;
            Stages = new List<IStage>();
        }

        public DevirtualizationCtx Ctx { get; set; }
        public List<IStage> Stages { get; set; }

        public void Devirtualize()
        {
            foreach (var stage in Stages)
            {
                Ctx.Options.Logger.Info($"Executing {stage.Name} Stage...");
                stage.Run(Ctx);
                Ctx.Options.Logger.Success($"Executed {stage.Name} Stage!");
            }
        }

        public void Save()
        {
            Ctx.Module.Write(Ctx.Options.OutPath);
            Ctx.Options.Logger.Success($"Wrote File At {Ctx.Options.OutPath}");
        }
    }
}