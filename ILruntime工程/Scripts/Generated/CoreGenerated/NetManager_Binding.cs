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
    unsafe class NetManager_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::NetManager);
            args = new Type[]{};
            method = type.GetMethod("get_App", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_App_0);
            args = new Type[]{};
            method = type.GetMethod("get_IsExitAccount", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_IsExitAccount_1);

            field = type.GetField("SubProtocolKey", flag);
            app.RegisterCLRFieldGetter(field, get_SubProtocolKey_0);
            app.RegisterCLRFieldSetter(field, set_SubProtocolKey_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_SubProtocolKey_0, AssignFromStack_SubProtocolKey_0);
            field = type.GetField("PackageStyle", flag);
            app.RegisterCLRFieldGetter(field, get_PackageStyle_1);
            app.RegisterCLRFieldSetter(field, set_PackageStyle_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_PackageStyle_1, AssignFromStack_PackageStyle_1);
            field = type.GetField("PackageNum", flag);
            app.RegisterCLRFieldGetter(field, get_PackageNum_2);
            app.RegisterCLRFieldSetter(field, set_PackageNum_2);
            app.RegisterCLRFieldBinding(field, CopyToStack_PackageNum_2, AssignFromStack_PackageNum_2);


        }


        static StackObject* get_App_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = global::NetManager.App;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* get_IsExitAccount_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = global::NetManager.IsExitAccount;

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method ? 1 : 0;
            return __ret + 1;
        }


        static object get_SubProtocolKey_0(ref object o)
        {
            return global::NetManager.SubProtocolKey;
        }

        static StackObject* CopyToStack_SubProtocolKey_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = global::NetManager.SubProtocolKey;
            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static void set_SubProtocolKey_0(ref object o, object v)
        {
            global::NetManager.SubProtocolKey = (System.Byte)v;
        }

        static StackObject* AssignFromStack_SubProtocolKey_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Byte @SubProtocolKey = (byte)ptr_of_this_method->Value;
            global::NetManager.SubProtocolKey = @SubProtocolKey;
            return ptr_of_this_method;
        }

        static object get_PackageStyle_1(ref object o)
        {
            return global::NetManager.PackageStyle;
        }

        static StackObject* CopyToStack_PackageStyle_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = global::NetManager.PackageStyle;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_PackageStyle_1(ref object o, object v)
        {
            global::NetManager.PackageStyle = (System.String)v;
        }

        static StackObject* AssignFromStack_PackageStyle_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.String @PackageStyle = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            global::NetManager.PackageStyle = @PackageStyle;
            return ptr_of_this_method;
        }

        static object get_PackageNum_2(ref object o)
        {
            return global::NetManager.PackageNum;
        }

        static StackObject* CopyToStack_PackageNum_2(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = global::NetManager.PackageNum;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_PackageNum_2(ref object o, object v)
        {
            global::NetManager.PackageNum = (System.String)v;
        }

        static StackObject* AssignFromStack_PackageNum_2(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.String @PackageNum = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            global::NetManager.PackageNum = @PackageNum;
            return ptr_of_this_method;
        }



    }
}
