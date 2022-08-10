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
    unsafe class TencentIMSDKMessage_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::TencentIMSDKMessage);

            field = type.GetField("sender", flag);
            app.RegisterCLRFieldGetter(field, get_sender_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_sender_0, null);
            field = type.GetField("nickName", flag);
            app.RegisterCLRFieldGetter(field, get_nickName_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_nickName_1, null);
            field = type.GetField("senderDetail", flag);
            app.RegisterCLRFieldGetter(field, get_senderDetail_2);
            app.RegisterCLRFieldBinding(field, CopyToStack_senderDetail_2, null);
            field = type.GetField("text", flag);
            app.RegisterCLRFieldGetter(field, get_text_3);
            app.RegisterCLRFieldBinding(field, CopyToStack_text_3, null);

            app.RegisterCLRCreateDefaultInstance(type, () => new global::TencentIMSDKMessage());


        }

        static void WriteBackInstance(ILRuntime.Runtime.Enviorment.AppDomain __domain, StackObject* ptr_of_this_method, IList<object> __mStack, ref global::TencentIMSDKMessage instance_of_this_method)
        {
            ptr_of_this_method = ILIntepreter.GetObjectAndResolveReference(ptr_of_this_method);
            switch(ptr_of_this_method->ObjectType)
            {
                case ObjectTypes.Object:
                    {
                        __mStack[ptr_of_this_method->Value] = instance_of_this_method;
                    }
                    break;
                case ObjectTypes.FieldReference:
                    {
                        var ___obj = __mStack[ptr_of_this_method->Value];
                        if(___obj is ILTypeInstance)
                        {
                            ((ILTypeInstance)___obj)[ptr_of_this_method->ValueLow] = instance_of_this_method;
                        }
                        else
                        {
                            var t = __domain.GetType(___obj.GetType()) as CLRType;
                            t.SetFieldValue(ptr_of_this_method->ValueLow, ref ___obj, instance_of_this_method);
                        }
                    }
                    break;
                case ObjectTypes.StaticFieldReference:
                    {
                        var t = __domain.GetType(ptr_of_this_method->Value);
                        if(t is ILType)
                        {
                            ((ILType)t).StaticInstance[ptr_of_this_method->ValueLow] = instance_of_this_method;
                        }
                        else
                        {
                            ((CLRType)t).SetStaticFieldValue(ptr_of_this_method->ValueLow, instance_of_this_method);
                        }
                    }
                    break;
                 case ObjectTypes.ArrayReference:
                    {
                        var instance_of_arrayReference = __mStack[ptr_of_this_method->Value] as global::TencentIMSDKMessage[];
                        instance_of_arrayReference[ptr_of_this_method->ValueLow] = instance_of_this_method;
                    }
                    break;
            }
        }


        static object get_sender_0(ref object o)
        {
            return ((global::TencentIMSDKMessage)o).sender;
        }

        static StackObject* CopyToStack_sender_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::TencentIMSDKMessage)o).sender;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static object get_nickName_1(ref object o)
        {
            return ((global::TencentIMSDKMessage)o).nickName;
        }

        static StackObject* CopyToStack_nickName_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::TencentIMSDKMessage)o).nickName;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static object get_senderDetail_2(ref object o)
        {
            return ((global::TencentIMSDKMessage)o).senderDetail;
        }

        static StackObject* CopyToStack_senderDetail_2(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::TencentIMSDKMessage)o).senderDetail;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static object get_text_3(ref object o)
        {
            return ((global::TencentIMSDKMessage)o).text;
        }

        static StackObject* CopyToStack_text_3(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::TencentIMSDKMessage)o).text;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }



    }
}
