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
    unsafe class CloudBase_AuthState_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(CloudBase.AuthState);

            field = type.GetField("AuthType", flag);
            app.RegisterCLRFieldGetter(field, get_AuthType_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_AuthType_0, null);


        }



        static object get_AuthType_0(ref object o)
        {
            return ((CloudBase.AuthState)o).AuthType;
        }

        static StackObject* CopyToStack_AuthType_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((CloudBase.AuthState)o).AuthType;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }



    }
}
