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
    unsafe class Google_Protobuf_Collections_RepeatedField_1_Google_Protobuf_ProtobufIMessageAdaptor_Binding_Adaptor_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(Google.Protobuf.Collections.RepeatedField<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>);
            args = new Type[]{typeof(Google.Protobuf.Collections.RepeatedField<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>)};
            method = type.GetMethod("Equals", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Equals_0);
            args = new Type[]{typeof(Google.Protobuf.CodedOutputStream), typeof(Google.Protobuf.FieldCodec<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>)};
            method = type.GetMethod("WriteTo", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, WriteTo_1);
            args = new Type[]{typeof(Google.Protobuf.FieldCodec<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>)};
            method = type.GetMethod("CalculateSize", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, CalculateSize_2);
            args = new Type[]{typeof(System.Collections.Generic.IEnumerable<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>)};
            method = type.GetMethod("Add", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Add_3);
            args = new Type[]{typeof(Google.Protobuf.CodedInputStream), typeof(Google.Protobuf.FieldCodec<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>)};
            method = type.GetMethod("AddEntriesFrom", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, AddEntriesFrom_4);
            args = new Type[]{};
            method = type.GetMethod("Clone", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Clone_5);

            args = new Type[]{};
            method = type.GetConstructor(flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Ctor_0);

        }


        static StackObject* Equals_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            Google.Protobuf.Collections.RepeatedField<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor> @other = (Google.Protobuf.Collections.RepeatedField<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>)typeof(Google.Protobuf.Collections.RepeatedField<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            Google.Protobuf.Collections.RepeatedField<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor> instance_of_this_method = (Google.Protobuf.Collections.RepeatedField<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>)typeof(Google.Protobuf.Collections.RepeatedField<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.Equals(@other);

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method ? 1 : 0;
            return __ret + 1;
        }

        static StackObject* WriteTo_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            Google.Protobuf.FieldCodec<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor> @codec = (Google.Protobuf.FieldCodec<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>)typeof(Google.Protobuf.FieldCodec<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            Google.Protobuf.CodedOutputStream @output = (Google.Protobuf.CodedOutputStream)typeof(Google.Protobuf.CodedOutputStream).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            Google.Protobuf.Collections.RepeatedField<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor> instance_of_this_method = (Google.Protobuf.Collections.RepeatedField<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>)typeof(Google.Protobuf.Collections.RepeatedField<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.WriteTo(@output, @codec);

            return __ret;
        }

        static StackObject* CalculateSize_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            Google.Protobuf.FieldCodec<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor> @codec = (Google.Protobuf.FieldCodec<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>)typeof(Google.Protobuf.FieldCodec<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            Google.Protobuf.Collections.RepeatedField<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor> instance_of_this_method = (Google.Protobuf.Collections.RepeatedField<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>)typeof(Google.Protobuf.Collections.RepeatedField<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.CalculateSize(@codec);

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* Add_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Collections.Generic.IEnumerable<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor> @values = (System.Collections.Generic.IEnumerable<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>)typeof(System.Collections.Generic.IEnumerable<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            Google.Protobuf.Collections.RepeatedField<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor> instance_of_this_method = (Google.Protobuf.Collections.RepeatedField<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>)typeof(Google.Protobuf.Collections.RepeatedField<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Add(@values);

            return __ret;
        }

        static StackObject* AddEntriesFrom_4(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            Google.Protobuf.FieldCodec<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor> @codec = (Google.Protobuf.FieldCodec<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>)typeof(Google.Protobuf.FieldCodec<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            Google.Protobuf.CodedInputStream @input = (Google.Protobuf.CodedInputStream)typeof(Google.Protobuf.CodedInputStream).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            Google.Protobuf.Collections.RepeatedField<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor> instance_of_this_method = (Google.Protobuf.Collections.RepeatedField<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>)typeof(Google.Protobuf.Collections.RepeatedField<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.AddEntriesFrom(@input, @codec);

            return __ret;
        }

        static StackObject* Clone_5(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            Google.Protobuf.Collections.RepeatedField<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor> instance_of_this_method = (Google.Protobuf.Collections.RepeatedField<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>)typeof(Google.Protobuf.Collections.RepeatedField<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.Clone();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


        static StackObject* Ctor_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);

            var result_of_this_method = new Google.Protobuf.Collections.RepeatedField<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


    }
}
