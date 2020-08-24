namespace Krypton.Core.Architecture
{
    public enum VMOpCode
    {
        Nop,

        Ldstr,

        Call,

        Br,
        BrFalse,

        Ldloc,
        Stloc,

        Pop,

        Ldc_I4
    }
}