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
    unsafe class RigidbodyAndBoxCollider_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(global::RigidbodyAndBoxCollider);
            args = new Type[]{typeof(System.Single), typeof(System.Single)};
            method = type.GetMethod("UpdateBound", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, UpdateBound_0);
            args = new Type[]{};
            method = type.GetMethod("get_Rigidbody", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_Rigidbody_1);
            args = new Type[]{typeof(System.Single), typeof(System.Single)};
            method = type.GetMethod("UpdateMoveTarget", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, UpdateMoveTarget_2);
            args = new Type[]{};
            method = type.GetMethod("CancelMoveTarget", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, CancelMoveTarget_3);
            args = new Type[]{typeof(System.Action<UnityEngine.Collision>)};
            method = type.GetMethod("SetOnCollisionEnter", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, SetOnCollisionEnter_4);
            args = new Type[]{typeof(System.Single), typeof(System.Single), typeof(System.Single)};
            method = type.GetMethod("UpdateMoveTargetByDistance", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, UpdateMoveTargetByDistance_5);


        }


        static StackObject* UpdateBound_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Single @height = *(float*)&ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Single @width = *(float*)&ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            global::RigidbodyAndBoxCollider instance_of_this_method = (global::RigidbodyAndBoxCollider)typeof(global::RigidbodyAndBoxCollider).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.UpdateBound(@width, @height);

            return __ret;
        }

        static StackObject* get_Rigidbody_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            global::RigidbodyAndBoxCollider instance_of_this_method = (global::RigidbodyAndBoxCollider)typeof(global::RigidbodyAndBoxCollider).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.Rigidbody;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* UpdateMoveTarget_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Single @y = *(float*)&ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Single @x = *(float*)&ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            global::RigidbodyAndBoxCollider instance_of_this_method = (global::RigidbodyAndBoxCollider)typeof(global::RigidbodyAndBoxCollider).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.UpdateMoveTarget(@x, @y);

            return __ret;
        }

        static StackObject* CancelMoveTarget_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            global::RigidbodyAndBoxCollider instance_of_this_method = (global::RigidbodyAndBoxCollider)typeof(global::RigidbodyAndBoxCollider).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.CancelMoveTarget();

            return __ret;
        }

        static StackObject* SetOnCollisionEnter_4(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Action<UnityEngine.Collision> @hander = (System.Action<UnityEngine.Collision>)typeof(System.Action<UnityEngine.Collision>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            global::RigidbodyAndBoxCollider instance_of_this_method = (global::RigidbodyAndBoxCollider)typeof(global::RigidbodyAndBoxCollider).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.SetOnCollisionEnter(@hander);

            return __ret;
        }

        static StackObject* UpdateMoveTargetByDistance_5(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 4);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Single @Distance = *(float*)&ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Single @y = *(float*)&ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            System.Single @x = *(float*)&ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 4);
            global::RigidbodyAndBoxCollider instance_of_this_method = (global::RigidbodyAndBoxCollider)typeof(global::RigidbodyAndBoxCollider).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.UpdateMoveTargetByDistance(@x, @y, @Distance);

            return __ret;
        }



    }
}
