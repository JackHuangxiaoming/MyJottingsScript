using System;
using System.Collections.Generic;
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
    unsafe class UnityEngine_LogType_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(UnityEngine.LogType);

            field = type.GetField("Error", flag);
            app.RegisterCLRFieldGetter(field, get_Error_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_Error_0, null);
            field = type.GetField("Assert", flag);
            app.RegisterCLRFieldGetter(field, get_Assert_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_Assert_1, null);
            field = type.GetField("Warning", flag);
            app.RegisterCLRFieldGetter(field, get_Warning_2);
            app.RegisterCLRFieldBinding(field, CopyToStack_Warning_2, null);
            field = type.GetField("Log", flag);
            app.RegisterCLRFieldGetter(field, get_Log_3);
            app.RegisterCLRFieldBinding(field, CopyToStack_Log_3, null);
            field = type.GetField("Exception", flag);
            app.RegisterCLRFieldGetter(field, get_Exception_4);
            app.RegisterCLRFieldBinding(field, CopyToStack_Exception_4, null);


            app.RegisterCLRCreateDefaultInstance(type, () => new UnityEngine.LogType());
            app.RegisterCLRCreateArrayInstance(type, s => new UnityEngine.LogType[s]);


        }

        static void WriteBackInstance(ILRuntime.Runtime.Enviorment.AppDomain __domain, StackObject* ptr_of_this_method, IList<object> __mStack, ref UnityEngine.LogType instance_of_this_method)
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
                        var instance_of_arrayReference = __mStack[ptr_of_this_method->Value] as UnityEngine.LogType[];
                        instance_of_arrayReference[ptr_of_this_method->ValueLow] = instance_of_this_method;
                    }
                    break;
            }
        }


        static object get_Error_0(ref object o)
        {
            return UnityEngine.LogType.Error;
        }

        static StackObject* CopyToStack_Error_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = UnityEngine.LogType.Error;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static object get_Assert_1(ref object o)
        {
            return UnityEngine.LogType.Assert;
        }

        static StackObject* CopyToStack_Assert_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = UnityEngine.LogType.Assert;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static object get_Warning_2(ref object o)
        {
            return UnityEngine.LogType.Warning;
        }

        static StackObject* CopyToStack_Warning_2(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = UnityEngine.LogType.Warning;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static object get_Log_3(ref object o)
        {
            return UnityEngine.LogType.Log;
        }

        static StackObject* CopyToStack_Log_3(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = UnityEngine.LogType.Log;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static object get_Exception_4(ref object o)
        {
            return UnityEngine.LogType.Exception;
        }

        static StackObject* CopyToStack_Exception_4(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = UnityEngine.LogType.Exception;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


        static object PerformMemberwiseClone(ref object o)
        {
            var ins = new UnityEngine.LogType();
            ins = (UnityEngine.LogType)o;
            return ins;
        }


    }
}
