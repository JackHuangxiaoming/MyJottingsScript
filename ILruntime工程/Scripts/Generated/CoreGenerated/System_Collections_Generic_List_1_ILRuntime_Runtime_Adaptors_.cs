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
    unsafe class System_Collections_Generic_List_1_ILRuntime_Runtime_Adaptors_IComparerAdaptor_Binding_Adaptor_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>);
            args = new Type[]{};
            method = type.GetMethod("get_Count", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_Count_0);
            args = new Type[]{typeof(System.Int32)};
            method = type.GetMethod("get_Item", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_Item_1);
            args = new Type[]{typeof(ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor)};
            method = type.GetMethod("Add", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Add_2);
            args = new Type[]{};
            method = type.GetMethod("Clear", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Clear_3);
            args = new Type[]{typeof(System.Predicate<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)};
            method = type.GetMethod("Find", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Find_4);
            args = new Type[]{typeof(System.Collections.Generic.IEnumerable<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)};
            method = type.GetMethod("AddRange", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, AddRange_5);
            args = new Type[]{typeof(System.Int32), typeof(System.Int32)};
            method = type.GetMethod("GetRange", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, GetRange_6);
            args = new Type[]{typeof(System.Predicate<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)};
            method = type.GetMethod("FindAll", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, FindAll_7);
            args = new Type[]{};
            method = type.GetMethod("Sort", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Sort_8);
            args = new Type[]{};
            method = type.GetMethod("GetEnumerator", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, GetEnumerator_9);
            args = new Type[]{};
            method = type.GetMethod("ToArray", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, ToArray_10);
            args = new Type[]{typeof(System.Int32), typeof(ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor)};
            method = type.GetMethod("set_Item", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_Item_11);
            args = new Type[]{typeof(ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor)};
            method = type.GetMethod("IndexOf", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, IndexOf_12);
            args = new Type[]{typeof(ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor)};
            method = type.GetMethod("Contains", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Contains_13);
            args = new Type[]{typeof(ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor)};
            method = type.GetMethod("Remove", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Remove_14);
            args = new Type[]{typeof(System.Predicate<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)};
            method = type.GetMethod("FindIndex", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, FindIndex_15);
            args = new Type[]{typeof(System.Comparison<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)};
            method = type.GetMethod("Sort", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Sort_16);
            args = new Type[]{typeof(System.Int32)};
            method = type.GetMethod("RemoveAt", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, RemoveAt_17);

            args = new Type[]{};
            method = type.GetConstructor(flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Ctor_0);
            args = new Type[]{typeof(System.Int32)};
            method = type.GetConstructor(flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Ctor_1);
            args = new Type[]{typeof(System.Collections.Generic.IEnumerable<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)};
            method = type.GetConstructor(flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Ctor_2);

        }


        static StackObject* get_Count_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor> instance_of_this_method = (System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)typeof(System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.Count;

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* get_Item_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Int32 @index = ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor> instance_of_this_method = (System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)typeof(System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method[index];

            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* Add_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor @item = (ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor)typeof(ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor> instance_of_this_method = (System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)typeof(System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Add(@item);

            return __ret;
        }

        static StackObject* Clear_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor> instance_of_this_method = (System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)typeof(System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Clear();

            return __ret;
        }

        static StackObject* Find_4(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Predicate<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor> @match = (System.Predicate<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)typeof(System.Predicate<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor> instance_of_this_method = (System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)typeof(System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.Find(@match);

            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* AddRange_5(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Collections.Generic.IEnumerable<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor> @collection = (System.Collections.Generic.IEnumerable<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)typeof(System.Collections.Generic.IEnumerable<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor> instance_of_this_method = (System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)typeof(System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.AddRange(@collection);

            return __ret;
        }

        static StackObject* GetRange_6(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Int32 @count = ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Int32 @index = ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor> instance_of_this_method = (System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)typeof(System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.GetRange(@index, @count);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* FindAll_7(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Predicate<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor> @match = (System.Predicate<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)typeof(System.Predicate<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor> instance_of_this_method = (System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)typeof(System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.FindAll(@match);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* Sort_8(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor> instance_of_this_method = (System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)typeof(System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Sort();

            return __ret;
        }

        static StackObject* GetEnumerator_9(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor> instance_of_this_method = (System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)typeof(System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.GetEnumerator();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* ToArray_10(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor> instance_of_this_method = (System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)typeof(System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.ToArray();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* set_Item_11(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor @value = (ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor)typeof(ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Int32 @index = ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor> instance_of_this_method = (System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)typeof(System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method[index] = value;

            return __ret;
        }

        static StackObject* IndexOf_12(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor @item = (ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor)typeof(ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor> instance_of_this_method = (System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)typeof(System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.IndexOf(@item);

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* Contains_13(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor @item = (ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor)typeof(ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor> instance_of_this_method = (System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)typeof(System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.Contains(@item);

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method ? 1 : 0;
            return __ret + 1;
        }

        static StackObject* Remove_14(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor @item = (ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor)typeof(ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor> instance_of_this_method = (System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)typeof(System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.Remove(@item);

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method ? 1 : 0;
            return __ret + 1;
        }

        static StackObject* FindIndex_15(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Predicate<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor> @match = (System.Predicate<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)typeof(System.Predicate<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor> instance_of_this_method = (System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)typeof(System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.FindIndex(@match);

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* Sort_16(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Comparison<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor> @comparison = (System.Comparison<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)typeof(System.Comparison<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor> instance_of_this_method = (System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)typeof(System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Sort(@comparison);

            return __ret;
        }

        static StackObject* RemoveAt_17(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Int32 @index = ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor> instance_of_this_method = (System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)typeof(System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.RemoveAt(@index);

            return __ret;
        }


        static StackObject* Ctor_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);

            var result_of_this_method = new System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* Ctor_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);
            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Int32 @capacity = ptr_of_this_method->Value;


            var result_of_this_method = new System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>(@capacity);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* Ctor_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);
            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Collections.Generic.IEnumerable<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor> @collection = (System.Collections.Generic.IEnumerable<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>)typeof(System.Collections.Generic.IEnumerable<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = new System.Collections.Generic.List<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>(@collection);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


    }
}
