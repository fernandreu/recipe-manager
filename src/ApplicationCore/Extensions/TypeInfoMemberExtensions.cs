using System;
using System.Collections.Generic;
using System.Reflection;

namespace RecipeManager.ApplicationCore.Extensions
{
    public static class TypeInfoMemberExtensions
    {
        public static IEnumerable<ConstructorInfo> GetAllConstructors(this TypeInfo typeInfo)
            => typeInfo.GetAll(ti => ti.DeclaredConstructors);

        public static IEnumerable<EventInfo> GetAllEvents(this TypeInfo typeInfo)
            => typeInfo.GetAll(ti => ti.DeclaredEvents);

        public static IEnumerable<FieldInfo> GetAllFields(this TypeInfo typeInfo)
            => typeInfo.GetAll(ti => ti.DeclaredFields);

        public static IEnumerable<MemberInfo> GetAllMembers(this TypeInfo typeInfo)
            => typeInfo.GetAll(ti => ti.DeclaredMembers);

        public static IEnumerable<MethodInfo> GetAllMethods(this TypeInfo typeInfo)
            => typeInfo.GetAll(ti => ti.DeclaredMethods);

        public static IEnumerable<TypeInfo> GetAllNestedTypes(this TypeInfo typeInfo)
            => typeInfo.GetAll(ti => ti.DeclaredNestedTypes);

        public static IEnumerable<PropertyInfo> GetAllProperties(this TypeInfo typeInfo)
            => typeInfo.GetAll(ti => ti.DeclaredProperties);

        private static IEnumerable<T> GetAll<T>(this TypeInfo typeInfo, Func<TypeInfo, IEnumerable<T>> accessor)
        {
            TypeInfo? tmp = typeInfo;
            while (tmp != null)
            {
                foreach (var t in accessor(tmp))
                {
                    yield return t;
                }

                tmp = tmp.BaseType?.GetTypeInfo();
            }
        }
    }
}
