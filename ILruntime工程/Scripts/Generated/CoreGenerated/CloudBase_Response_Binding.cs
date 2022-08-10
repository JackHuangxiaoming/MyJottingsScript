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
    unsafe class CloudBase_Response_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(CloudBase.Response);

            field = type.GetField("Code", flag);
            app.RegisterCLRFieldGetter(field, get_Code_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_Code_0, null);
            field = type.GetField("Message", flag);
            app.RegisterCLRFieldGetter(field, get_Message_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_Message_1, null);
            field = type.GetField("RequestId", flag);
            app.RegisterCLRFieldGetter(field, get_RequestId_2);
            app.RegisterCLRFieldBinding(field, CopyToStack_RequestId_2, null);


        }



        static object get_Code_0(ref object o)
        {
            return ((CloudBase.Response)o).Code;
        }

        static StackObject* CopyToStack_Code_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((CloudBase.Response)o).Code;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static object get_Message_1(ref object o)
        {
            return ((CloudBase.Response)o).Message;
        }

        static StackObject* CopyToStack_Message_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((CloudBase.Response)o).Message;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static object get_RequestId_2(ref object o)
        {
            return ((CloudBase.Response)o).RequestId;
        }

        static StackObject* CopyToStack_RequestId_2(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((CloudBase.Response)o).RequestId;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }



    }
}
