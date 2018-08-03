using Mono.Cecil;

namespace IX.Aspects.Weaver.Fody
{
    public interface ILoadable
    {
        TypeReference PersistedType { get; }
        InstructionBlock Load(bool forDereferencing);
    }
}
