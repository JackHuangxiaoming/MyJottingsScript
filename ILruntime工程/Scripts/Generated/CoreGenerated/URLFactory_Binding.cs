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
    unsafe class URLFactory_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::URLFactory);
            args = new Type[]{typeof(System.String), typeof(System.String)};
            method = type.GetMethod("FormatWebPhpUrl", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, FormatWebPhpUrl_0);
            args = new Type[]{typeof(System.Boolean), typeof(System.Object[])};
            method = type.GetMethod("FormatPhpParam", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, FormatPhpParam_1);

            field = type.GetField("GameAuditState", flag);
            app.RegisterCLRFieldGetter(field, get_GameAuditState_0);
            app.RegisterCLRFieldSetter(field, set_GameAuditState_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_GameAuditState_0, AssignFromStack_GameAuditState_0);
            field = type.GetField("WebServerPath", flag);
            app.RegisterCLRFieldGetter(field, get_WebServerPath_1);
            app.RegisterCLRFieldSetter(field, set_WebServerPath_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_WebServerPath_1, AssignFromStack_WebServerPath_1);
            field = type.GetField("RequestIdenCodePhp", flag);
            app.RegisterCLRFieldGetter(field, get_RequestIdenCodePhp_2);
            app.RegisterCLRFieldSetter(field, set_RequestIdenCodePhp_2);
            app.RegisterCLRFieldBinding(field, CopyToStack_RequestIdenCodePhp_2, AssignFromStack_RequestIdenCodePhp_2);
            field = type.GetField("RequestCloudBaseTicketPhp", flag);
            app.RegisterCLRFieldGetter(field, get_RequestCloudBaseTicketPhp_3);
            app.RegisterCLRFieldSetter(field, set_RequestCloudBaseTicketPhp_3);
            app.RegisterCLRFieldBinding(field, CopyToStack_RequestCloudBaseTicketPhp_3, AssignFromStack_RequestCloudBaseTicketPhp_3);
            field = type.GetField("RequestSocketIOTokenPhp", flag);
            app.RegisterCLRFieldGetter(field, get_RequestSocketIOTokenPhp_4);
            app.RegisterCLRFieldSetter(field, set_RequestSocketIOTokenPhp_4);
            app.RegisterCLRFieldBinding(field, CopyToStack_RequestSocketIOTokenPhp_4, AssignFromStack_RequestSocketIOTokenPhp_4);
            field = type.GetField("GameVersion", flag);
            app.RegisterCLRFieldGetter(field, get_GameVersion_5);
            app.RegisterCLRFieldSetter(field, set_GameVersion_5);
            app.RegisterCLRFieldBinding(field, CopyToStack_GameVersion_5, AssignFromStack_GameVersion_5);


        }


        static StackObject* FormatWebPhpUrl_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.String @phpName = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.String @phpServerPath = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = global::URLFactory.FormatWebPhpUrl(@phpServerPath, @phpName);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* FormatPhpParam_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Object[] @paramsArr = (System.Object[])typeof(System.Object[]).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Boolean @isPost = ptr_of_this_method->Value == 1;


            var result_of_this_method = global::URLFactory.FormatPhpParam(@isPost, @paramsArr);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


        static object get_GameAuditState_0(ref object o)
        {
            return global::URLFactory.GameAuditState;
        }

        static StackObject* CopyToStack_GameAuditState_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = global::URLFactory.GameAuditState;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_GameAuditState_0(ref object o, object v)
        {
            global::URLFactory.GameAuditState = (System.String)v;
        }

        static StackObject* AssignFromStack_GameAuditState_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.String @GameAuditState = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            global::URLFactory.GameAuditState = @GameAuditState;
            return ptr_of_this_method;
        }

        static object get_WebServerPath_1(ref object o)
        {
            return global::URLFactory.WebServerPath;
        }

        static StackObject* CopyToStack_WebServerPath_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = global::URLFactory.WebServerPath;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_WebServerPath_1(ref object o, object v)
        {
            global::URLFactory.WebServerPath = (System.String)v;
        }

        static StackObject* AssignFromStack_WebServerPath_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.String @WebServerPath = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            global::URLFactory.WebServerPath = @WebServerPath;
            return ptr_of_this_method;
        }

        static object get_RequestIdenCodePhp_2(ref object o)
        {
            return global::URLFactory.RequestIdenCodePhp;
        }

        static StackObject* CopyToStack_RequestIdenCodePhp_2(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = global::URLFactory.RequestIdenCodePhp;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_RequestIdenCodePhp_2(ref object o, object v)
        {
            global::URLFactory.RequestIdenCodePhp = (System.String)v;
        }

        static StackObject* AssignFromStack_RequestIdenCodePhp_2(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.String @RequestIdenCodePhp = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            global::URLFactory.RequestIdenCodePhp = @RequestIdenCodePhp;
            return ptr_of_this_method;
        }

        static object get_RequestCloudBaseTicketPhp_3(ref object o)
        {
            return global::URLFactory.RequestCloudBaseTicketPhp;
        }

        static StackObject* CopyToStack_RequestCloudBaseTicketPhp_3(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = global::URLFactory.RequestCloudBaseTicketPhp;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_RequestCloudBaseTicketPhp_3(ref object o, object v)
        {
            global::URLFactory.RequestCloudBaseTicketPhp = (System.String)v;
        }

        static StackObject* AssignFromStack_RequestCloudBaseTicketPhp_3(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.String @RequestCloudBaseTicketPhp = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            global::URLFactory.RequestCloudBaseTicketPhp = @RequestCloudBaseTicketPhp;
            return ptr_of_this_method;
        }

        static object get_RequestSocketIOTokenPhp_4(ref object o)
        {
            return global::URLFactory.RequestSocketIOTokenPhp;
        }

        static StackObject* CopyToStack_RequestSocketIOTokenPhp_4(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = global::URLFactory.RequestSocketIOTokenPhp;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_RequestSocketIOTokenPhp_4(ref object o, object v)
        {
            global::URLFactory.RequestSocketIOTokenPhp = (System.String)v;
        }

        static StackObject* AssignFromStack_RequestSocketIOTokenPhp_4(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.String @RequestSocketIOTokenPhp = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            global::URLFactory.RequestSocketIOTokenPhp = @RequestSocketIOTokenPhp;
            return ptr_of_this_method;
        }

        static object get_GameVersion_5(ref object o)
        {
            return global::URLFactory.GameVersion;
        }

        static StackObject* CopyToStack_GameVersion_5(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = global::URLFactory.GameVersion;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_GameVersion_5(ref object o, object v)
        {
            global::URLFactory.GameVersion = (System.String)v;
        }

        static StackObject* AssignFromStack_GameVersion_5(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.String @GameVersion = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            global::URLFactory.GameVersion = @GameVersion;
            return ptr_of_this_method;
        }



    }
}
