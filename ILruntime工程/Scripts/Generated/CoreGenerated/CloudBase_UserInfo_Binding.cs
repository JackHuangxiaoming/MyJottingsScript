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
    unsafe class CloudBase_UserInfo_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(CloudBase.UserInfo);

            field = type.GetField("Uuid", flag);
            app.RegisterCLRFieldGetter(field, get_Uuid_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_Uuid_0, null);


        }



        static object get_Uuid_0(ref object o)
        {
            return ((CloudBase.UserInfo)o).Uuid;
        }

        static StackObject* CopyToStack_Uuid_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((CloudBase.UserInfo)o).Uuid;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }



    }
}
