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
    unsafe class Google_Protobuf_UnknownFieldSet_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(Google.Protobuf.UnknownFieldSet);
            args = new Type[]{typeof(Google.Protobuf.CodedOutputStream)};
            method = type.GetMethod("WriteTo", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, WriteTo_0);
            args = new Type[]{};
            method = type.GetMethod("CalculateSize", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, CalculateSize_1);
            args = new Type[]{typeof(Google.Protobuf.UnknownFieldSet), typeof(Google.Protobuf.UnknownFieldSet)};
            method = type.GetMethod("MergeFrom", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, MergeFrom_2);
            args = new Type[]{typeof(Google.Protobuf.UnknownFieldSet), typeof(Google.Protobuf.CodedInputStream)};
            method = type.GetMethod("MergeFieldFrom", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, MergeFieldFrom_3);
            args = new Type[]{typeof(Google.Protobuf.UnknownFieldSet)};
            method = type.GetMethod("Clone", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Clone_4);


        }


        static StackObject* WriteTo_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            Google.Protobuf.CodedOutputStream @output = (Google.Protobuf.CodedOutputStream)typeof(Google.Protobuf.CodedOutputStream).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            Google.Protobuf.UnknownFieldSet instance_of_this_method = (Google.Protobuf.UnknownFieldSet)typeof(Google.Protobuf.UnknownFieldSet).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.WriteTo(@output);

            return __ret;
        }

        static StackObject* CalculateSize_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            Google.Protobuf.UnknownFieldSet instance_of_this_method = (Google.Protobuf.UnknownFieldSet)typeof(Google.Protobuf.UnknownFieldSet).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.CalculateSize();

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* MergeFrom_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            Google.Protobuf.UnknownFieldSet @other = (Google.Protobuf.UnknownFieldSet)typeof(Google.Protobuf.UnknownFieldSet).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            Google.Protobuf.UnknownFieldSet @unknownFields = (Google.Protobuf.UnknownFieldSet)typeof(Google.Protobuf.UnknownFieldSet).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = Google.Protobuf.UnknownFieldSet.MergeFrom(@unknownFields, @other);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* MergeFieldFrom_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            Google.Protobuf.CodedInputStream @input = (Google.Protobuf.CodedInputStream)typeof(Google.Protobuf.CodedInputStream).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            Google.Protobuf.UnknownFieldSet @unknownFields = (Google.Protobuf.UnknownFieldSet)typeof(Google.Protobuf.UnknownFieldSet).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = Google.Protobuf.UnknownFieldSet.MergeFieldFrom(@unknownFields, @input);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* Clone_4(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            Google.Protobuf.UnknownFieldSet @other = (Google.Protobuf.UnknownFieldSet)typeof(Google.Protobuf.UnknownFieldSet).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = Google.Protobuf.UnknownFieldSet.Clone(@other);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }



    }
}
