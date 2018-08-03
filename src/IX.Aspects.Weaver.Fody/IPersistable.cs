using Mono.Cecil;

namespace IX.Aspects.Weaver.Fody
{
    public interface IPersistable : ILoadable
    {
        InstructionBlock Store(InstructionBlock loadNewValueOntoStack, TypeReference typeOnStack);
    }
}
