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
    unsafe class CloudBase_CloudBaseApp_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(CloudBase.CloudBaseApp);

            field = type.GetField("Auth", flag);
            app.RegisterCLRFieldGetter(field, get_Auth_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_Auth_0, null);
            field = type.GetField("Function", flag);
            app.RegisterCLRFieldGetter(field, get_Function_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_Function_1, null);


        }



        static object get_Auth_0(ref object o)
        {
            return ((CloudBase.CloudBaseApp)o).Auth;
        }

        static StackObject* CopyToStack_Auth_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((CloudBase.CloudBaseApp)o).Auth;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static object get_Function_1(ref object o)
        {
            return ((CloudBase.CloudBaseApp)o).Function;
        }

        static StackObject* CopyToStack_Function_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((CloudBase.CloudBaseApp)o).Function;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }



    }
}
