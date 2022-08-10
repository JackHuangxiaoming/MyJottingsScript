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
    unsafe class System_Collections_Generic_Queue_1_List_1_ILRuntime_Runtime_Adaptors_IComparerAdaptor_Binding_Adaptor_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(System.Collections.Generic.Queue<System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>>);
            args = new Type[]{typeof(System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)};
            method = type.GetMethod("Enqueue", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Enqueue_0);
            args = new Type[]{};
            method = type.GetMethod("get_Count", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_Count_1);
            args = new Type[]{};
            method = type.GetMethod("Dequeue", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Dequeue_2);

            args = new Type[]{};
            method = type.GetConstructor(flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Ctor_0);

        }


        static StackObject* Enqueue_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor> @item = (System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)typeof(System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Collections.Generic.Queue<System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>> instance_of_this_method = (System.Collections.Generic.Queue<System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>>)typeof(System.Collections.Generic.Queue<System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Enqueue(@item);

            return __ret;
        }

        static StackObject* get_Count_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Collections.Generic.Queue<System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>> instance_of_this_method = (System.Collections.Generic.Queue<System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>>)typeof(System.Collections.Generic.Queue<System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.Count;

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* Dequeue_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Collections.Generic.Queue<System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>> instance_of_this_method = (System.Collections.Generic.Queue<System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>>)typeof(System.Collections.Generic.Queue<System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.Dequeue();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


        static StackObject* Ctor_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);

            var result_of_this_method = new System.Collections.Generic.Queue<System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>>();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


    }
}
