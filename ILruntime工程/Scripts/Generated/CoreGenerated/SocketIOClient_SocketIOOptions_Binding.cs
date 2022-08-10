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
    unsafe class SocketIOClient_SocketIOOptions_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(SocketIOClient.SocketIOOptions);
            args = new Type[]{typeof(System.TimeSpan)};
            method = type.GetMethod("set_ConnectionTimeout", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_ConnectionTimeout_0);
            args = new Type[]{typeof(SocketIOClient.Transport.TransportProtocol)};
            method = type.GetMethod("set_Transport", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_Transport_1);
            args = new Type[]{typeof(System.Double)};
            method = type.GetMethod("set_RandomizationFactor", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_RandomizationFactor_2);
            args = new Type[]{typeof(System.Double)};
            method = type.GetMethod("set_ReconnectionDelay", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_ReconnectionDelay_3);
            args = new Type[]{typeof(System.Int32)};
            method = type.GetMethod("set_ReconnectionDelayMax", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_ReconnectionDelayMax_4);
            args = new Type[]{typeof(System.Int32)};
            method = type.GetMethod("set_ReconnectionAttempts", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_ReconnectionAttempts_5);
            args = new Type[]{typeof(System.String)};
            method = type.GetMethod("set_Path", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_Path_6);
            args = new Type[]{typeof(System.Boolean)};
            method = type.GetMethod("set_Reconnection", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_Reconnection_7);
            args = new Type[]{typeof(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String, System.String>>)};
            method = type.GetMethod("set_Query", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_Query_8);

            args = new Type[]{};
            method = type.GetConstructor(flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Ctor_0);

        }


        static StackObject* set_ConnectionTimeout_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.TimeSpan @value = (System.TimeSpan)typeof(System.TimeSpan).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)16);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            SocketIOClient.SocketIOOptions instance_of_this_method = (SocketIOClient.SocketIOOptions)typeof(SocketIOClient.SocketIOOptions).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.ConnectionTimeout = value;

            return __ret;
        }

        static StackObject* set_Transport_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            SocketIOClient.Transport.TransportProtocol @value = (SocketIOClient.Transport.TransportProtocol)typeof(SocketIOClient.Transport.TransportProtocol).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)20);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            SocketIOClient.SocketIOOptions instance_of_this_method = (SocketIOClient.SocketIOOptions)typeof(SocketIOClient.SocketIOOptions).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Transport = value;

            return __ret;
        }

        static StackObject* set_RandomizationFactor_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Double @value = *(double*)&ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            SocketIOClient.SocketIOOptions instance_of_this_method = (SocketIOClient.SocketIOOptions)typeof(SocketIOClient.SocketIOOptions).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.RandomizationFactor = value;

            return __ret;
        }

        static StackObject* set_ReconnectionDelay_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Double @value = *(double*)&ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            SocketIOClient.SocketIOOptions instance_of_this_method = (SocketIOClient.SocketIOOptions)typeof(SocketIOClient.SocketIOOptions).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.ReconnectionDelay = value;

            return __ret;
        }

        static StackObject* set_ReconnectionDelayMax_4(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Int32 @value = ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            SocketIOClient.SocketIOOptions instance_of_this_method = (SocketIOClient.SocketIOOptions)typeof(SocketIOClient.SocketIOOptions).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.ReconnectionDelayMax = value;

            return __ret;
        }

        static StackObject* set_ReconnectionAttempts_5(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Int32 @value = ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            SocketIOClient.SocketIOOptions instance_of_this_method = (SocketIOClient.SocketIOOptions)typeof(SocketIOClient.SocketIOOptions).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.ReconnectionAttempts = value;

            return __ret;
        }

        static StackObject* set_Path_6(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.String @value = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            SocketIOClient.SocketIOOptions instance_of_this_method = (SocketIOClient.SocketIOOptions)typeof(SocketIOClient.SocketIOOptions).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Path = value;

            return __ret;
        }

        static StackObject* set_Reconnection_7(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Boolean @value = ptr_of_this_method->Value == 1;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            SocketIOClient.SocketIOOptions instance_of_this_method = (SocketIOClient.SocketIOOptions)typeof(SocketIOClient.SocketIOOptions).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Reconnection = value;

            return __ret;
        }

        static StackObject* set_Query_8(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String, System.String>> @value = (System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String, System.String>>)typeof(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String, System.String>>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            SocketIOClient.SocketIOOptions instance_of_this_method = (SocketIOClient.SocketIOOptions)typeof(SocketIOClient.SocketIOOptions).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Query = value;

            return __ret;
        }


        static StackObject* Ctor_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);

            var result_of_this_method = new SocketIOClient.SocketIOOptions();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


    }
}
