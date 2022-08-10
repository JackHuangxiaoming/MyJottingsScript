using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class ParticleSystemSubjoin_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::ParticleSystemSubjoin);

            field = type.GetField("PS", flag);
            app.RegisterCLRFieldGetter(field, get_PS_0);
            app.RegisterCLRFieldSetter(field, set_PS_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_PS_0, AssignFromStack_PS_0);


        }



        static object get_PS_0(ref object o)
        {
            return ((global::ParticleSystemSubjoin)o).PS;
        }

        static StackObject* CopyToStack_PS_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::ParticleSystemSubjoin)o).PS;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_PS_0(ref object o, object v)
        {
            ((global::ParticleSystemSubjoin)o).PS = (UnityEngine.ParticleSystem)v;
        }

        static StackObject* AssignFromStack_PS_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            UnityEngine.ParticleSystem @PS = (UnityEngine.ParticleSystem)typeof(UnityEngine.ParticleSystem).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::ParticleSystemSubjoin)o).PS = @PS;
            return ptr_of_this_method;
        }



    }
}
