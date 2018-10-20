using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Mono.Cecil;
using NBrowse.Reflection;

namespace NBrowse.Reflection
{
    public struct AssemblyModel
    {
        // See: https://github.com/jbevain/cecil/wiki/HOWTO
        private static readonly Func<TypeDefinition, bool> IsVisible = member => member.CustomAttributes.All(attribute => attribute.AttributeType.FullName != typeof(CompilerGeneratedAttribute).FullName);

        public string Name => _assembly.Name.Name;
        public Version Version => _assembly.Name.Version;
        public IEnumerable<TypeModel> Types => _assembly.Modules.SelectMany(module => module.GetTypes()).Where(IsVisible).Select(type => new TypeModel(type));

        private readonly AssemblyDefinition _assembly;

        public AssemblyModel(AssemblyDefinition assembly)
        {
            _assembly = assembly;
        }

        public override string ToString()
        {
            return $"{{Assembly={Name}}}";
        }
    }
}