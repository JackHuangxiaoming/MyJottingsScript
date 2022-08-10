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
    unsafe class CloudBase_FunctionResponse_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(CloudBase.FunctionResponse);

            field = type.GetField("Data", flag);
            app.RegisterCLRFieldGetter(field, get_Data_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_Data_0, null);


        }



        static object get_Data_0(ref object o)
        {
            return ((CloudBase.FunctionResponse)o).Data;
        }

        static StackObject* CopyToStack_Data_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((CloudBase.FunctionResponse)o).Data;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }



    }
}
